using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Agents;

public class AgentGetQuery : IRequest<OperationResult<AgentDto>>
{
    public int Id { get; set; }

}
