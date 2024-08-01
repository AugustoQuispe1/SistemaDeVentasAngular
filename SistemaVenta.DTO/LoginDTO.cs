using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    // se van a recibir las credenciales para poder acceder
    public class LoginDTO
    {
        public string Correo { get; set; }

        public string Clave { get; set; }

    }
}
