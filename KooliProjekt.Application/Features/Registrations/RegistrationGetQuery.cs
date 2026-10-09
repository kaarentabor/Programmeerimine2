using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Registrations;

public class RegistrationGetQuery : IRequest<OperationResult<RegistrationDto>>
{
    public int Id { get; set; }

}
