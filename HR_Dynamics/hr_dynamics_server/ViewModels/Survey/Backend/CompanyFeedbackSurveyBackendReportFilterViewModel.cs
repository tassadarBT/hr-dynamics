using hr_dynamics_server.ViewModels.Shared;

namespace hr_dynamics_server.ViewModels.Survey.Backend
{
    public class CompanyFeedbackSurveyBackendReportFilterViewModel
    {
        public int CampaignId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public List<OptionViewModel<int>>? CampaignOptions { get; set; }
    }
}
