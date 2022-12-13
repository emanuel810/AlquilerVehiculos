using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlquilerVehiculo_Api.Entities;
using AlquilerVehiculo_Api.Repository;
using AlquilerVehiculo_Api.Dto;

namespace AlquilerVehiculo_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public VehiculoController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Vehiculo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehiculoEntity>>> GetVehiculo()
        {
            return await _context.Vehiculo.ToListAsync();
        }

        // GET: api/Vehiculo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehiculoEntity>> GetVehiculoEntity(int id)
        {
            var vehiculoEntity = await _context.Vehiculo.FindAsync(id);

            if (vehiculoEntity == null)
            {
                return NotFound();
            }

            return vehiculoEntity;
        }

        // PUT: api/Vehiculo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehiculoEntity(int id, VehiculoUpdateDto vehiculoUpdateDto)
        {
            var vehiculoEncontrado = await _context.Vehiculo.FindAsync(id);

            if (vehiculoEncontrado is null)
            {
                return NotFound("No se encontro el vehiculo");
            }

            VehiculoEntity vehiculoEntity = vehiculoUpdateDto.toDtoUpdate();

            vehiculoEntity.vehiculoNumero = id;

            _context.Entry(vehiculoEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiculoEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Vehiculo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VehiculoEntity>> PostVehiculoEntity(VehiculoAddDto vehiculoAddDto)
        {
            VehiculoEntity vehiculo = vehiculoAddDto.toDtoAdd();

            _context.Vehiculo.Add(vehiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehiculoEntity", new { id = vehiculo.vehiculoNumero }, vehiculo);
        }

        // DELETE: api/Vehiculo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiculoEntity(int id)
        {

            var vehiculoEntity = await _context.Vehiculo.FindAsync(id);

            var reservacions = await _context.Reservacion.ToListAsync();


            foreach (var reservacion in reservacions)
            {

                if (reservacion.vehiculoNumero== id)
                {
                    return NotFound("no se puede borrar ya que esta relacionado en la tabla reservacion");
                }
            }


            if (vehiculoEntity == null)
            {
                return NotFound();
            }

            _context.Vehiculo.Remove(vehiculoEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehiculoEntityExists(int id)
        {
            return _context.Vehiculo.Any(e => e.vehiculoNumero == id);
        }
    }
}
