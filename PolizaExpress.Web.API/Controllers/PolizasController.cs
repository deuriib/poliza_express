using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PolizaExpress.Application.Extensions;
using PolizaExpress.Application.Polizas.Commands.Crear;
using PolizaExpress.Application.Polizas.Queries.Buscar;

namespace PolizaExpress.Web.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PolizasController : ControllerBase
{
    private readonly IMediator _mediator;

    public PolizasController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    // GET
    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> Get([FromQuery] string query)
    {
        var polizas = await _mediator.Send(new BuscarQuery
        {
            Query = query
        });

        if (polizas is null) return NotFound();
        
        return Ok(polizas);
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> Post(
        [FromBody] CrearPolizaCommand polizaCommand, 
        [FromServices] IValidator<CrearPolizaCommand> validator)
    {
        var validationResults = await validator.ValidateAsync(polizaCommand);

        if (!validationResults.IsValid)
        {
            return BadRequest(validationResults.Errors.ToErrorResponse());
        }
        
        var poliza = await _mediator.Send(polizaCommand);
        
        return Created(new Uri($"/api/Polizas/search/{poliza.NumeroPoliza}"), poliza);
    }
}