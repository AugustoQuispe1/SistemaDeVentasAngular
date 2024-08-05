using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVenta.Datos.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.Datos.Repositorios.Contrato;
using SistemaVenta.Datos.Repositorios;
using SistemaVenta.Utility;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.BLL.Servicios;


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

            //utilizamos el modelo generico (se utiliza en cualquier modelo)
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //aca espcificaremos en que modelo
            services.AddScoped<IVentaRepository, VentaRepository>();



            //automapper
            services.AddAutoMapper(typeof(AutoMapperProfile));

            //dependecias de las interfaces

            services.AddScoped<DbContext, DbventaContext>();


            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IVentaService, VentaService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<IMenuService, MenuService>();




        }

    }
}
