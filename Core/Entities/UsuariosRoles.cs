﻿namespace Core.Entities;

public class UsuariosRoles
{
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
}
