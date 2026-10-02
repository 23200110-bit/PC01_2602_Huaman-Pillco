namespace PC01_Huaman_pillco.Core.Core.DTOs
{
    public class OrdenServicioListDTO
    {
        public int Id { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string DescripcionProblema { get; set; } = string.Empty;
        public decimal CostoEstimado { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public string TipoServicioNombre { get; set; } = string.Empty;
    }

    public class OrdenServicioDetailDTO
    {
        public int Id { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string DescripcionProblema { get; set; } = string.Empty;
        public decimal CostoEstimado { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int VehiculoId { get; set; }
        public string Placa { get; set; } = string.Empty;
        public int TipoServicioId { get; set; }
        public string TipoServicioNombre { get; set; } = string.Empty;
    }

    public class OrdenServicioCreateDTO
    {
        public string DescripcionProblema { get; set; } = string.Empty;
        public decimal CostoEstimado { get; set; }
        public string? Estado { get; set; }
        public int VehiculoId { get; set; }
        public int TipoServicioId { get; set; }
    }

    public class OrdenServicioUpdateDTO
    {
        public int Id { get; set; }
        public string DescripcionProblema { get; set; } = string.Empty;
        public decimal CostoEstimado { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int VehiculoId { get; set; }
        public int TipoServicioId { get; set; }
    }
}