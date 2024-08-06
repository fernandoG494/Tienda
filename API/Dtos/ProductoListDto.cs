namespace API.Dtos;

public class ProductoListDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public DateTime FechCreacion { get; set; }
    public MarcaDto Marca { get; set; }
    public CategoriaDto Categoria { get; set; }
}
