﻿using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class ProductoAddUpdateDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public DateTime FechCreacion { get; set; }
    public MarcaDto Marca { get; set; }
    public CategoriaDto Categoria { get; set; }
}
