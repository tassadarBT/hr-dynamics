using hr_dynamics_server.Data.DataModels;
using Microsoft.EntityFrameworkCore;

namespace hr_dynamics_server.Data
{
    public class HrDynamicsDbContext : DbContext
    {
        public HrDynamicsDbContext(DbContextOptions<HrDynamicsDbContext> options)
       : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<CampaignDataModel> Campaigns { get; set; }
        public DbSet<SurveyDataModel> Surveys { get; set; }
        public DbSet<QuestionDataModel> Questions { get; set; }
        public DbSet<QuestionOptionGroupDataModel> QuestionOptionGroups { get; set; }
        public DbSet<QuestionOptionDataModel> QuestionOptions { get; set; }
        public DbSet<ResponseDataModel> Responses { get; set; }
        public DbSet<ResponseQuestionAnswerDataModel> ResponseQuestionAnswers { get; set; }
    }
}
