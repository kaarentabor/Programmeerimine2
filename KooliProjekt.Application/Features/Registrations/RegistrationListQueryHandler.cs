using KooliProjekt.Application.Infrastructure.Paging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Registrations;

public class RegistrationListQueryHandler : IRequestHandler<RegistrationListQuery, OperationResult<PagedResult<RegistrationDto>>>
{
    private readonly ApplicationDbContext _dbContext;

    public RegistrationListQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OperationResult<PagedResult<RegistrationDto>>> Handle(RegistrationListQuery request, CancellationToken cancellationToken)
    {
        var value = await _dbContext.Registrations.AsNoTracking()
            .OrderBy(entity => entity.RegistrationId)
            .Select(entity => new RegistrationDto
            {
                EventTitle = entity.Event.Title,
                TransactionName = entity.Transaction.Name,
                ClientName = entity.Transaction.Client.Name,
                RegistrationId = entity.RegistrationId,
                TransactionId = entity.TransactionId,
                EventId = entity.EventId,
                RegistrationTime = entity.RegistrationTime,
                Status = entity.Status
            })
            .GetPagedAsync(request.Page, request.PageSize, cancellationToken);

        return new OperationResult<PagedResult<RegistrationDto>>(value);
    }
}
