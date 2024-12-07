namespace Api_Usuarios_Proyecto_Programación_Avanzada_.Models
{
    public class PokemonUsersModel
    {
        public int pokemon_userID { get; set; }
        public int user_id { get; set; }
        public int pokemon_id { get; set; }

        // Propiedad de navegación para la relación con la tabla 'users'
        public UsuarioModel user { get; set; }

        // Propiedad de navegación para la relación con la tabla 'pokedex'
        public PokedexModel pokedex { get; set; }
    }
}
