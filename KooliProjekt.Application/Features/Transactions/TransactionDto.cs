#nullable enable annotations
using System;

namespace KooliProjekt.Application.Features.Transactions;

public class TransactionDto
{
    public int TransactionId { get; set; }
    public int ClientId { get; set; }
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal PaidAmount { get; set; }
    public string PaymentMethod { get; set; }
    public string? Summary { get; set; }
}
