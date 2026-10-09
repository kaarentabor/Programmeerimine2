using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Events;

public class EventGetQueryHandler : IRequestHandler<EventGetQuery, OperationResult<EventDto>>
{
    private readonly ApplicationDbContext _dbContext;

    public EventGetQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OperationResult<EventDto>> Handle(EventGetQuery request, CancellationToken cancellationToken)
    {
        var value = await _dbContext.Events.AsNoTracking()
            .Where(entity => entity.EventId == request.Id)
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
            .FirstOrDefaultAsync(cancellationToken);

        return new OperationResult<EventDto>(value);
    }
}
