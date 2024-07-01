using ImmobiliareApi.Entities;
using ImmobiliareApi.Models.BuildingModels;
using ImmobiliareApi.Models.ListViewModel;

namespace ImmobiliareApi.Interfaces.IBusinessServices
{
    public interface IBuildingServices
    {
        Task Create(BuildingCreateModel dto);
        Task<ListViewModel<BuildingSelectModel>> Get(int currentPage, string? filterRequest, char? fromName, char? toName);
        Task<BuildingSelectModel> Update(BuildingUpdateModel dto);
        Task<BuildingSelectModel> GetById(int id);
        Task<Building> Delete(int id);
    }
}
