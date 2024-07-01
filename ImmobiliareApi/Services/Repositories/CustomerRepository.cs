using ImmobiliareApi.DataContext;
using ImmobiliareApi.Entities;
using ImmobiliareApi.Interfaces.IRepositories;

namespace ImmobiliareApi.Services.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ImmobiliareApiContext dbContext) : base(dbContext)
        {

        }
    }
}
