using System.ComponentModel.DataAnnotations;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Transactions;

public class TransactionListQuery : IRequest<OperationResult<PagedResult<TransactionDto>>>
{
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;
    [Range(1, 100)]
    public int PageSize { get; set; } = 10;
}
