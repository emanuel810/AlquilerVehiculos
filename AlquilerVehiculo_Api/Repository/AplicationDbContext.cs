using AlquilerVehiculo_Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlquilerVehiculo_Api.Repository
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ClienteEntity> Cliente { get; set; }

        public DbSet<VehiculoEntity> Vehiculo { get; set; }

        public DbSet<ReservacionEntity> Reservacion { get; set; }

        public DbSet<PersonaEntity> Persona { get; set; }

    }
}
