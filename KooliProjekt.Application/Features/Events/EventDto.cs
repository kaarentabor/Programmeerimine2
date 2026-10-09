using System;

namespace KooliProjekt.Application.Features.Events;

public class EventDto
{
    public int EventId { get; set; }
    public int ClientId { get; set; }
    public int EventTypeId { get; set; }
    public string InvoiceNumber { get; set; }
    public decimal Summary { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string EventPlace { get; set; }
    public string Status { get; set; }
    public string Title { get; set; }
}
