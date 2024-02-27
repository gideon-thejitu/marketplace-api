using System.Net.Mime;
using Marketplace.Dto;
using Marketplace.Infrastructure.Filters;
using Marketplace.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userServiceService;

    public UsersController(IUserService userServiceService)
    {
        _userServiceService = userServiceService;
    }
    
    [HttpGet]
    [Authorize]
    [IsAuthorizedFor("user", "read")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponseDto<UserIdentityDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PaginatedResponseDto<UserIdentityDto>>> AllUsers([FromQuery] UserIdentityFilterDto query)
    {
        var result = await _userServiceService.GetAllUsers(query);

        return Ok(result);
    }
}
