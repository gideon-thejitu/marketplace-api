using System.Net.Mime;
using Marketplace.Dto;
using Marketplace.Services.AuthServiceService;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class AuthController :  ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    public async Task<ActionResult<AuthCreateDto>> Create([FromBody] AuthCreateDto data)
    {
        var result = await _authService.Create(data);

        if (result is null)
        {
            return Unauthorized("Incorrect email or password");
        }

        return Ok(result);
    }
}