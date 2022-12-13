using AlquilerVehiculo_Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace AlquilerVehiculo_Api.Dto
{
    public class ClienteDto
    {
        [Required(ErrorMessage = "Se requiere un id")]
        public int personaNumero { get; set; }

        public ClienteEntity toDto() {
            return new ClienteEntity()
            {
                personaNumero = personaNumero
            };
        }
    }
}
