using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Events;

public class EventListQueryHandler : IRequestHandler<EventListQuery, OperationResult<IList<EventDto>>>
{
    private readonly ApplicationDbContext _dbContext;

    public EventListQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OperationResult<IList<EventDto>>> Handle(EventListQuery request, CancellationToken cancellationToken)
    {
        var value = await _dbContext.Events.AsNoTracking()
            .OrderBy(entity => entity.EventId)
            .Select(entity => new EventDto
            {
                EventId = entity.EventId,
                ClientId = entity.ClientId,
                EventTypeId = entity.EventTypeId,
                InvoiceNumber = entity.InvoiceNumber,
                Summary = entity.Summary,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                EventPlace = entity.EventPlace,
                Status = entity.Status,
                Title = entity.Title
            })
            .ToListAsync(cancellationToken);

        return new OperationResult<IList<EventDto>>(value);
    }
}
