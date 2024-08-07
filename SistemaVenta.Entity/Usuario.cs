﻿using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? NombreCompleto { get; set; }

    public string? Correo { get; set; }

    public int? IdRol { get; set; }

    public string? Clave { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }


    //sirve para poder relacionar el usuario con el rol (cray cray)
    public virtual Rol? IdRolNavigation { get; set; }
}
