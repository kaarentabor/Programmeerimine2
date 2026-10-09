using System;

namespace KooliProjekt.Application.Features.FileAttachments;

public class FileAttachmentDto
{
    public int FileId { get; set; }
    public int EventId { get; set; }
    public string Filename { get; set; }
    public string FileType { get; set; }
    public DateTime UploadTime { get; set; }
}
