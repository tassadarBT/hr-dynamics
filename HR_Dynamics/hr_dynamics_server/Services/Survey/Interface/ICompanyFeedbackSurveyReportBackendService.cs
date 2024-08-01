using hr_dynamics_server.ViewModels.Survey.Backend;

namespace hr_dynamics_server.Services.Survey.Interface
{
    public interface ICompanyFeedbackSurveyReportBackendService
    {
        Task<CompanyFeedbackSurveyBackendReportFilterViewModel> GetFilterData(CancellationToken cancellationToken);
        Task<CompanyFeedbackSurveyBackendReportViewModel> GetReportData(CompanyFeedbackSurveyBackendReportFilterViewModel filterVm, CancellationToken cancellationToken);
        Task<byte[]> ExportReportData(CompanyFeedbackSurveyBackendReportFilterViewModel filter, CancellationToken cancellationToken);
    }
}
