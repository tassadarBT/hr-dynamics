using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hr_dynamics_server.Data.DataModels
{
    [Table("Surveys")]
    public class SurveyDataModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(256)]
	    public string? Name { get; set; }
        [MaxLength(1024)]
        public string? Description { get; set; }
        
	    public bool IsAnonymous { get; set; }
	    public bool Active { get; set; }

        public List<QuestionDataModel> Questions { get; set; }
    }
}
