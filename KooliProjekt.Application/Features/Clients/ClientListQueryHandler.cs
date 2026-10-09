using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Clients;

public class ClientListQueryHandler : IRequestHandler<ClientListQuery, OperationResult<IList<ClientDto>>>
{
    private readonly ApplicationDbContext _dbContext;

    public ClientListQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OperationResult<IList<ClientDto>>> Handle(ClientListQuery request, CancellationToken cancellationToken)
    {
        var value = await _dbContext.Clients.AsNoTracking()
            .OrderBy(entity => entity.ClientId)
            .Select(entity => new ClientDto
            {
                ClientId = entity.ClientId,
                Name = entity.Name,
                Email = entity.Email,
                Phone = entity.Phone
            })
            .ToListAsync(cancellationToken);

        return new OperationResult<IList<ClientDto>>(value);
    }
}
