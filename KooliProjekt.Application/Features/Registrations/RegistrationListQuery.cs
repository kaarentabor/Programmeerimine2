using System.Collections.Generic;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Registrations;

public class RegistrationListQuery : IRequest<OperationResult<IList<RegistrationDto>>>
{
}
