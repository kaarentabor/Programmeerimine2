using System;

namespace KooliProjekt.Application.Features.Payments;

public class PaymentDto
{
    public string EventTitle { get; set; }
    public string InvoiceNumber { get; set; }
    public string ClientName { get; set; }
    public int PaymentId { get; set; }
    public int RegistrationId { get; set; }
    public int EventId { get; set; }
    public int InvoiceId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Status { get; set; }
    public string PaymentMethod { get; set; }
}
