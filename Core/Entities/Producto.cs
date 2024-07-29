namespace Core.Entities;

public class Producto
{
    public int Id { set; get; }
    public string Nombre { set; get; }
    public decimal Precio { set; get; }
    public DateTime FechaCreacion { set; get; }

    public int MarcaId { set; get; }
    public Marca Marca { set; get; }

    public int CategoriaId { set; get; }
    public Categoria Categoria { set; get; }

}
