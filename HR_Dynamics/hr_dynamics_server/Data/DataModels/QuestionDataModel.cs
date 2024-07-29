using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hr_dynamics_server.Data.DataModels
{
    [Table("Questions")]
    public class QuestionDataModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int? ParentId { get; set; }
        public int? OptionGroupId { get; set; }
        public int DisplayOrder { get; set; }
        [Required, MaxLength(128)]
        public string? DisplayOrderText { get; set; }
        [Required, MaxLength(128)]
        public string? Type { get; set; }
        [Required, MaxLength(1024)]
        public string? Text { get; set; }
        public bool IsSection { get; set; }
        public bool Required { get; set; }
        public bool Active { get; set; }
        [ForeignKey("SurveyId")]
        public SurveyDataModel? Survey { get; set; }
        [ForeignKey("OptionGroupId")]
        public QuestionOptionGroupDataModel? OptionGroup { get; set; }
        [ForeignKey("ParentId")]
        public QuestionDataModel? Parent { get; set; }
    }
}
