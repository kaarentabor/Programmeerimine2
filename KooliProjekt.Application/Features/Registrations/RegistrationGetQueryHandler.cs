using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Registrations;

public class RegistrationGetQueryHandler : IRequestHandler<RegistrationGetQuery, OperationResult<RegistrationDto>>
{
    private readonly ApplicationDbContext _dbContext;

    public RegistrationGetQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OperationResult<RegistrationDto>> Handle(RegistrationGetQuery request, CancellationToken cancellationToken)
    {
        var value = await _dbContext.Registrations.AsNoTracking()
            .Where(entity => entity.RegistrationId == request.Id)
            .Select(entity => new RegistrationDto
            {
                RegistrationId = entity.RegistrationId,
                TransactionId = entity.TransactionId,
                EventId = entity.EventId,
                RegistrationTime = entity.RegistrationTime,
                Status = entity.Status
            })
            .FirstOrDefaultAsync(cancellationToken);

        return new OperationResult<RegistrationDto>(value);
    }
}
