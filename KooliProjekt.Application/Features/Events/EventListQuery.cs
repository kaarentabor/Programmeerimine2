using System.Collections.Generic;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Events;

public class EventListQuery : IRequest<OperationResult<IList<EventDto>>>
{
}
