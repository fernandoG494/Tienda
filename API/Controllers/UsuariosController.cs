using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : BaseApiController
{
    private readonly IUserService _userService;

    public UsuariosController(IUserService userService)
    {
        _userService = userService;
    }
}
