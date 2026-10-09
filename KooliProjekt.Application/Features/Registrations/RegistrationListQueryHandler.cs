using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Registrations;

public class RegistrationListQueryHandler : IRequestHandler<RegistrationListQuery, OperationResult<IList<RegistrationDto>>>
{
    private readonly ApplicationDbContext _dbContext;

    public RegistrationListQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OperationResult<IList<RegistrationDto>>> Handle(RegistrationListQuery request, CancellationToken cancellationToken)
    {
        var value = await _dbContext.Registrations.AsNoTracking()
            .OrderBy(entity => entity.RegistrationId)
            .Select(entity => new RegistrationDto
            {
                RegistrationId = entity.RegistrationId,
                TransactionId = entity.TransactionId,
                EventId = entity.EventId,
                RegistrationTime = entity.RegistrationTime,
                Status = entity.Status
            })
            .ToListAsync(cancellationToken);

        return new OperationResult<IList<RegistrationDto>>(value);
    }
}
