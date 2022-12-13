using AlquilerVehiculo_Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace AlquilerVehiculo_Api.Dto
{
    public class ReservacionAddDto
    {
        [Required(ErrorMessage = "Se requiere fecha de reservacion")]
        public DateTime fechaReservacion { get; set; }
        [Required(ErrorMessage = "Se requiere fecha de devolucion")]
        public DateTime fechaDevolucion { get; set; }
        [Required(ErrorMessage = "Se requiere un id")]
        public int vehiculoNumero { get; set; }
        [Required(ErrorMessage = "Se requiere un id")]
        public int clienteNumero { get; set; }

        public ReservacionEntity toAddDto() {
            return new ReservacionEntity()
            {
                fechaReservacion = fechaReservacion,
                fechaDevolucion = fechaDevolucion,
                vehiculoNumero=vehiculoNumero,
                clienteNumero=clienteNumero
            };
        }
    }
}
