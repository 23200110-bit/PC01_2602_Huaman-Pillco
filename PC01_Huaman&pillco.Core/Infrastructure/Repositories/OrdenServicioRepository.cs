using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PC01_Huaman_pillco.Core.Core.Entities;
using PC01_Huaman_pillco.Core.Core.Interfaces;
using PC01_Huaman_pillco.Core.Infrastructure.Data;

namespace PC01_Huaman_pillco.Core.Infrastructure.Repositories
{
    public class OrdenServicioRepository : IOrdenServicioRepository
    {
        private readonly TallerDbContext _dbContext;
        private readonly ILogger<OrdenServicioRepository> _logger;

        public OrdenServicioRepository(TallerDbContext dbContext, ILogger<OrdenServicioRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<OrdenServicio>> GetOrdenesServicio()
        {
            try
            {
                return await _dbContext.OrdenServicios
                    .Include(o => o.Vehiculo)
                    .Include(o => o.TipoServicio)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las órdenes de servicio");
                throw;
            }
        }

        public async Task<OrdenServicio?> GetOrdenServicioById(int id)
        {
            try
            {
                return await _dbContext.OrdenServicios
                    .Include(o => o.Vehiculo)
                    .Include(o => o.TipoServicio)
                    .FirstOrDefaultAsync(o => o.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener orden de servicio con id {Id}", id);
                throw;
            }
        }

        public async Task<int> CreateOrdenServicio(OrdenServicio ordenServicio)
        {
            try
            {
                await _dbContext.OrdenServicios.AddAsync(ordenServicio);
                var rows = await _dbContext.SaveChangesAsync();
                return rows > 0 ? ordenServicio.Id : 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error de base de datos al crear orden de servicio");
                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear orden de servicio");
                throw;
            }
        }

        public async Task<bool> UpdateOrdenServicio(OrdenServicio ordenServicio)
        {
            try
            {
                var existente = await _dbContext.OrdenServicios
                    .FirstOrDefaultAsync(o => o.Id == ordenServicio.Id);
                if (existente == null)
                {
                    _logger.LogWarning("Intento de actualizar orden de servicio inexistente con id {Id}", ordenServicio.Id);
                    return false;
                }

                existente.DescripcionProblema = ordenServicio.DescripcionProblema;
                existente.CostoEstimado = ordenServicio.CostoEstimado;
                existente.Estado = ordenServicio.Estado;
                existente.VehiculoId = ordenServicio.VehiculoId;
                existente.TipoServicioId = ordenServicio.TipoServicioId;

                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error de base de datos al actualizar orden de servicio {Id}", ordenServicio.Id);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar orden de servicio {Id}", ordenServicio.Id);
                throw;
            }
        }

        public async Task<bool> DeleteOrdenServicio(int id)
        {
            try
            {
                var existente = await _dbContext.OrdenServicios.FirstOrDefaultAsync(o => o.Id == id);
                if (existente == null)
                {
                    _logger.LogWarning("Intento de eliminar orden de servicio inexistente con id {Id}", id);
                    return false;
                }

                _dbContext.OrdenServicios.Remove(existente);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error de base de datos al eliminar orden de servicio {Id}", id);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al eliminar orden de servicio {Id}", id);
                throw;
            }
        }

        public async Task<bool> VehiculoExiste(int vehiculoId)
        {
            try
            {
                return await _dbContext.Vehiculos.AnyAsync(v => v.Id == vehiculoId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar existencia de vehículo {VehiculoId}", vehiculoId);
                throw;
            }
        }

        public async Task<bool> TipoServicioExiste(int tipoServicioId)
        {
            try
            {
                return await _dbContext.TipoServicios.AnyAsync(ts => ts.Id == tipoServicioId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar existencia de tipo de servicio {TipoServicioId}", tipoServicioId);
                throw;
            }
        }
    }
}