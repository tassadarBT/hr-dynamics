using hr_dynamics_server.Validation.Attributes;

namespace hr_dynamics_server.ViewModels.Survey.Frontend.Shared
{
    public class FrontendQuestionViewModel
    {
        public int Id { get; set; }
        public string? DisplayOrderText { get; set; }
        public string? Type { get; set; }
        public string? Text { get; set; }
        [RequiredIfQuestion]
        public string? AnswerValue { get; set; }
        public List<FrontendQuestionOptionViewModel>? Options { get; set; }
        public List<FrontendQuestionViewModel>? Questions { get; set; }
    }
}
