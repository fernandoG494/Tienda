namespace Core.Entities;

public class Usuario : BaseEntity
{
    public string Nombres { get; set; }
    public string ApellidoMaterno { get; set; }
    public string ApellidoPaterno { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<Role> Roles { get; set; } = new HashSet<Role>();
    public ICollection<UsuariosRoles> UsuariosRoles { get; set; }

}
