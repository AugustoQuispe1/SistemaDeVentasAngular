using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.Datos.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _usuarioReposito;
        private readonly IMapper _mapper;

        public UsuarioService(IGenericRepository<Usuario> usuarioReposito, IMapper mapper)
        {
            _usuarioReposito = usuarioReposito;
            _mapper = mapper;
        }


        async Task<List<UsuarioDTO>> IUsuarioService.Lista()
              { 
            try
            {
                var queryUsuario = await _usuarioReposito.Consultar();
                var listaUsuario = queryUsuario.Include(rol => rol.IdRolNavigation).ToList();
                return _mapper.Map<List<UsuarioDTO>>(listaUsuario);
            }
            catch
            {
                throw;
            }
              }

         async Task<SesionDTO> IUsuarioService.ValidarCredenciales(string correo, string clave)
         {
            try
            {
                var queryUsusario = await _usuarioReposito.Consultar(u =>
                u.Correo == correo &&
                u.Clave == clave
                );

                if(queryUsusario.FirstOrDefault() == null)
                    throw new TaskCanceledException("El usuario no existe");

                Usuario devolverUsuario = queryUsusario.Include(rol => rol.IdRolNavigation).First();

                return _mapper.Map<SesionDTO>(devolverUsuario);

            }
            catch 
            {
                throw;
            }
         }


        async Task<UsuarioDTO> IUsuarioService.Crear(UsuarioDTO modelo)
        {
            try
            {
                var usuarioCreado = await _usuarioReposito.Crear(_mapper.Map<Usuario>(modelo));

                if (usuarioCreado.IdUsuario == 0)
                    throw new TaskCanceledException("El usuario no se pudo crear");

                var query = await _usuarioReposito.Consultar(u => u.IdUsuario == usuarioCreado.IdUsuario);
                usuarioCreado = query.Include(rol =>rol.IdRolNavigation).First();
                return _mapper.Map<UsuarioDTO>(usuarioCreado);
            }
            catch 
            {
                throw;
            }
        }

        async Task<bool> IUsuarioService.Editar(UsuarioDTO modelo)
        {
            try
            {
                var usuarioModelo = _mapper.Map<Usuario>(modelo);
                var usuarioEncontrado = await _usuarioReposito.Obtener(u => u.IdUsuario == usuarioModelo.IdUsuario);

                if (usuarioEncontrado == null)
                    throw new TaskCanceledException("No existe el usuario");

                usuarioEncontrado.NombreCompleto = usuarioModelo.NombreCompleto;
                usuarioEncontrado.Correo = usuarioModelo.Correo;
                usuarioEncontrado.IdRol = usuarioModelo.IdRol;
                usuarioEncontrado.Clave = usuarioModelo.Clave;
                usuarioEncontrado.EsActivo = usuarioModelo.EsActivo;

                bool respuesta = await _usuarioReposito.Editar(usuarioEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar el usuario");
                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        async Task<bool>IUsuarioService.Eliminar(int id)
        {
            try
            {
                //buscamos el usuario
                var usuarioEncontrado =  _usuarioReposito.Obtener(u => u.IdUsuario == id);

                //validacion si es que encontro el usuario
                if (usuarioEncontrado == null)
                    throw new TaskCanceledException("El usuario no existe");

                bool respuesta = await _usuarioReposito.Eliminar(await usuarioEncontrado); //awit de usuarioencontrado 

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo eliminar el usuario");
                return respuesta;
            }
            catch
            {
                throw;
            }
        }

 


    }
}
