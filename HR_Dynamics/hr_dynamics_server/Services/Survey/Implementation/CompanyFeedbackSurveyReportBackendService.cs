using AutoMapper;
using ClosedXML.Excel;
using hr_dynamics_server.Data;
using hr_dynamics_server.Services.Survey.Interface;
using hr_dynamics_server.ViewModels.Shared;
using hr_dynamics_server.ViewModels.Survey.Backend;
using Microsoft.EntityFrameworkCore;

namespace hr_dynamics_server.Services.Survey.Implementation
{
    public class CompanyFeedbackSurveyReportBackendService : ICompanyFeedbackSurveyReportBackendService
    {
        private readonly HrDynamicsDbContext _hrDynamicsDbContext;
        private readonly IMapper _mapper;
        public CompanyFeedbackSurveyReportBackendService(HrDynamicsDbContext hrDynamicsDbContext, IMapper mapper)
        {
            _hrDynamicsDbContext = hrDynamicsDbContext;
            _mapper = mapper;
        }

        public async Task<CompanyFeedbackSurveyBackendReportFilterViewModel> GetFilterData(CancellationToken cancellationToken)
        {
            var campaignOptions = await _hrDynamicsDbContext.Campaigns.OrderByDescending(t => t.EndTime).AsNoTracking().Where(t => t.SurveyId == 1).Select(t => new OptionViewModel<int> { Value = t.Id, Text = t.Description }).ToListAsync(cancellationToken);
            campaignOptions.Insert(0, new OptionViewModel<int> { Value = -1, Text = "All" });
            var res = new CompanyFeedbackSurveyBackendReportFilterViewModel { CampaignId = -1, CampaignOptions = campaignOptions };
            return res;
        }

        public async Task<CompanyFeedbackSurveyBackendReportViewModel> GetReportData(CompanyFeedbackSurveyBackendReportFilterViewModel filter, CancellationToken cancellationToken)
        {
            var options = await _hrDynamicsDbContext.QuestionOptions.Where(t => t.OptionGroupId == 1).OrderBy(t => t.DisplayOrder).ToListAsync(cancellationToken);
            var res = new CompanyFeedbackSurveyBackendReportViewModel {
                Questions = await _hrDynamicsDbContext.Questions.Include(t => t.Parent).AsNoTracking().Where(t => t.SurveyId == 1 && t.IsSection == false || t.Type == "section-radio-list").OrderBy(t => t.DisplayOrder)
                .Select(t => new CompanyFeedbackSurveyBackendQuestionViewModel { Id = t.Id, DisplayOrderText = t.DisplayOrderText, Text = t.Parent != null ? $"{t.Parent.Text} {t.Text}" : t.Text }).ToListAsync(cancellationToken)
            }; 
            var answerDbs = await _hrDynamicsDbContext.ResponseQuestionAnswers.Include(t => t.Response).AsNoTracking()
                .Where(t => t.Response != null && (filter.CampaignId < 0 ||  t.Response.CampaignId == filter.CampaignId) 
                                               && (filter.StartTime == null || t.Response.StartTime >= filter.StartTime)
                                               && (filter.EndTime == null || t.Response.EndTime <= filter.EndTime)).ToListAsync(cancellationToken);
            foreach(var question in res.Questions)
            {
                var questionAnswerDbs = answerDbs.Where(t => t.QuestionId == question.Id).GroupBy(t => t.Text).Select(t => new { label = t.Key, count = t.Count()}).OrderBy(t => t.label).ToList();
                var labels = questionAnswerDbs.Select(t => t.label ?? string.Empty).ToList();
                var descriptions = labels.Select(t => options.First(o => o.Text == t).Description ?? string.Empty).ToList();
                var counts = questionAnswerDbs.Select(t => t.count).ToList();
                var colors = labels.Select(t => options.First(o => o.Text == t).Color).ToList();
                question.AnswerPieChartData = new CompanyFeedbackSurveyBackendQuestionAnswerPieChartViewModel
                {
                    Labels = descriptions,
                    Datasets = new List<CompanyFeedbackSurveyBackendQuestionAnswerPieChartDataSetsViewModel?>()
                    {
                        new CompanyFeedbackSurveyBackendQuestionAnswerPieChartDataSetsViewModel {
                            BackgroundColor =  colors,
                            Data = counts
                        }
                    }
                };
            }
            return res;
        }

        public async Task<byte[]> ExportReportData(CompanyFeedbackSurveyBackendReportFilterViewModel filter, CancellationToken cancellationToken)
        {
            byte[] excelFileBytes;
            var optionDbs = await _hrDynamicsDbContext.QuestionOptions.Where(t => t.OptionGroupId == 1).OrderBy(t => t.DisplayOrder).ToListAsync(cancellationToken);
            var questionDbs = await _hrDynamicsDbContext.Questions.Include(t => t.Parent).AsNoTracking().Where(t => t.SurveyId == 1 && t.IsSection == false || t.Type == "section-radio-list").OrderBy(t => t.DisplayOrder)
                .Select(t => new { t.Id, t.DisplayOrderText, Text = t.Parent != null ? $"{t.Parent.Text} {t.Text}" : t.Text }).ToListAsync(cancellationToken);          
            var answerDbs = await _hrDynamicsDbContext.ResponseQuestionAnswers.Include(t => t.Response).AsNoTracking()
                .Where(t => t.Response != null && (filter.CampaignId < 0 || t.Response.CampaignId == filter.CampaignId)
                                               && (filter.StartTime == null || t.Response.StartTime >= filter.StartTime)
                                               && (filter.EndTime == null || t.Response.EndTime <= filter.StartTime)).ToListAsync(cancellationToken);
            var responseDbs = answerDbs.Select(t => t.Response).GroupBy(t => t.Id).Select(t => t.First()).OrderBy(t => t.StartTime).ToList();
            var sheetIndex = 0;

            using (var workbook = new XLWorkbook())
            {
                foreach (var responseDb in responseDbs)
                {
                    workbook.Worksheets.Add($"{responseDb?.StartTime.ToString("dd-MM-yyyy HH_mm")} #{sheetIndex+1} Response", sheetIndex);
                    var workSheet = workbook.Worksheets.ToList()[sheetIndex];
                    workSheet.Cell(1, 1).SetValue("Idx");
                    workSheet.Cell(1, 2).SetValue("Text");
                    workSheet.Cell(1, 3).SetValue("Answer");
                    var rowIndex = 2;
                    foreach (var questionDb in questionDbs)
                    {
                        var answerDb = answerDbs.FirstOrDefault(t => t.QuestionId == questionDb.Id && t.ResponseId == responseDb?.Id);
                        workSheet.Cell(rowIndex, 1).SetValue(questionDb.DisplayOrderText);
                        workSheet.Cell(rowIndex, 2).SetValue(questionDb.Text);
                        workSheet.Cell(rowIndex, 3).SetValue(optionDbs.FirstOrDefault(t => t.Value == answerDb?.Value)?.Description);
                        rowIndex++;
                    }
                    workSheet.Columns().AdjustToContents(0, (double)100);
                    sheetIndex++;
                }     
                using (var mms = new MemoryStream())
                {
                    workbook.SaveAs(mms);
                    excelFileBytes = mms.ToArray();
                }
            }
            
            return excelFileBytes;
        }
    }
}
