namespace Api_Usuarios_Proyecto_Programación_Avanzada_.Models
{
    public class UsuarioModel
    {
        public int user_id { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string contraseña { get; set; }
        public RolModel rol_id { get; set; }
    }
}
