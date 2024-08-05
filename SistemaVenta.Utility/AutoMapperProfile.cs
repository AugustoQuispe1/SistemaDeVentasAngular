using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SistemaVenta.DTO;
using SistemaVenta.Entity;

namespace SistemaVenta.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            #region Rol
            //origen (rol) y destino (RolDTO)
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion

            #region Menu

            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion

            #region Usuario
            //especificaremos como va ser la conversion al destino
            CreateMap<Usuario, UsuarioDTO>()
            .ForMember(destino =>
              destino.RolDescripcion,
               opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)) //se convertiran los datos hacia el destino
            .ForMember(destino =>
              destino.EsActivo,
              opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));


            CreateMap<Usuario, SesionDTO>()
              .ForMember(destino =>
                destino.RolDescripcion,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                );

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino =>
                destino.IdRolNavigation,
                opt => opt.Ignore()
                    )
                .ForMember(destino =>
                  destino.EsActivo,
                  opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false));




            #endregion

            #region Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            #endregion

            #region Producto
            CreateMap<Producto, ProductoDTO>()
                .ForMember(destino =>
                destino.DescripcionCategoria,
                opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Nombre)
                )
                .ForMember(destino =>
                destino.Precio, //lo convertimos de decimal a string
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-ARG")))
                )
                .ForMember(destino =>
                 destino.EsActivo,
                 opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));

            //reverse hehe
            CreateMap<ProductoDTO, Producto>()
                .ForMember(destino =>
                destino.IdCategoriaNavigation,
                opt => opt.Ignore()
                )
                .ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-ARG")))
                )
                .ForMember(destino =>
                 destino.EsActivo,
                 opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)); //ojo con este
            #endregion

            #region Venta
            CreateMap<Venta, VentaDTO>()
            .ForMember(destino =>
                destino.TotalTexto,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-ARG")))
                )
            .ForMember(destino =>
                destino.FechaRegistro,
                opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                );

            CreateMap<VentaDTO, Venta>()
                .ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-ARG")))
                );

            #endregion

            #region DetalleVenta
            CreateMap<DetalleVenta, DetalleVentaDTO>()
              .ForMember(destino =>
               destino.DescripcionProducto,
                opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
                )
              .ForMember(destino =>
                destino.PrecioTexto,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-ARG")))
                )
              .ForMember(destino =>
                destino.TotalTexto,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-ARG")))
                );

            CreateMap<DetalleVentaDTO, DetalleVenta>()
                .ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToString(origen.PrecioTexto, new CultureInfo("es-ARG")))
                )
                .ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToString(origen.TotalTexto, new CultureInfo("es-ARG")))
                );






            #endregion

            #region Reporte
            CreateMap<DetalleVenta, ReporteDTO>()
                .ForMember(destino =>
                destino.FechaRegistro,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destino =>
                destino.FechaRegistro,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation.NumeroDocumento)
                )
                .ForMember(destino =>
                destino.TipoPago,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation.TipoPago)
                )
                .ForMember(destino =>
                destino.TotalVenta,
                opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-ARG")))
                )
                .ForMember(destino =>
                destino.Producto,
                opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
                )
                .ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-ARG")))
                )
                .ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-ARG")))
                );

            #endregion
        }
    }
}
