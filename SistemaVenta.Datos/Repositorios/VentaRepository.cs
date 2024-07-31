using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.Datos.DBContext;
using SistemaVenta.Datos.Repositorios.Contrato;
using SistemaVenta.Entity;

namespace SistemaVenta.Datos.Repositorios
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {

        private readonly DbventaContext _dbContext;

        public VentaRepository(DbventaContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        //implementacion de venta
        public async Task<Venta> Registrar(Venta modelo)
        {
            
            Venta VentaGenerada = new Venta();

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {

                    foreach(DetalleVenta dv in modelo.DetalleVenta)
                    {
                        Producto producto_encontrado = _dbContext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();

                        //restar productos del stock
                        producto_encontrado.Stock = producto_encontrado.Stock - dv.Cantidad;
                        _dbContext.Productos.Update(producto_encontrado);
                    }
                    //guardar cambios
                    await _dbContext.SaveChangesAsync();

                    //se registrara y actualizara el registro
                    NumeroDocumento correlaitvo = _dbContext.NumeroDocumentos.First();
                    correlaitvo.UltimoNumero = correlaitvo.UltimoNumero + 1;
                    correlaitvo.FechaRegistro = DateTime.Now;

                    _dbContext.NumeroDocumentos.Update(correlaitvo);
                    await _dbContext.SaveChangesAsync();

                    //generar formato del numero del documento
                    int CantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string numeroVenta = ceros + correlaitvo.UltimoNumero.ToString();

                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos,CantidadDigitos);

                    modelo.NumeroDocumento = numeroVenta;
                    await _dbContext.Venta.AddAsync(modelo);
                    await _dbContext.SaveChangesAsync();

                    VentaGenerada = modelo;

                    transaction.Commit();


                }
                catch
                {
                    //si existe un error anteriormente se restablecera
                    transaction.Rollback();
                    throw;
                }

                return VentaGenerada;
            }

        }
    }
}
