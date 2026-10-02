
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data;

public class Event
{
    [Key]
    public int EventId { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public int EventTypeId { get; set; }

    [Required]
    [StringLength(50)]
    public string InvoiceNumber { get; set; } = string.Empty;

    [Range(typeof(decimal), "0", "99999999")]
    public decimal Summary { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [Required]
    [StringLength(150)]
    public string EventPlace { get; set; } = string.Empty;

    [Required]
    [StringLength(30)]
    public string Status { get; set; } = string.Empty;

    [Required]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;

    public ICollection<Registration> Registrations { get; set; }
        = new List<Registration>();

    public ICollection<Payment> Payments { get; set; }
        = new List<Payment>();

    public ICollection<FileAttachment> Files { get; set; }
        = new List<FileAttachment>();
}
