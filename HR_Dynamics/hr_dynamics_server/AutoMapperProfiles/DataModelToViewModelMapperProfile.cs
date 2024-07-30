using hr_dynamics_server.Data.DataModels;
using hr_dynamics_server.ViewModels.Survey.Frontend.Shared;

namespace hr_dynamics_server.AutoMapperProfiles
{
    public class DataModelToViewModelMapperProfile : AutoMapper.Profile
    {
        public override string ProfileName => "DataModelToViewModelMapperProfile";

        public DataModelToViewModelMapperProfile()
        {
            CreateMap<QuestionDataModel, FrontendQuestionViewModel>();
        }

    }
}
