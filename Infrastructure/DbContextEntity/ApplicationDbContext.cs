using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Enums;

namespace Infrastructure.Data.DbContextEntity
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Branch> Branch { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Agregar datos semilla para la tabla users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Document = "1088027397",
                    Name = "Jorge Enrique",
                    LastName = "Franco Herrera",
                    PhoneNumber = "3148434889",
                    TypeUser = Domain.Enums.TypeUser.Administrator,
                    Password = "12345678a"
                },
                new User
                {
                    Id = 2,
                    Document = "1094952038",
                    Name = "Juan Luis",
                    LastName = "Portela Guerra",
                    PhoneNumber = "3127248659",
                    TypeUser = Domain.Enums.TypeUser.Manager, 
                    Password = "12345678a"
                },
                new User
                {
                    Id = 3,
                    Document = "1088352391",
                    Name = "Natalia",
                    LastName = "Perez Ortega",
                    PhoneNumber = "3022901238",
                    TypeUser = Domain.Enums.TypeUser.Manager, 
                    Password = "12345678a"
                }
            );

            // Agregar datos semilla para la tabla de branches
            modelBuilder.Entity<Branch>().HasData(
                new Branch
                {
                    Id = 1,
                    Name = "Alpina",
                    Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                    PhoneNumber = "3154096906",
                    Email = "gerencia@alpina.com",
                    Schedule = "8:00am - 6:00pm",
                    Exchange = TypeExchange.COP, // Ajusta según tu enumeración
                    CreateDate = DateTime.Parse("2024-03-31T16:18:42.6988983-05:00"),
                    UpdateDate = null,
                    UserId = 2
                },
                new Branch
                {
                    Id = 2,
                    Name = "Colanta",
                    Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                    PhoneNumber = "3154096906",
                    Email = "gerencia@colanta.com",
                    Schedule = "8:00am - 6:00pm",
                    Exchange = TypeExchange.COP, // Ajusta según tu enumeración
                    CreateDate = DateTime.Parse("2024-03-29T22:44:34.2583444"),
                    UpdateDate = null,
                    UserId = 3
                }
            );
        }

    }
}
