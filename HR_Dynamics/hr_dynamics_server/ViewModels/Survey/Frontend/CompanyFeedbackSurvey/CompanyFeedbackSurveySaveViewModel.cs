using hr_dynamics_server.ViewModels.Survey.Frontend.Shared;
using System.ComponentModel.DataAnnotations;

namespace hr_dynamics_server.ViewModels.Survey.Frontend.CompanyFeedbackSurvey
{
    public class CompanyFeedbackSurveySaveViewModel
    {
        [Required]
        public DateTime? StartTime { get; set; }
        public List<FrontendQuestionViewModel>? Sections { get; set; }
    }
}
