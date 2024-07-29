using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class TiendaContext : DbContext
{
    public TiendaContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Producto> Productos { get; set; }
}
