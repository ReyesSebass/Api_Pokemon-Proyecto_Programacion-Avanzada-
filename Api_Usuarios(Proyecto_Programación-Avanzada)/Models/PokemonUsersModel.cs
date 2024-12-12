namespace Api_Usuarios_Proyecto_Programación_Avanzada_.Models
{
    public class PokemonUsersModel
    {
        public int pokemon_user_id { get; set; }
        public UsuarioModel user_id { get; set; }
        public PokedexModel pokemon_id { get; set; }
    }
}
