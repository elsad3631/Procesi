using ImmobiliareApi.DataContext;
using ImmobiliareApi.Entities;
using ImmobiliareApi.Interfaces.IRepositories;

namespace ImmobiliareApi.Services.Repositories
{
    
        public class BuildingRepository : GenericRepository<Building>, IBuildingRepository
        {
            public BuildingRepository(ImmobiliareApiContext dbContext) : base(dbContext)
            {

            }
        }
    
}
