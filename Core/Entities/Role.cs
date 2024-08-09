namespace Core.Entities;

public class Role : BaseEntity
{
    public string Nombre { get; set; }
    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    public ICollection<UsuariosRoles> UsuariosRoles { get; set; } = new List<UsuariosRoles>();
}
