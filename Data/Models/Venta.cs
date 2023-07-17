using System;
using System.Collections.Generic;

namespace ApiTextilCorp.Data.Models;

public partial class Venta
{
    public int VentasId { get; set; }

    public float MontoTotal { get; set; }

    public int Telefono { get; set; }

    public string? Direccion { get; set; }

    public int ClientesId { get; set; }

    public int ProductosId { get; set; }

    public virtual Cliente Clientes { get; set; } = null!;

    public virtual Producto Productos { get; set; } = null!;
}
