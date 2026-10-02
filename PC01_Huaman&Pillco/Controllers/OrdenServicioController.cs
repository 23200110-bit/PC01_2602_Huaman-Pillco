using Microsoft.AspNetCore.Mvc;
using PC01_Huaman_pillco.Core.Core.DTOs;
using PC01_Huaman_pillco.Core.Core.Interfaces;

namespace PC01_Huaman_pillco.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenServicioController : ControllerBase
    {
        private readonly IOrdenServicioService _service;

        public OrdenServicioController(IOrdenServicioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdenesServicio()
        {
            var ordenes = await _service.GetOrdenesServicio();
            return Ok(ordenes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdenServicioById(int id)
        {
            var orden = await _service.GetOrdenServicioById(id);
            if (orden == null) return NotFound();
            return Ok(orden);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrdenServicio([FromBody] OrdenServicioCreateDTO dto)
        {
            var id = await _service.CreateOrdenServicio(dto);
            if (id == 0) return BadRequest("Datos inválidos: revise estado, costo, vehículo y tipo de servicio.");
            return CreatedAtAction(nameof(GetOrdenServicioById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrdenServicio(int id, [FromBody] OrdenServicioUpdateDTO dto)
        {
            if (id != dto.Id) return BadRequest("El id de la URL no coincide con el del cuerpo.");

            var existente = await _service.GetOrdenServicioById(id);
            if (existente == null) return NotFound();

            var ok = await _service.UpdateOrdenServicio(dto);
            if (!ok) return BadRequest("Datos inválidos.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdenServicio(int id)
        {
            var ok = await _service.DeleteOrdenServicio(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}