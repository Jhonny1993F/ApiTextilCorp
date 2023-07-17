using System;
using System.Collections.Generic;

namespace ApiTextilCorp.Data.Models;

public partial class Cliente
{
    public int ClientesId { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? Correo { get; set; }

    public string? Clave { get; set; }

    public string? Direccion { get; set; }

    public int Telefono { get; set; }

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
