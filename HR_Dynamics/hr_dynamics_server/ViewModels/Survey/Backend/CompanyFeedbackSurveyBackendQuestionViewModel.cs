namespace hr_dynamics_server.ViewModels.Survey.Backend
{
    public class CompanyFeedbackSurveyBackendQuestionViewModel
    {
        public int Id { get; set; }
        public string? DisplayOrderText { get; set; }
        public string? Text { get; set; }
        public CompanyFeedbackSurveyBackendQuestionAnswerPieChartViewModel? AnswerPieChartData { get; set; }
    }
}
