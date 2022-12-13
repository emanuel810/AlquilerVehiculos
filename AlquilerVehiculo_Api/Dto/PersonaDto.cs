using AlquilerVehiculo_Api.Entities;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace AlquilerVehiculo_Api.Dto
{
    public class PersonaDto
    {
        [Required(ErrorMessage = "Se requiere el nombre de la persona")]
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        [Required(ErrorMessage = "Se requiere el apellido de la persona")]
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        [Required(ErrorMessage = "Se requiere la identificacion")]
        public string identificacion { get; set; }
        [Required(ErrorMessage = "Se requiere la licencia")]
        public string licencia { get; set; }
        [Required(ErrorMessage = "Se requiere el numero telefonico")]
        public string telefono { get; set; }
        public DateTime fechaNacimiento { get; set; }

        public PersonaEntity ToDto()
        {
            return new PersonaEntity()
            {
                primerNombre=primerNombre,
                segundoNombre=segundoNombre,
                primerApellido=primerApellido,
                segundoApellido=segundoApellido,
                identificacion=identificacion,
                licencia=licencia,
                telefono=telefono,
                fechaNacimiento=fechaNacimiento

            };
        }
    }
}
