using System;

namespace KooliProjekt.Application.Features.Registrations;

public class RegistrationDto
{
    public int RegistrationId { get; set; }
    public int TransactionId { get; set; }
    public int EventId { get; set; }
    public DateTime RegistrationTime { get; set; }
    public string Status { get; set; }
}
