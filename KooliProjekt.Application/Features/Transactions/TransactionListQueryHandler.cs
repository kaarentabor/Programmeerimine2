using KooliProjekt.Application.Infrastructure.Paging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Transactions;

public class TransactionListQueryHandler : IRequestHandler<TransactionListQuery, OperationResult<PagedResult<TransactionDto>>>
{
    private readonly ApplicationDbContext _dbContext;

    public TransactionListQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OperationResult<PagedResult<TransactionDto>>> Handle(TransactionListQuery request, CancellationToken cancellationToken)
    {
        var value = await _dbContext.Transactions.AsNoTracking()
            .OrderBy(entity => entity.TransactionId)
            .Select(entity => new TransactionDto
            {
                ClientName = entity.Client.Name,
                OutstandingAmount = entity.TotalPrice - entity.PaidAmount,
                TransactionId = entity.TransactionId,
                ClientId = entity.ClientId,
                Name = entity.Name,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                TotalPrice = entity.TotalPrice,
                PaidAmount = entity.PaidAmount,
                PaymentMethod = entity.PaymentMethod,
                Summary = entity.Summary
            })
            .GetPagedAsync(request.Page, request.PageSize, cancellationToken);

        return new OperationResult<PagedResult<TransactionDto>>(value);
    }
}
