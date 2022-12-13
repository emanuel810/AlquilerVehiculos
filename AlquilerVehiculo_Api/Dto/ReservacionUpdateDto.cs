using AlquilerVehiculo_Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace AlquilerVehiculo_Api.Dto
{
    public class ReservacionUpdateDto
    {
        [Required(ErrorMessage = "Se requiere fecha de reservacion")]
        public DateTime fechaReservacion { get; set; }
        [Required(ErrorMessage = "Se requiere fecha de devolucion")]
        public DateTime fechaDevolucion { get; set; }
        public int vehiculoNumero { get; set; }


        public ReservacionEntity toUpdateDto() {
            return new ReservacionEntity() 
            {
                fechaReservacion = fechaReservacion,
                fechaDevolucion = fechaDevolucion,
                vehiculoNumero= vehiculoNumero
            };
        }
    }
}
