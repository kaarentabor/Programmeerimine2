using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Clients;

public class ClientGetQueryHandler : IRequestHandler<ClientGetQuery, OperationResult<ClientDto>>
{
    private readonly ApplicationDbContext _dbContext;

    public ClientGetQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OperationResult<ClientDto>> Handle(ClientGetQuery request, CancellationToken cancellationToken)
    {
        var value = await _dbContext.Clients.AsNoTracking()
            .Where(entity => entity.ClientId == request.Id)
            .Select(entity => new ClientDto
            {
                ClientId = entity.ClientId,
                Name = entity.Name,
                Email = entity.Email,
                Phone = entity.Phone
            })
            .FirstOrDefaultAsync(cancellationToken);

        return new OperationResult<ClientDto>(value);
    }
}
