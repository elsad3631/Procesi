using ImmobiliareApi.Entities;
using ImmobiliareApi.Models.CustomerModels;
using ImmobiliareApi.Models.ListViewModel;

namespace ImmobiliareApi.Interfaces
{
    public interface ICustomerServices
    {

        Task Create(CustomerCreateModel dto);
        Task<ListViewModel<CustomerSelectModel>> Get(int currentPage, string? filterRequest, char? fromName, char? toName);
        Task<CustomerSelectModel> Update(CustomerUpdateModel dto);
        Task<CustomerSelectModel> GetById(int id);
        Task<Customer> Delete(int id);
    }
}
