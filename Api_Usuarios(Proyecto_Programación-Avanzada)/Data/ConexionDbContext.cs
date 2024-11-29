using Api_Usuarios_Proyecto_Programación_Avanzada_.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Api_Usuarios_Proyecto_Programación_Avanzada_.Data
{
    public class ConexionDbContext : DbContext
    {

        public ConexionDbContext(DbContextOptions<ConexionDbContext> options) : base(options) { }

        // Parte de Usuarios
        public DbSet<UsuarioModel> users { get; set; }

        // Parte de Canciones
        public DbSet<RolModel> roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioModel>().ToTable("users");
            modelBuilder.Entity<RolModel>().ToTable("roles");
        }

    }
}
