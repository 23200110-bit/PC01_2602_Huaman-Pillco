using Microsoft.Extensions.Logging;
using PC01_Huaman_pillco.Core.Core.DTOs;
using PC01_Huaman_pillco.Core.Core.Entities;
using PC01_Huaman_pillco.Core.Core.Interfaces;

namespace PC01_Huaman_pillco.Core.Core.Services
{
    public class OrdenServicioService : IOrdenServicioService
    {
        private static readonly string[] EstadosValidos = { "Pendiente", "EnProceso", "Finalizado", "Cancelado" };
        private readonly IOrdenServicioRepository _repository;
        private readonly ILogger<OrdenServicioService> _logger;

        public OrdenServicioService(IOrdenServicioRepository repository, ILogger<OrdenServicioService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<OrdenServicioListDTO>> GetOrdenesServicio()
        {
            try
            {
                var ordenes = await _repository.GetOrdenesServicio();
                return ordenes.Select(o => MapToListDTO(o)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las órdenes de servicio");
                throw;
            }
        }

        public async Task<OrdenServicioDetailDTO?> GetOrdenServicioById(int id)
        {
            try
            {
                var orden = await _repository.GetOrdenServicioById(id);
                if (orden == null)
                {
                    _logger.LogInformation("Orden de servicio con id {Id} no encontrada", id);
                    return null;
                }
                return MapToDetailDTO(orden);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener orden de servicio con id {Id}", id);
                throw;
            }
        }

        public async Task<int> CreateOrdenServicio(OrdenServicioCreateDTO dto)
        {
            try
            {
                if (!DatosValidos(dto.DescripcionProblema, dto.CostoEstimado, dto.Estado ?? "Pendiente"))
                {
                    _logger.LogWarning("Datos inválidos para crear orden: Descripción={Desc}, Costo={Costo}, Estado={Estado}",
                        dto.DescripcionProblema, dto.CostoEstimado, dto.Estado);
                    return 0;
                }

                var vehiculoExiste = await _repository.VehiculoExiste(dto.VehiculoId);
                if (!vehiculoExiste)
                {
                    _logger.LogWarning("Vehículo con id {VehiculoId} no existe", dto.VehiculoId);
                    return 0;
                }

                var tipoServicioExiste = await _repository.TipoServicioExiste(dto.TipoServicioId);
                if (!tipoServicioExiste)
                {
                    _logger.LogWarning("Tipo de servicio con id {TipoServicioId} no existe", dto.TipoServicioId);
                    return 0;
                }

                var estado = string.IsNullOrWhiteSpace(dto.Estado) ? "Pendiente" : dto.Estado;
                var orden = new OrdenServicio
                {
                    FechaIngreso = DateTime.Now,
                    DescripcionProblema = dto.DescripcionProblema,
                    CostoEstimado = dto.CostoEstimado,
                    Estado = estado,
                    VehiculoId = dto.VehiculoId,
                    TipoServicioId = dto.TipoServicioId
                };

                var id = await _repository.CreateOrdenServicio(orden);
                if (id > 0)
                {
                    _logger.LogInformation("Orden de servicio creada con éxito: id={Id}", id);
                }
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear orden de servicio");
                throw;
            }
        }

        public async Task<bool> UpdateOrdenServicio(OrdenServicioUpdateDTO dto)
        {
            try
            {
                if (!DatosValidos(dto.DescripcionProblema, dto.CostoEstimado, dto.Estado))
                {
                    _logger.LogWarning("Datos inválidos para actualizar orden {Id}", dto.Id);
                    return false;
                }

                var vehiculoExiste = await _repository.VehiculoExiste(dto.VehiculoId);
                if (!vehiculoExiste)
                {
                    _logger.LogWarning("Vehículo con id {VehiculoId} no existe para actualizar orden {Id}", dto.VehiculoId, dto.Id);
                    return false;
                }

                var tipoServicioExiste = await _repository.TipoServicioExiste(dto.TipoServicioId);
                if (!tipoServicioExiste)
                {
                    _logger.LogWarning("Tipo de servicio con id {TipoServicioId} no existe para actualizar orden {Id}", dto.TipoServicioId, dto.Id);
                    return false;
                }

                var orden = new OrdenServicio
                {
                    Id = dto.Id,
                    DescripcionProblema = dto.DescripcionProblema,
                    CostoEstimado = dto.CostoEstimado,
                    Estado = dto.Estado,
                    VehiculoId = dto.VehiculoId,
                    TipoServicioId = dto.TipoServicioId
                };

                var resultado = await _repository.UpdateOrdenServicio(orden);
                if (resultado)
                {
                    _logger.LogInformation("Orden de servicio actualizada con éxito: id={Id}", dto.Id);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar orden de servicio {Id}", dto.Id);
                throw;
            }
        }

        public async Task<bool> DeleteOrdenServicio(int id)
        {
            try
            {
                var resultado = await _repository.DeleteOrdenServicio(id);
                if (resultado)
                {
                    _logger.LogInformation("Orden de servicio eliminada con éxito: id={Id}", id);
                }
                else
                {
                    _logger.LogWarning("Intento de eliminar orden de servicio inexistente: id={Id}", id);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al eliminar orden de servicio {Id}", id);
                throw;
            }
        }

        private static bool DatosValidos(string descripcion, decimal costo, string estado)
        {
            return !string.IsNullOrWhiteSpace(descripcion)
                && descripcion.Length <= 1000
                && costo >= 0
                && EstadosValidos.Contains(estado);
        }

        private static OrdenServicioListDTO MapToListDTO(OrdenServicio orden)
        {
            return new OrdenServicioListDTO
            {
                Id = orden.Id,
                FechaIngreso = orden.FechaIngreso,
                DescripcionProblema = orden.DescripcionProblema,
                CostoEstimado = orden.CostoEstimado,
                Estado = orden.Estado,
                Placa = orden.Vehiculo?.Placa ?? string.Empty,
                TipoServicioNombre = orden.TipoServicio?.Nombre ?? string.Empty
            };
        }

        private static OrdenServicioDetailDTO MapToDetailDTO(OrdenServicio orden)
        {
            return new OrdenServicioDetailDTO
            {
                Id = orden.Id,
                FechaIngreso = orden.FechaIngreso,
                DescripcionProblema = orden.DescripcionProblema,
                CostoEstimado = orden.CostoEstimado,
                Estado = orden.Estado,
                VehiculoId = orden.VehiculoId,
                Placa = orden.Vehiculo?.Placa ?? string.Empty,
                TipoServicioId = orden.TipoServicioId,
                TipoServicioNombre = orden.TipoServicio?.Nombre ?? string.Empty
            };
        }
    }
}