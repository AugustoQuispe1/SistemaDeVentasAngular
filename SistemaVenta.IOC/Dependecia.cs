using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVenta.Datos.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.IOC
{
    public static class Dependecia
    {
        //agregar dependecias con un servicio de ASP.NET (metodo de extension)
        public static void InyectarDependecias(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<DbventaContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CadenaSQL"));
            });
        }

    }
}
