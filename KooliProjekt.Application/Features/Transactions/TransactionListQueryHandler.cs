using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Transactions;

public class TransactionListQueryHandler : IRequestHandler<TransactionListQuery, OperationResult<IList<TransactionDto>>>
{
    private readonly ApplicationDbContext _dbContext;

    public TransactionListQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OperationResult<IList<TransactionDto>>> Handle(TransactionListQuery request, CancellationToken cancellationToken)
    {
        var value = await _dbContext.Transactions.AsNoTracking()
            .OrderBy(entity => entity.TransactionId)
            .Select(entity => new TransactionDto
            {
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
            .ToListAsync(cancellationToken);

        return new OperationResult<IList<TransactionDto>>(value);
    }
}
