
using AlquilerVehiculo_Api.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlquilerVehiculo_Api.Entities
{
    [Table("Reservacion")]
    public class ReservacionEntity
    {
        [Key]
        public int reservacionNumero { get; set; }
        public DateTime fechaReservacion { get; set; }
        public DateTime fechaDevolucion { get; set; }
        public int costoReservacion { get; set; }
        public int vehiculoNumero { get; set; }
        public int clienteNumero { get; set; }


    }
}
