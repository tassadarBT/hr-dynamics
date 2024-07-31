using hr_dynamics_server.ViewModels.Shared;
using hr_dynamics_server.ViewModels.Survey.Frontend.CompanyFeedbackSurvey;

namespace hr_dynamics_server.Services.Survey.Implementation
{
    public interface ICompanyFeedbackSurveyFrontendService
    {
        Task<CompanyFeedbackSurveySaveViewModel> GetCompanyFeedbackSurveyData(CancellationToken cancellationToken);
        Task<SaveResultViewModel<int>> SaveCompanyFeedbackSurveyData(CompanyFeedbackSurveySaveViewModel vm, CancellationToken cancellationToken);
    }
}
