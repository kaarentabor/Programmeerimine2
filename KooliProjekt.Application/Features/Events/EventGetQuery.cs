using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Events;

public class EventGetQuery : IRequest<OperationResult<EventDto>>
{
    public int Id { get; set; }

}
