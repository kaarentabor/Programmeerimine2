using System.Collections.Generic;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Agents;

public class AgentListQuery : IRequest<OperationResult<IList<AgentDto>>>
{
}
