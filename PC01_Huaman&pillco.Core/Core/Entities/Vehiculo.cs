using System;
using System.Collections.Generic;

namespace PC01_Huaman_pillco.Core.Core.Entities;

public partial class Vehiculo
{
    public int Id { get; set; }

    public string Placa { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public int Anio { get; set; }

    public int ClienteId { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<OrdenServicio> OrdenServicios { get; set; } = new List<OrdenServicio>();
}
