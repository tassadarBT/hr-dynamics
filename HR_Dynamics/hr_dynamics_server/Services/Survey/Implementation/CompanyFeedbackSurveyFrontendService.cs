using AutoMapper;
using hr_dynamics_server.Data;
using hr_dynamics_server.Data.DataModels;
using hr_dynamics_server.ViewModels.Shared;
using hr_dynamics_server.ViewModels.Survey.Frontend.CompanyFeedbackSurvey;
using hr_dynamics_server.ViewModels.Survey.Frontend.Shared;
using Microsoft.EntityFrameworkCore;

namespace hr_dynamics_server.Services.Survey.Implementation
{
    public class CompanyFeedbackSurveyFrontendService : ICompanyFeedbackSurveyFrontendService
    {
        private readonly HrDynamicsDbContext _hrDynamicsDbContext;
        private readonly IMapper _mapper;
        public CompanyFeedbackSurveyFrontendService(HrDynamicsDbContext hrDynamicsDbContext, IMapper mapper)
        {
            _hrDynamicsDbContext = hrDynamicsDbContext;
            _mapper = mapper;
        }

        public async Task<CompanyFeedbackSurveySaveViewModel> GetCompanyFeedbackSurveyData(CancellationToken cancellationToken)
        {
            var optionDbs = await _hrDynamicsDbContext.QuestionOptions.AsNoTracking().ToListAsync(cancellationToken);
            var questionDbs = await _hrDynamicsDbContext.Questions.AsNoTracking().Include(t => t.Parent).Where(t => t.SurveyId == 1 && t.Active).ToListAsync(cancellationToken);
            var sectionVms = _mapper.Map<List<FrontendQuestionViewModel>>(questionDbs.Where(t => t.ParentId == null).OrderBy(t => t.DisplayOrder));

            foreach (var sectionVm in sectionVms)
            {
                sectionVm.Questions = _mapper.Map<List<FrontendQuestionViewModel>>(questionDbs.Where(t => t.ParentId == sectionVm.Id).OrderBy(t => t.DisplayOrder));
                sectionVm.Options = optionDbs.Where(t => t.Active && t.OptionGroupId == 1).OrderBy(t => t.DisplayOrder).Select(t => new FrontendQuestionOptionViewModel { Id = t.Value, Description = t.Text }).ToList();
                foreach(var question in sectionVm.Questions ?? new List<FrontendQuestionViewModel>())                
                    question.Options = optionDbs.Where(t => t.Active && t.OptionGroupId == 1).OrderBy(t => t.DisplayOrder).Select(t => new FrontendQuestionOptionViewModel { Id = t.Value, Description = t.Text }).ToList();                
            }
            return new CompanyFeedbackSurveySaveViewModel { StartTime = DateTime.UtcNow, Sections = sectionVms };
        }

        public async Task<SaveResultViewModel<int>> SaveCompanyFeedbackSurveyData(CompanyFeedbackSurveySaveViewModel vm, CancellationToken cancellationToken)
        {
            var res = new SaveResultViewModel<int> { Success = true };
            try
            {
                var questionOptionDbs = await _hrDynamicsDbContext.QuestionOptions.Where(t => t.OptionGroupId == 1).ToListAsync(cancellationToken);
                var responseDb = new ResponseDataModel { SurveyId = 1, CampaignId = await GetLastEligibleCampaignIdByStartTime(vm.StartTime.GetValueOrDefault(), cancellationToken), StartTime = vm.StartTime.Value, EndTime = DateTime.UtcNow };
                if (vm.Sections != null)
                {
                    var sectionAnswerDbs = vm.Sections.Where(t => t.Questions == null || !t.Questions.Any()).Select(t => new ResponseQuestionAnswerDataModel { Response = responseDb, QuestionId = t.Id, Value = t.AnswerValue,  Text = questionOptionDbs.FirstOrDefault(opt => opt.Value == t.AnswerValue)?.Text }).ToList();
                    var questionAnswerDbs = vm.Sections.Where(t => t.Questions != null && t.Questions.Any()).SelectMany(t => t.Questions ?? new List<FrontendQuestionViewModel>()).Select(t => new ResponseQuestionAnswerDataModel { Response = responseDb, QuestionId = t.Id, Value = t.AnswerValue, Text = questionOptionDbs.FirstOrDefault(opt => opt.Value ==  t.AnswerValue)?.Text }).ToList();
                    _hrDynamicsDbContext.ResponseQuestionAnswers.AddRange(sectionAnswerDbs);
                    _hrDynamicsDbContext.ResponseQuestionAnswers.AddRange(questionAnswerDbs);
                }
                await _hrDynamicsDbContext.SaveChangesAsync(cancellationToken);
                res.Result = responseDb.Id;
            }
            catch
            {
                res.Success = false;
                res.ErrorMessage = "Save failed!";
            }
            return res;
        }

        private async Task<int?> GetLastEligibleCampaignIdByStartTime(DateTime surveyStartTime, CancellationToken cancellationToken)
        {
            var lastEligibleCampaignId = await _hrDynamicsDbContext.Campaigns.OrderByDescending(t => t.StartTime).FirstOrDefaultAsync(t => t.Active && t.SurveyId == 1 && surveyStartTime >= t.StartTime && surveyStartTime <= t.EndTime, cancellationToken);
            return lastEligibleCampaignId?.Id;                 
        }
    }
}
