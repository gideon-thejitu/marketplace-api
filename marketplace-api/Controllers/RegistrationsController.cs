using System.Net.Mime;
using marketplace_api.Dto;
using marketplace_api.Models;
using marketplace_api.Services.RegistrationService;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class RegistrationsController :  ControllerBase
{
    private readonly IRegistrationService _registrationService;

    public RegistrationsController(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserIdentityDto))]
    public async Task<ActionResult<UserIdentityDto>> Create([FromBody] RegistrationCreateDto data)
    {
        var isEmailTaken = await _registrationService.EmailTaken(data.Email);

        if (isEmailTaken)
        {
            ModelState.AddModelError(nameof(UserIdentity.Email), "Provided email is taken!");
        }

        if (ModelState.IsValid == false)
        {
            return ValidationProblem(ModelState);
        }

        var result = await _registrationService.Create(data);

        return Ok(result);
    }
}
