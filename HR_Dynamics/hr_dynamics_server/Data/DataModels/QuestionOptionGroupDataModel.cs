using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hr_dynamics_server.Data.DataModels
{
    [Table("QuestionOptionGroups")]
    public class QuestionOptionGroupDataModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(256)]
        public string? Name { get; set; }        
        public bool Active { get; set; }
    }
}
