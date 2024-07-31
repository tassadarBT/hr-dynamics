using hr_dynamics_server.ViewModels.Survey.Backend;

namespace hr_dynamics_server.Services.Survey.Interface
{
    public interface ICompanyFeedbackSurveyReportBackendService
    {
        Task<CompanyFeedbackSurveyBackendReportFilterViewModel> GetFilter(CancellationToken cancellationToken);
        Task<CompanyFeedbackSurveyBackendReportViewModel> GetReport(CompanyFeedbackSurveyBackendReportFilterViewModel filterVm, CancellationToken cancellationToken);
    }
}
