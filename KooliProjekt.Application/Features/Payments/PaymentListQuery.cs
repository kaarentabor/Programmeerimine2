using System.Collections.Generic;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Payments;

public class PaymentListQuery : IRequest<OperationResult<IList<PaymentDto>>>
{
}
