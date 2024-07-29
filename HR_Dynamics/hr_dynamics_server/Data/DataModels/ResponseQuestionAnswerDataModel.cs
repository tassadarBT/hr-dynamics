using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace hr_dynamics_server.Data.DataModels
{
    public class ResponseQuestionAnswerDataModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ResponseId { get; set; }
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
        [Required, MaxLength(128)]
        public string? Value { get; set; }
        [Required, MaxLength(256)]
        public string? Text { get; set; }
        [ForeignKey("ResponseId")]
        public ResponseDataModel? Response { get; set; }
        [ForeignKey("SurveyId")]
        public SurveyDataModel? Survey { get; set; }
    }
}
