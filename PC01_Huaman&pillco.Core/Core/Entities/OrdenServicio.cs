using System;
using System.Collections.Generic;

namespace PC01_Huaman_pillco.Core.Core.Entities;

public partial class OrdenServicio
{
    public int Id { get; set; }

    public DateTime FechaIngreso { get; set; }

    public string DescripcionProblema { get; set; } = null!;

    public decimal CostoEstimado { get; set; }

    public string Estado { get; set; } = null!;

    public int VehiculoId { get; set; }

    public int TipoServicioId { get; set; }

    public virtual TipoServicio TipoServicio { get; set; } = null!;

    public virtual Vehiculo Vehiculo { get; set; } = null!;
}
