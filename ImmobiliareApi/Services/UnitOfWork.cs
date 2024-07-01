using ImmobiliareApi.DataContext;
using ImmobiliareApi.Interfaces;
using ImmobiliareApi.Interfaces.IBusinessServices;
using ImmobiliareApi.Interfaces.IRepositories;
using ImmobiliareApi.Interfaces.IRepositories.ITypologiesRepositories;
using ImmobiliareApi.Services.Repositories;
using ImmobiliareApi.Services.Repositories.TypologiesRepositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace ImmobiliareApi.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ImmobiliareApiContext _dbContext;
        public ImmobiliareApiContext dbContext { get; set; }

        public UnitOfWork(ImmobiliareApiContext context)
        {
            this._dbContext = context;
            this.dbContext = context;

            CustomerRepository = new CustomerRepository(this._dbContext);
            BuildingRepository = new BuildingRepository(this._dbContext);
            BuildingTypeRepository = new BuildingTypeRepository(this._dbContext);


        }

        public ICustomerRepository CustomerRepository
        {
            get;
            private set;
        }
        public IBuildingRepository BuildingRepository
        {
            get;
            private set;
        }
        public IBuildingTypeRepository BuildingTypeRepository
        {
            get;
            private set;
        }


        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();

        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }


    }
}
