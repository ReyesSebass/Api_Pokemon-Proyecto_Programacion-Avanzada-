namespace Api_Usuarios_Proyecto_Programación_Avanzada_.Models
{
    public class EnfermeriaModel
    {
        public int peticion_id { get; set; }
        public UsuarioModel enfermera_id { get; set; }
        public UsuarioModel entrenador_id { get; set; }
        public PokedexModel pokemon_id { get; set; }
        public DateTime fecha_peticion { get; set; }
        public string estado { get; set; }
    }
}
