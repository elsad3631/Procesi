using ImmobiliareApi.Entities;
using ImmobiliareApi.Models.CustomerModels;

namespace ImmobiliareApi.Profiles
{
    public class CustomerProfile : AutoMapper.Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerCreateModel>();
            CreateMap<Customer, CustomerUpdateModel>();
            CreateMap<Customer, CustomerSelectModel>();
            CreateMap<CustomerSelectModel, CustomerUpdateModel>();
            CreateMap<CustomerUpdateModel, CustomerSelectModel>();

            CreateMap<CustomerCreateModel, Customer>();
            CreateMap<CustomerUpdateModel, Customer>();
            CreateMap<CustomerSelectModel, Customer>();

        }
    }
}
