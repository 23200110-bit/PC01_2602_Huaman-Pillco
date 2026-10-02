using System;
using System.Collections.Generic;

namespace PC01_Huaman_pillco.Core.Core.Entities;

public partial class TipoServicio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal PrecioBase { get; set; }

    public virtual ICollection<OrdenServicio> OrdenServicios { get; set; } = new List<OrdenServicio>();
}
