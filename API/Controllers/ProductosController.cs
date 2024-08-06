﻿using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductosController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ProductosController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    // GET api/
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductoListDto>>> Get()
    {
        var productos = await _unitOfWork.Productos.GetAllAsync();
        return _mapper.Map<List<ProductoListDto>>(productos);
    }

    // GET api/:id
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductoDto>> Get(int id)
    {
        var producto = await _unitOfWork.Productos.GetByIdAsync(id);
        
        if (producto == null)
        {
            return NotFound();
        }
        
        return _mapper.Map<ProductoDto>(producto);
    }

    // POST api/Productos
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Producto>> Post(ProductoAddUpdateDto productoDto)
    {
        var producto = _mapper.Map<Producto>(productoDto);
        _unitOfWork.Productos.Add(producto);
        await _unitOfWork.SaveAsync();

        if (producto == null)
        {
            return BadRequest();
        }
        productoDto.Id = producto.Id;
        return CreatedAtAction(nameof(Post), new {id = producto.Id}, producto);
    }

    // PUT api/Productos
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductoAddUpdateDto>> Put(int id, [FromBody] ProductoAddUpdateDto productoDto)
    {
        if ( productoDto == null )
        {
            return NotFound();
        }

        var producto = _mapper.Map<Producto>(productoDto);
        _unitOfWork.Productos.Update(producto);

        await _unitOfWork.SaveAsync();

        return productoDto;
    }

    // DELETE api/Productos
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var producto = await _unitOfWork.Productos.GetByIdAsync(id);
        if ( producto == null )
        {
            return NotFound();
        }

        _unitOfWork.Productos.Remove(producto);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}
