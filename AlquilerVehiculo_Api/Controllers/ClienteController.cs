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
    [Route("/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public ClienteController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteEntity>>> GetCliente()
        {
            return await _context.Cliente.ToListAsync();
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteEntity>> GetClienteEntity(int id)
        {
            var clienteEntity = await _context.Cliente.FindAsync(id);

            if (clienteEntity == null)
            {
                return NotFound();
            }

            return clienteEntity;
        }

        // POST: api/Cliente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClienteEntity>> PostClienteEntity(ClienteDto clienteDto)
        {

            var personaEntity = await _context.Persona.FindAsync(clienteDto.personaNumero);

            if (personaEntity is null) {
                return NotFound("No se encontro la persona");
            }

            ClienteEntity clienteEntity= clienteDto.toDto();

            _context.Cliente.Add(clienteEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClienteEntity", new { id = clienteEntity.clienteNumero }, clienteEntity);
        }

        // DELETE: api/Cliente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClienteEntity(int id)
        {
            var clienteEntity = await _context.Cliente.FindAsync(id);
            var reservaciones = await _context.Reservacion.ToListAsync();

            foreach (var reservacion in reservaciones)
            {

                if (reservacion.clienteNumero == id)
                {
                    return NotFound("no se puede borrar ya que esta relacionado en la tabla cliente");
                }
            }

            if (clienteEntity == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(clienteEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteEntityExists(int id)
        {
            return _context.Cliente.Any(e => e.clienteNumero == id);
        }
    }
}
