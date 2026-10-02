
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data;

public class Transaction
{
    [Key]
    public int TransactionId { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    [Range(typeof(decimal), "0", "99999999")]
    public decimal TotalPrice { get; set; }

    [Range(typeof(decimal), "0", "99999999")]
    public decimal PaidAmount { get; set; }

    [Required]
    public string PaymentMethod { get; set; } = string.Empty;

    public string? Summary { get; set; }

    public ICollection<Registration> Registrations { get; set; }
        = new List<Registration>();
}
