using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hr_dynamics_server.Data.DataModels
{
    [Table("Campaigns")]
    public class CampaignDataModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(256)]
        public string? Name { get; set; }
        [MaxLength(1024)]
        public string? Description { get; set; }
        public int SurveyId { get; set; }
        public string? RecipientEmails { get; set; }
	    public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool Active { get; set; }
        [ForeignKey("SurveyId")]
        public SurveyDataModel? Survey { get; set; }
    }
}
