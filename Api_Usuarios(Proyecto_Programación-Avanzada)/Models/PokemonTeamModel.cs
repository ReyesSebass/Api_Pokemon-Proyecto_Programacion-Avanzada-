namespace Api_Usuarios_Proyecto_Programación_Avanzada_.Models
{
    public class PokemonTeamModel
    {
        public int team_id { get; set; }
        public int entrenador_id { get; set; }
        public int pokemon1_id { get; set; }
        public int pokemon2_id { get; set; }
        public int pokemon3_id { get; set; }

        // Relaciones con la tabla 'users' y 'pokedex'
        public UsuarioModel Entrenador { get; set; }
        public PokedexModel Pokemon1 { get; set; }
        public PokedexModel Pokemon2 { get; set; }
        public PokedexModel Pokemon3 { get; set; }
    }
}
