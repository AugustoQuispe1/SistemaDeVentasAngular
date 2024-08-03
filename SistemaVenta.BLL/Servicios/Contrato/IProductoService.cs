using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IProductoService
    {
        //se va listar
        Task<List<ProductoDTO>> Lista();

        //creacion
        Task<ProductoDTO> Crear(ProductoDTO modelo);

        //editar
        Task<bool> Editar(ProductoDTO modelo);

        //eliminar
        Task<bool> Eliminar(int id);



    }
}
