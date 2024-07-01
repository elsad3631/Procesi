using ImmobiliareApi.DataContext;
using ImmobiliareApi.Interfaces.IBusinessServices;
using ImmobiliareApi.Interfaces.IRepositories;
using ImmobiliareApi.Interfaces.IRepositories.ITypologiesRepositories;
using System;

namespace ImmobiliareApi.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ImmobiliareApiContext dbContext { get; }
        ICustomerRepository CustomerRepository { get; }
        IBuildingRepository BuildingRepository { get; }
        IBuildingTypeRepository BuildingTypeRepository { get; }
        
        Task<int> SaveAsync();

        int Save();
    }
}
