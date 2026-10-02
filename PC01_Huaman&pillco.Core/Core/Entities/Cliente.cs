using System;
using System.Collections.Generic;

namespace PC01_Huaman_pillco.Core.Core.Entities;

public partial class Cliente
{
    public int Id { get; set; }

    public string Paterno { get; set; } = null!;

    public string Materno { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
