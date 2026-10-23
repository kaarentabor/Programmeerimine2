using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Features.Registrations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers;

public class RegistrationsController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public RegistrationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] RegistrationListQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Result(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RegistrationGetQuery { Id = id }, cancellationToken);
        return Result(result);
    }
}