using AlquilerVehiculo_Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace AlquilerVehiculo_Api.Dto
{
    public class VehiculoAddDto
    {
        [Required(ErrorMessage = "Se requiere la matricula")]
        public string matricula { get; set; }
        [Required(ErrorMessage = "Se requiere el color")]
        public string color { get; set; }
        [Required(ErrorMessage = "Se requiere la marca")]
        public string marca { get; set; }
        public int precioAlquiler { get; set; }
        [Required(ErrorMessage = "Se requiere el numero de garage")]
        public int garage { get; set; }

        public VehiculoEntity toDtoAdd(){
            return new VehiculoEntity()
            {
                matricula = matricula,
                color = color,
                marca = marca,
                precioAlquiler = precioAlquiler,
                garage = garage
            };
        }
    }
}
