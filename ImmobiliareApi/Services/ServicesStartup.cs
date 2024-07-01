using ImmobiliareApi.Interfaces;
using ImmobiliareApi.Interfaces.IBusinessServices;
using ImmobiliareApi.Interfaces.IBusinessServices.ITypologiesServices;
using ImmobiliareApi.Services.BusinessServices;
using ImmobiliareApi.Services.TypologiesServices;

namespace ImmobiliareApi.Services
{
    public static class ServicesStartup
    {

        public static void ConfigureServices(this WebApplicationBuilder builder)
        {

            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<ICustomerServices, CustomerServices>();
            builder.Services.AddTransient<IBuildingServices, BuildingServices>();
            builder.Services.AddTransient<IBuildingTypeServices, BuildingTypeServices>();
            builder.Services.AddTransient<IEmailService, EmailService>();
        }
    }
}
