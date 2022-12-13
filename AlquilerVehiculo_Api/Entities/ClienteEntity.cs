
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AlquilerVehiculo_Api.Dto;

namespace AlquilerVehiculo_Api.Entities
{
    [Table("Cliente")]
    public class ClienteEntity
    {
        [Key]
        public int clienteNumero { get; set; }

        public int personaNumero { get; set; }

        public ClienteDto toDto()
        {
            return new ClienteDto()
            {
                personaNumero = personaNumero
            };
        }

    }
}