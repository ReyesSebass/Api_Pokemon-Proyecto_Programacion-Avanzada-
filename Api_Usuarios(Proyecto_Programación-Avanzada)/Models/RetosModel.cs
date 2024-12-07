namespace Api_Usuarios_Proyecto_Programación_Avanzada_.Models
{
    public class RetosModel
    {
        public int reto_id { get; set; }
        public int entrenador1_id { get; set; }
        public int entrenador2_id { get; set; }
        public int equipo1_id { get; set; }
        public int equipo2_id { get; set; }
        public DateTime fecha_reto { get; set; }
        public string estado { get; set; }

        // Relaciones con la tabla 'users'
        public UsuarioModel Entrenador1 { get; set; }
        public UsuarioModel Entrenador2 { get; set; }
    }
}
