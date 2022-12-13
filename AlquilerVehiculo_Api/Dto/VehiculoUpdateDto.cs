using AlquilerVehiculo_Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace AlquilerVehiculo_Api.Dto
{
    public class VehiculoUpdateDto
    {
        [Required(ErrorMessage = "Se requiere una matricula")]
        public string matricula { get; set; }
        [Required(ErrorMessage = "Se requiere el color")]
        public string color { get; set; }
        [Required(ErrorMessage = "Se requiere la marca")]
        public string marca { get; set; }
        [Required(ErrorMessage = "Se requiere el precio de alquiler")]
        public int precioAlquiler { get; set; }

        public VehiculoEntity toDtoUpdate()
        {
            return new VehiculoEntity()
            {
                matricula = matricula,
                color = color,
                marca = marca,
                precioAlquiler = precioAlquiler,
            };
        }
    }
}
