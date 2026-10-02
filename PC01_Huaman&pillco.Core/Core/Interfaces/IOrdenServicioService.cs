using PC01_Huaman_pillco.Core.Core.DTOs;

namespace PC01_Huaman_pillco.Core.Core.Interfaces
{
    public interface IOrdenServicioService
    {
        Task<int> CreateOrdenServicio(OrdenServicioCreateDTO dto);
        Task<bool> DeleteOrdenServicio(int id);
        Task<IEnumerable<OrdenServicioListDTO>> GetOrdenesServicio();
        Task<OrdenServicioDetailDTO?> GetOrdenServicioById(int id);
        Task<bool> UpdateOrdenServicio(OrdenServicioUpdateDTO dto);
    }
}