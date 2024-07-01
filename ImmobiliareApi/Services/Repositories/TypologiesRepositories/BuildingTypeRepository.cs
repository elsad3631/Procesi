using ImmobiliareApi.DataContext;
using ImmobiliareApi.Entities.Typologies;
using ImmobiliareApi.Interfaces.IRepositories;
using ImmobiliareApi.Interfaces.IRepositories.ITypologiesRepositories;

namespace ImmobiliareApi.Services.Repositories.TypologiesRepositories
{
    public class BuildingTypeRepository : GenericRepository<BuildingType>, IBuildingTypeRepository
    {
        public BuildingTypeRepository(ImmobiliareApiContext dbContext) : base(dbContext)
        {

        }
    }
}
