using System;
using System.Collections.Generic;

namespace ApiTextilCorp.Data.Models;

public partial class Usuario
{
    public int UsuariosId { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? Correo { get; set; }

    public string? Clave { get; set; }

    public string? Direccion { get; set; }

    public int Telefono { get; set; }
}
