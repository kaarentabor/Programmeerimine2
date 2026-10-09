using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Transactions;

public class TransactionGetQueryHandler : IRequestHandler<TransactionGetQuery, OperationResult<TransactionDto>>
{
    private readonly ApplicationDbContext _dbContext;

    public TransactionGetQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OperationResult<TransactionDto>> Handle(TransactionGetQuery request, CancellationToken cancellationToken)
    {
        var value = await _dbContext.Transactions.AsNoTracking()
            .Where(entity => entity.TransactionId == request.Id)
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
            .FirstOrDefaultAsync(cancellationToken);

        return new OperationResult<TransactionDto>(value);
    }
}
