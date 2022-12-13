
using AlquilerVehiculo_Api.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace AlquilerVehiculo_Api.Entities
{
    [Table("Vehiculo")]
    public class VehiculoEntity
    {
        [Key]
        public int vehiculoNumero { get; set; }
        public string matricula { get; set; }
        public string color { get; set; }
        public string marca { get; set; }
        public int precioAlquiler { get; set; }
        public int garage { get; set; }

        public VehiculoAddDto toDtoAdd()
        {
            return new VehiculoAddDto()
            {
                matricula = matricula,
                color = color,
                marca = marca,
                precioAlquiler = precioAlquiler,
                garage = garage
            };
        }
        public VehiculoUpdateDto toDtoUpdate()
        {
            return new VehiculoUpdateDto()
            {
                matricula = matricula,
                color = color,
                marca = marca,
                precioAlquiler = precioAlquiler,
            };
        }

    }
}
