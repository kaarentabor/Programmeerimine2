using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Payments;

public class PaymentGetQueryHandler : IRequestHandler<PaymentGetQuery, OperationResult<PaymentDto>>
{
    private readonly ApplicationDbContext _dbContext;

    public PaymentGetQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OperationResult<PaymentDto>> Handle(PaymentGetQuery request, CancellationToken cancellationToken)
    {
        var value = await _dbContext.Payments.AsNoTracking()
            .Where(entity => entity.PaymentId == request.Id)
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
            .FirstOrDefaultAsync(cancellationToken);

        return new OperationResult<PaymentDto>(value);
    }
}
