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
    public class ReservacionController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public ReservacionController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Reservacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservacionEntity>>> GetReservacion()
        {
            return await _context.Reservacion.ToListAsync();
        }

        // GET: api/Reservacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservacionEntity>> GetReservacionEntity(int id)
        {
            var reservacionEntity = await _context.Reservacion.FindAsync(id);

            if (reservacionEntity == null)
            {
                return NotFound();
            }

            return reservacionEntity;
        }

        // PUT: api/Reservacion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservacionEntity(int id, ReservacionUpdateDto reservacionUpdateDto)
        {
            var reservacionEncontrada = await _context.Reservacion.FindAsync(id);

            if (reservacionEncontrada is null) {
                return NotFound("No se encontro la reservacion");
            }

            ReservacionEntity reservacionEntity = reservacionUpdateDto.toUpdateDto();
            reservacionEntity.reservacionNumero = id;
            reservacionEntity.clienteNumero = reservacionEncontrada.clienteNumero;

            var reservaciones = await _context.Reservacion.ToListAsync();

            foreach (var reservacion in reservaciones)
            {

                if (reservacion.fechaReservacion == reservacionEntity.fechaReservacion && reservacion.clienteNumero == reservacionEntity.clienteNumero)
                {
                    return NotFound("No puede reservar otro auto el mismo dia");
                }
            }
            var vehiculo = await _context.Vehiculo.FindAsync(reservacionEntity.vehiculoNumero);
            DateTime diaInicio = reservacionEntity.fechaReservacion;
            DateTime diaFin = reservacionEntity.fechaDevolucion;

            TimeSpan diferencia = diaFin - diaInicio;
            int dias = diferencia.Days;

            reservacionEntity.costoReservacion = vehiculo.precioAlquiler * dias;

            _context.Entry(reservacionEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservacionEntityExists(id))
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

        // POST: api/Reservacion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReservacionEntity>> PostReservacionEntity(ReservacionAddDto reservacionAddDto)
        {
            ReservacionEntity reservacionEntity = reservacionAddDto.toAddDto();

            var reservaciones = await _context.Reservacion.ToListAsync();
            var vehiculos = await _context.Vehiculo.ToListAsync();
            var clientes = await _context.Cliente.ToListAsync();

            foreach (var vehic in vehiculos) {

                if (vehic.vehiculoNumero == reservacionEntity.vehiculoNumero) {
                    return NotFound("No se encontro el vehiculo");
                }
            
            }

            foreach (var cliente in clientes)
            {

                if (cliente.clienteNumero == reservacionEntity.clienteNumero)
                {
                    return NotFound("No se encontro la persona");
                }

            }

            foreach (var reservacion in reservaciones) {

                if (reservacion.fechaReservacion == reservacionEntity.fechaReservacion && reservacion.clienteNumero==reservacionEntity.clienteNumero) {
                    return NotFound("No puede reservar otro auto el mismo dia");
                }
                if (reservacion.fechaReservacion == reservacionEntity.fechaReservacion && reservacion.vehiculoNumero == reservacionEntity.vehiculoNumero && reservacion.clienteNumero != reservacionEntity.clienteNumero) {
                    return NotFound("El vehiculo ya esta apartado");
                }
            }

            var vehiculo = await _context.Vehiculo.FindAsync(reservacionEntity.vehiculoNumero);
            DateTime diaInicio = reservacionEntity.fechaReservacion;
            DateTime diaFin = reservacionEntity.fechaDevolucion;

            TimeSpan diferencia = diaFin - diaInicio;
            int dias = diferencia.Days;

            reservacionEntity.costoReservacion = vehiculo.precioAlquiler * dias;
            _context.Reservacion.Add(reservacionEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservacionEntity", new { id = reservacionEntity.reservacionNumero }, reservacionEntity);
        }

        // DELETE: api/Reservacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservacionEntity(int id)
        {
            var reservacionEntity = await _context.Reservacion.FindAsync(id);

            if (reservacionEntity == null)
            {
                return NotFound();
            }

            _context.Reservacion.Remove(reservacionEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservacionEntityExists(int id)
        {
            return _context.Reservacion.Any(e => e.reservacionNumero == id);
        }
    }
}
