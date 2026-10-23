using KooliProjekt.Application.Infrastructure.Paging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Agents;

public class AgentListQueryHandler : IRequestHandler<AgentListQuery, OperationResult<PagedResult<AgentDto>>>
{
    private readonly ApplicationDbContext _dbContext;

    public AgentListQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OperationResult<PagedResult<AgentDto>>> Handle(AgentListQuery request, CancellationToken cancellationToken)
    {
        var value = await _dbContext.Agents.AsNoTracking()
            .OrderBy(entity => entity.AgentId)
            .Select(entity => new AgentDto
            {
                AgentId = entity.AgentId,
                Name = entity.Name,
                Email = entity.Email,
                Role = entity.Role
            })
            .GetPagedAsync(request.Page, request.PageSize, cancellationToken);

        return new OperationResult<PagedResult<AgentDto>>(value);
    }
}
