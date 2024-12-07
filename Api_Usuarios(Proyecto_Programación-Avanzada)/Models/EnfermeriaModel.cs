namespace Api_Usuarios_Proyecto_Programación_Avanzada_.Models
{
    public class EnfermeriaModel
    {
        public int peticion_id { get; set; }
        public int enfermera_id { get; set; }
        public int entrenador_id { get; set; }
        public int pokemon_id { get; set; }
        public DateTime fecha_peticion { get; set; }
        public string estado { get; set; }

        // Relaciones con la tabla 'users' y 'pokedex'
        public UsuarioModel Enfermera { get; set; }
        public UsuarioModel Entrenador { get; set; }
        public PokedexModel Pokemon { get; set; }
    }
}
