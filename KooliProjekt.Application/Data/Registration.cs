
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data;

public class Registration
{
    [Key]
    public int RegistrationId { get; set; }

    public int TransactionId { get; set; }
    public Transaction Transaction { get; set; } = null!;

    public int EventId { get; set; }
    public Event Event { get; set; } = null!;

    public DateTime RegistrationTime { get; set; }

    [Required]
    [StringLength(30)]
    public string Status { get; set; } = string.Empty;

    public ICollection<Payment> Payments { get; set; }
        = new List<Payment>();
}
