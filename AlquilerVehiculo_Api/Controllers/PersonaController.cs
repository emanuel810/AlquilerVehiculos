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
using Microsoft.AspNetCore.Components.Forms;

namespace AlquilerVehiculo_Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public PersonaController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Persona
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonaEntity>>> GetPersona()
        {
            return await _context.Persona.ToListAsync();
        }

        // GET: api/Persona/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaEntity>> GetPersonaEntity(int id)
        {
            var personaEntity = await _context.Persona.FindAsync(id);

            if (personaEntity == null)
            {
                return NotFound();
            }

            return personaEntity;
        }

        // PUT: api/Persona/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonaEntity(int id, PersonaDto personaDto)
        {

            var personaEncontrada = await _context.Persona.FindAsync(id);

            if (personaEncontrada is null)
            {
                return NotFound("No se encontro la persona");
            }

            PersonaEntity persona = personaDto.ToDto();

            persona.personaNumero = id;
            _context.Entry(persona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaEntityExists(id))
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

        // POST: api/Persona
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PersonaEntity>> PostPersonaEntity(PersonaDto personaDto)
        {
            PersonaEntity persona = personaDto.ToDto();
            _context.Persona.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonaEntity", new { id = persona.personaNumero }, persona);
        }

        // DELETE: api/Persona/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonaEntity(int id)
        {
            var personaEntity = await _context.Persona.FindAsync(id);

            var clientes = await _context.Cliente.ToListAsync();


            foreach (var cliente in clientes) {

                if (cliente.personaNumero == id) {
                    return NotFound("no se puede borrar ya que esta relacionado en la tabla cliente");
                }
            }

            if (personaEntity == null)
            {
                return NotFound();
            }

            _context.Persona.Remove(personaEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonaEntityExists(int id)
        {
            return _context.Persona.Any(e => e.personaNumero == id);
        }
    }
}
