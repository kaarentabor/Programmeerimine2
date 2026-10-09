using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Clients;

public class ClientGetQuery : IRequest<OperationResult<ClientDto>>
{
    public int Id { get; set; }

}
