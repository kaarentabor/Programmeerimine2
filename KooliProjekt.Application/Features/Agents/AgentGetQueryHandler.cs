using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Agents;

public class AgentGetQueryHandler : IRequestHandler<AgentGetQuery, OperationResult<AgentDto>>
{
    private readonly ApplicationDbContext _dbContext;

    public AgentGetQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OperationResult<AgentDto>> Handle(AgentGetQuery request, CancellationToken cancellationToken)
    {
        var value = await _dbContext.Agents.AsNoTracking()
            .Where(entity => entity.AgentId == request.Id)
            .Select(entity => new AgentDto
            {
                AgentId = entity.AgentId,
                Name = entity.Name,
                Email = entity.Email,
                Role = entity.Role
            })
            .FirstOrDefaultAsync(cancellationToken);

        return new OperationResult<AgentDto>(value);
    }
}
