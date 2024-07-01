using ImmobiliareApi.DataContext;
using Microsoft.EntityFrameworkCore;
using System;

namespace ImmobiliareApi.Services
{
    public static class DatabaseStartup
    {
        public static readonly ILoggerFactory ConsoleLogFactory
                        = LoggerFactory.Create(builder => { builder.AddConsole(); });
        /// <summary>
        /// It is used to configure the connection properties for the database
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        public static void ConfigureDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ImmobiliareApiContext>(options =>
            {
                options.UseSqlServer(
                    "name=ConnectionStrings:DefaultConnection",
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);

                    });
#if DEBUG
                options.UseLoggerFactory(ConsoleLogFactory);
                options.EnableSensitiveDataLogging();
#endif
            }

        );
        }
    }
}

