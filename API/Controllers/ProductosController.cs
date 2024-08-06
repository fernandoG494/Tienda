using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductosController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductosController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET api/
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Producto>>> Get()
    {
        var productos = await _unitOfWork.Productos.GetAllAsync();
        return Ok(productos);
    }

    // GET api/:id
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int id)
    {
        var productos = await _unitOfWork.Productos.GetByIdAsync(id);
        return Ok(productos);
    }

    // POST api/Productos
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Producto>> Post(Producto producto)
    {
        _unitOfWork.Productos.Add(producto);
        _unitOfWork.Save();

        if (producto == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Post), new {id=producto.Id}, producto);
    }

    // PUT api/Productos
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Producto>> Put(int id, [FromBody]Producto producto)
    {
        if (producto == null)
        {
            return NotFound();
        }

        _unitOfWork.Productos.Update(producto);
        _unitOfWork.Save();

        return producto;
    }

    // DELETE api/Productos
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Producto>> Delete(int id)
    {
        var producto = await _unitOfWork.Productos.GetByIdAsync(id);
        if (producto == null)
        {
            return NotFound();
        }

        _unitOfWork.Productos.Remove(producto);
        _unitOfWork.Save();

        return NoContent();
    }
}
