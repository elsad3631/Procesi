using ImmobiliareApi.Entities.Typologies;
using ImmobiliareApi.Models.BuildingModels;
using ImmobiliareApi.Models.TypologiesModels.BuildingTypeModels;

namespace ImmobiliareApi.Profiles.TypologiesProfiles
{
    public class BuildingTypeProfile : AutoMapper.Profile
    {
        public BuildingTypeProfile()
        {
            CreateMap<BuildingType, BuildingTypeCreateModel>();
            CreateMap<BuildingType, BuildingTypeUpdateModel>();
            CreateMap<BuildingType, BuildingTypeSelectModel>();
            CreateMap<BuildingTypeSelectModel, BuildingTypeUpdateModel>();
            CreateMap<BuildingTypeUpdateModel, BuildingTypeSelectModel>();

            CreateMap<BuildingTypeCreateModel, BuildingType>();
            CreateMap<BuildingTypeUpdateModel, BuildingType>();
            CreateMap<BuildingTypeSelectModel, BuildingType>();
        }
    }
}
