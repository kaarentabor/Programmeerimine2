
using System;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data;

public class FileAttachment
{
    [Key]
    public int FileId { get; set; }

    public int EventId { get; set; }
    public Event Event { get; set; } = null!;

    [Required]
    [StringLength(255)]
    public string Filename { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string FileType { get; set; } = string.Empty;

    public DateTime UploadTime { get; set; }
}
