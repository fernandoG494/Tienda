using API.Dtos;
using Asp.Versioning;
using AutoMapper;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[ApiVersion("0.8")]
[ApiVersion("1")]
//[Route("api/v{v:apiVersion}/datosproductos")]
// Nota: se manda el query param como '?v=1' u otra version que querramos 

public class DatosProductosController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DatosProductosController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductoListDto>>> Get()
    {
        var productos = await _unitOfWork.Productos.GetAllAsync();
        return _mapper.Map<List<ProductoListDto>>(productos);
    }

    [HttpGet]
    [MapToApiVersion("1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> GetProductosDesglosados()
    {
        var productos = await _unitOfWork.Productos.GetAllAsync();
        return _mapper.Map<List<ProductoDto>>(productos);
    }
}
