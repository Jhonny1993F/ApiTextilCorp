using System;
using System.Collections.Generic;

namespace ApiTextilCorp.Data.Models;

public partial class Categoria
{
    public int CategoriasId { get; set; }

    public int Cantidad { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
