using Api_Usuarios_Proyecto_Programación_Avanzada_.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Usuarios_Proyecto_Programación_Avanzada_.Data
{
    public class ConexionDbContext : DbContext
    {

        public ConexionDbContext ( DbContextOptions <ConexionDbContext> options) : base(options) { }

        // Parte de Usuarios
        public DbSet<UsuarioModel> G8_Users { get; set; }

        // Parte de Roles
        public DbSet<RolModel> G8_Roles { get; set; }

        //Parte de Pokedex
        public DbSet<PokedexModel> G8_Pokedex { get; set; }

        //Parte de Pokemon_users
        public DbSet<PokemonUsersModel> G8_Pokemon_Users { get; set; }

        //Parte de Pokemon_team
        public DbSet<PokemonTeamModel> G8_Pokemon_Team { get; set; }

        //Parte de Peticiones de enfermeria
       public DbSet<EnfermeriaModel> G8_Peticiones_de_Enfermeria { get; set; }

        //Parte de Retos
        public DbSet<RetosModel> G8_Retos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UsuarioModel>().ToTable("G8_Users");
            //modelBuilder.Entity<RolModel>().ToTable("G8_Roles");
            //modelBuilder.Entity<PokedexModel>().ToTable("G8_Pokedex");
            //modelBuilder.Entity<PokemonUsersModel>().ToTable("G8_Pokemon_Users");
            //modelBuilder.Entity<PokemonTeamModel>().ToTable("G8_Pokemon_Team");
            //modelBuilder.Entity<EnfermeriaModel>().ToTable("G8_Peticiones_de_Enfermeria");
            //modelBuilder.Entity<RetosModel>().ToTable("G8_Retos");

            modelBuilder.Entity<UsuarioModel>().ToTable("G8_Users").HasKey(u => u.user_id);
            modelBuilder.Entity<RolModel>().ToTable("G8_Roles").HasKey(r => r.rol_id);
            modelBuilder.Entity<PokedexModel>().ToTable("G8_Pokedex").HasKey(p => p.pokemon_id);
            modelBuilder.Entity<PokemonUsersModel>().ToTable("G8_Pokemon_Users").HasKey(pu => new { pu.user_id, pu.pokemon_id });
            modelBuilder.Entity<PokemonTeamModel>().ToTable("G8_Pokemon_Team").HasKey(pt => pt.team_id);
            modelBuilder.Entity<EnfermeriaModel>().ToTable("G8_Peticiones_de_Enfermeria").HasKey(e => e.peticion_id);
            modelBuilder.Entity<RetosModel>().ToTable("G8_Retos").HasKey(r => r.reto_id);
        }

    }
}
