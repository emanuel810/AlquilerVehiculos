
using AlquilerVehiculo_Api.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlquilerVehiculo_Api.Entities
{
    [Table("Persona")]
    public class PersonaEntity
    {
        [Key]
        public int personaNumero { get; set; }
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string identificacion { get; set; }
        public string licencia { get; set; }
        public string telefono { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public PersonaDto ToDto()
        {
            return new PersonaDto()
            {
                primerNombre = primerNombre,
                segundoNombre = segundoNombre,
                primerApellido = primerApellido,
                segundoApellido = segundoApellido,
                identificacion = identificacion,
                licencia = licencia,
                telefono = telefono,
                fechaNacimiento = fechaNacimiento

            };
        }
    }
}
