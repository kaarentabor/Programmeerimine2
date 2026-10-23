using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Features.FileAttachments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers;

public class FileAttachmentsController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public FileAttachmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] FileAttachmentListQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Result(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new FileAttachmentGetQuery { Id = id }, cancellationToken);
        return Result(result);
    }
}