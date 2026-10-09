using System.Collections.Generic;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Transactions;

public class TransactionListQuery : IRequest<OperationResult<IList<TransactionDto>>>
{
}
