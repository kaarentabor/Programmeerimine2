
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data;

public class Payment
{
    [Key]
    public int PaymentId { get; set; }

    public int RegistrationId { get; set; }
    public Registration Registration { get; set; } = null!;

    public int EventId { get; set; }
    public Event Event { get; set; } = null!;

    public int InvoiceId { get; set; }

    [Range(typeof(decimal), "0", "99999999")]
    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    [Required]
    [StringLength(30)]
    public string Status { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string PaymentMethod { get; set; } = string.Empty;
}
