using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Payments;

public class PaymentListQueryHandler : IRequestHandler<PaymentListQuery, OperationResult<IList<PaymentDto>>>
{
    private readonly ApplicationDbContext _dbContext;

    public PaymentListQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OperationResult<IList<PaymentDto>>> Handle(PaymentListQuery request, CancellationToken cancellationToken)
    {
        var value = await _dbContext.Payments.AsNoTracking()
            .OrderBy(entity => entity.PaymentId)
            .Select(entity => new PaymentDto
            {
                PaymentId = entity.PaymentId,
                RegistrationId = entity.RegistrationId,
                EventId = entity.EventId,
                InvoiceId = entity.InvoiceId,
                Amount = entity.Amount,
                PaymentDate = entity.PaymentDate,
                Status = entity.Status,
                PaymentMethod = entity.PaymentMethod
            })
            .ToListAsync(cancellationToken);

        return new OperationResult<IList<PaymentDto>>(value);
    }
}
