using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;
using System.Linq.Expressions;
using AutoMapper;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.Datos.Repositorios.Contrato;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IUsuarioService
    {
        //se va listar
        Task<List<UsuarioDTO>> Lista();
        
        //validacion
        Task<SesionDTO> ValidarCredenciales(string correo,string clave);

        //creacion
        Task<UsuarioDTO> Crear(UsuarioDTO modelo);

        //editar
        Task<bool>Editar(UsuarioDTO modelo);

        //eliminar
        Task<bool>Eliminar(int id);


    }
}
