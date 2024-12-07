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

        // Parte de Roles
        public DbSet<RolModel> roles { get; set; }

        //Parte de Pokedex
        public DbSet<PokedexModel> pokedex { get; set; }

        //Parte de Pokemon_users
        public DbSet<PokemonUsersModel> pokemon_users { get; set; }

        //Parte de Pokemon_team
        public DbSet<PokemonTeamModel> pokemon_team { get; set; }

        //Parte de Peticiones de enfermeria
        public DbSet<EnfermeriaModel> peticones_de_enfermeria { get; set; }

        //Parte de Retos
        public DbSet<RetosModel> retos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioModel>().ToTable("users");
            modelBuilder.Entity<RolModel>().ToTable("roles");
            modelBuilder.Entity<PokedexModel>().ToTable("pokedex");
            modelBuilder.Entity<PokemonUsersModel>().ToTable("pokemon_users");
            modelBuilder.Entity<PokemonTeamModel>().ToTable("pokemon_team");
            modelBuilder.Entity<EnfermeriaModel>().ToTable("peticiones_de_enfermeria");
            modelBuilder.Entity<RetosModel>().ToTable("retos");
        }

    }
}
