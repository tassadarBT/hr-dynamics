using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hr_dynamics_server.Data.DataModels
{
    [Table("Responses")]
    public class ResponseDataModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int? CampaignId { get; set; }
        [MaxLength(450)]
        public string? UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [MaxLength(1024)]
        public string? Notes { get; set; }
        [ForeignKey("SurveyId")]
        public SurveyDataModel? Survey { get; set; }
        [ForeignKey("CampaignId")]
        public CampaignDataModel? Campaign { get; set; }
    }
}
