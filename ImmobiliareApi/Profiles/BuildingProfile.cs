using ImmobiliareApi.Entities;
using ImmobiliareApi.Models.BuildingModels;

namespace ImmobiliareApi.Profiles
{
    public class BuildingProfile : AutoMapper.Profile
    {
        public BuildingProfile()
        {
            CreateMap<Building, BuildingCreateModel>();
            CreateMap<Building, BuildingUpdateModel>();
            CreateMap<Building, BuildingSelectModel>();
            CreateMap<BuildingSelectModel, BuildingUpdateModel>();
            CreateMap<BuildingUpdateModel, BuildingSelectModel>();

            CreateMap<BuildingCreateModel, Building>();
            CreateMap<BuildingUpdateModel, Building>();
            CreateMap<BuildingSelectModel, Building>();
        }
    }
}
