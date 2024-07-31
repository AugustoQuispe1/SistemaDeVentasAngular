using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

//controladores jeje

namespace SistemaVenta.Datos.Repositorios.Contrato
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        //buscar
        Task<TModel> Obtener(Expression<Func<TModel, bool>> filtro);
        //crear
        Task<TModel> Crear(TModel modelo);
        //Editar
        Task<bool> Editar(TModel modelo);
        //eliminar
        Task<bool> Eliminar(TModel modelo);


        Task<IQueryable<TModel>> Consultar(Expression<Func<TModel, bool>> filtro = null);
    }
}
