using System;
using System.Collections.Generic;

namespace ApiTextilCorp.Data.Models;

public partial class Carrito
{
    public int CarritoId { get; set; }

    public int Cantidad { get; set; }

    public int ClientesId { get; set; }

    public int ProductosId { get; set; }

    public virtual Cliente Clientes { get; set; } = null!;

    public virtual Producto Productos { get; set; } = null!;
}
