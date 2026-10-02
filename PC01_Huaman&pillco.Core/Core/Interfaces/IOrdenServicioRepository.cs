using PC01_Huaman_pillco.Core.Core.Entities;

namespace PC01_Huaman_pillco.Core.Core.Interfaces
{
    public interface IOrdenServicioRepository
    {
        Task<int> CreateOrdenServicio(OrdenServicio ordenServicio);
        Task<bool> DeleteOrdenServicio(int id);
        Task<IEnumerable<OrdenServicio>> GetOrdenesServicio();
        Task<OrdenServicio?> GetOrdenServicioById(int id);
        Task<bool> UpdateOrdenServicio(OrdenServicio ordenServicio);
        Task<bool> VehiculoExiste(int vehiculoId);
        Task<bool> TipoServicioExiste(int tipoServicioId);
    }
}