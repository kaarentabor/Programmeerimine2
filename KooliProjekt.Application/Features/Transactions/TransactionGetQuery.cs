using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Transactions;

public class TransactionGetQuery : IRequest<OperationResult<TransactionDto>>
{
    public int Id { get; set; }

}
