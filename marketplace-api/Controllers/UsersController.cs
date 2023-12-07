using System.Net.Mime;
using marketplace_api.Dto;
using marketplace_api.Services.UserService;
using marketplace_api.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_api.Controllers;

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
    [HasPermission("user.read")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponseDto<UserIdentityDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PaginatedResponseDto<UserIdentityDto>>> AllUsers([FromQuery] UserIdentityFilterDto query)
    {
        var result = await _userServiceService.GetAllUsers(query);

        return Ok(result);
    }
}
