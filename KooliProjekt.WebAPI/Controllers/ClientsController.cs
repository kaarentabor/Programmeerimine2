using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Features.Clients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers;

public class ClientsController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public ClientsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ClientListQuery(), cancellationToken);
        if (result == null)
        {
            return NotFound();
        }

        return Result(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        var result = await _mediator.Send(new ClientGetQuery { Id = id }, cancellationToken);
        if (result == null)
        {
            return NotFound();
        }
        if (!result.HasErrors && result.Value == null)
        {
            return NotFound();
        }

        return Result(result);
    }
}
