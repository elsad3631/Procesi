using ImmobiliareApi.Entities.Typologies;
using ImmobiliareApi.Models.BuildingModels;
using ImmobiliareApi.Models.ListViewModel;
using ImmobiliareApi.Models.TypologiesModels.BuildingTypeModels;

namespace ImmobiliareApi.Interfaces.IBusinessServices.ITypologiesServices
{
    public interface IBuildingTypeServices
    {
        Task Create(BuildingTypeCreateModel dto);
        Task<ListViewModel<BuildingTypeSelectModel>> Get(int currentPage, string? filterRequest, char? fromName, char? toName);
        Task<BuildingTypeSelectModel> Update(BuildingTypeUpdateModel dto);
        Task<BuildingTypeSelectModel> GetById(int id);
        Task<BuildingType> Delete(int id);
    }
}
