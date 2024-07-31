using AutoMapper;
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

        public async Task<CompanyFeedbackSurveyBackendReportFilterViewModel> GetFilter(CancellationToken cancellationToken)
        {
            var campaignOptions = await _hrDynamicsDbContext.Campaigns.OrderByDescending(t => t.EndTime).AsNoTracking().Where(t => t.SurveyId == 1).Select(t => new OptionViewModel<int> { Value = t.Id, Text = t.Description }).ToListAsync(cancellationToken);
            campaignOptions.Insert(0, new OptionViewModel<int> { Value = -1, Text = "All" });
            var res = new CompanyFeedbackSurveyBackendReportFilterViewModel { CampaignId = -1, CampaignOptions = campaignOptions };
            return res;
        }

        public async Task<CompanyFeedbackSurveyBackendReportViewModel> GetReport(CompanyFeedbackSurveyBackendReportFilterViewModel filter, CancellationToken cancellationToken)
        {
            var res = new CompanyFeedbackSurveyBackendReportViewModel {
                Questions = await _hrDynamicsDbContext.Questions.Include(t => t.Parent).AsNoTracking().Where(t => t.SurveyId == 1 && t.IsSection == false).OrderBy(t => t.DisplayOrder)
                .Select(t => new CompanyFeedbackSurveyBackendQuestionViewModel { Id = t.Id, DisplayOrderText = t.DisplayOrderText, Text = t.Parent != null ? $"{t.Parent.Text} {t.Text}" : t.Text }).ToListAsync(cancellationToken)
            };                        
            var answerDbs = await _hrDynamicsDbContext.ResponseQuestionAnswers.Include(t => t.Response).AsNoTracking()
                .Where(t => t.Response != null && (filter.CampaignId < 0 ||  t.Response.CampaignId == filter.CampaignId) 
                                               && (filter.StartTime == null || t.Response.StartTime >= filter.StartTime)
                                               && (filter.EndTime == null || t.Response.EndTime <= filter.StartTime)).ToListAsync(cancellationToken);
            foreach(var question in res.Questions)
            {
                var questionAnswerDbs = answerDbs.Where(t => t.QuestionId == question.Id).GroupBy(t => t.Value).Select(t => new { label = t.Key, count = t.Count()}).OrderBy(t => t.count).ToList();
                question.ChartLabels = questionAnswerDbs.Select(t => t.label ?? string.Empty).ToList();
                question.ChartCountValues = questionAnswerDbs.Select(t => t.count).ToList();
            }
            return res;
        }
    }
}
