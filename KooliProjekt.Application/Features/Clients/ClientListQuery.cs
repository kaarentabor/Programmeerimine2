using System.Collections.Generic;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Clients;

public class ClientListQuery : IRequest<OperationResult<IList<ClientDto>>>
{
}
