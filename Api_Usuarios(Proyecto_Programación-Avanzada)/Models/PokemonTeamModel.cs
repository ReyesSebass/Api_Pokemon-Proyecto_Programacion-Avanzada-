namespace Api_Usuarios_Proyecto_Programación_Avanzada_.Models
{
    public class PokemonTeamModel
    {
        public int team_id { get; set; }
        public UsuarioModel entrenador_id { get; set; }
        public PokedexModel pokemon1_id { get; set; }
        public PokedexModel pokemon2_id { get; set; }
        public PokedexModel pokemon3_id { get; set; }
    }
}
