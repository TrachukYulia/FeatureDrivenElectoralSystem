using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Extension
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<DataContext>(options =>
               options.UseNpgsql(
               configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("FeatureDrivenElectoralSystemApi"))
               );
        }
    }
}
