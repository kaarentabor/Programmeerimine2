using System;

namespace KooliProjekt.Application.Features.Registrations;

public class RegistrationDto
{
    public string EventTitle { get; set; }
    public string TransactionName { get; set; }
    public string ClientName { get; set; }
    public int RegistrationId { get; set; }
    public int TransactionId { get; set; }
    public int EventId { get; set; }
    public DateTime RegistrationTime { get; set; }
    public string Status { get; set; }
}
