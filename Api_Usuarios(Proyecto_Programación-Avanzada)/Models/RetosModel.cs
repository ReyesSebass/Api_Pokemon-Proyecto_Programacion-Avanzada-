namespace Api_Usuarios_Proyecto_Programación_Avanzada_.Models
{
    public class RetosModel
    {
        public int reto_id { get; set; }
        public UsuarioModel entrenador1_id { get; set; }
        public UsuarioModel entrenador2_id { get; set; }
        public PokemonTeamModel equipo1_id { get; set; }
        public PokemonTeamModel equipo2_id { get; set; }
        public DateTime fecha_reto { get; set; }
        public string estado { get; set; }
    }
}
