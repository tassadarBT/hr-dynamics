namespace hr_dynamics_server.ViewModels.Survey.Backend
{
    public class CompanyFeedbackSurveyBackendQuestionViewModel
    {
        public int Id { get; set; }
        public string? DisplayOrderText { get; set; }
        public string? Text { get; set; }
        public List<string>? ChartLabels { get; set; }
        public List<string>? ChartColors { get; set; }
        public List<int>? ChartCountValues { get; set; }
    }
}
