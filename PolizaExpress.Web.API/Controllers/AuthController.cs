using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolizaExpress.Application.Extensions;
using PolizaExpress.Application.Usuarios.Loguear;
using PolizaExpress.Application.Usuarios.Registrar;

namespace PolizaExpress.Web.API.Controllers;

[Route("api/autenticacion")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("registrar")]
    public async Task<IActionResult> Post(
        [FromBody] RegistrarUsuarioCommand command,
        [FromServices] IValidator<RegistrarUsuarioCommand> validator)
    {
        var validationResults = await validator.ValidateAsync(command);

        if (!validationResults.IsValid)
        {
            return BadRequest(validationResults.Errors.ToErrorResponse());
        }
        
        await _mediator.Send(command);
        
        return Ok();
    }
    
    [HttpPost("loguear")]
    public async Task<IActionResult> Post(
        [FromBody] LoguearUsuarioCommand command,
        [FromServices] IValidator<LoguearUsuarioCommand> validator)
    {
        var validationResults = await validator.ValidateAsync(command);

        if (!validationResults.IsValid)
        {
            return BadRequest(validationResults.Errors.ToErrorResponse());
        }
        
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }
}