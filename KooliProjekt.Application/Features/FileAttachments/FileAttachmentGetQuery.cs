using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.FileAttachments;

public class FileAttachmentGetQuery : IRequest<OperationResult<FileAttachmentDto>>
{
    public int Id { get; set; }

}
