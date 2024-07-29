using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hr_dynamics_server.Data.DataModels
{
    [Table("QuestionOptions")]
    public class QuestionOptionDataModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? OptionGroupId { get; set; }
        public int DisplayOrder { get; set; }
        [Required, MaxLength(128)]
        public string? Value { get; set; }
        [Required, MaxLength(256)]
        public string? Text { get; set; }
        public bool Active { get; set; }
        [ForeignKey("OptionGroupId")]
        public QuestionOptionGroupDataModel? OptionGroup { get; set; }
    }
}
