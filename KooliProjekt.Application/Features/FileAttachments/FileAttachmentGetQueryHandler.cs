using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.FileAttachments;

public class FileAttachmentGetQueryHandler : IRequestHandler<FileAttachmentGetQuery, OperationResult<FileAttachmentDto>>
{
    private readonly ApplicationDbContext _dbContext;

    public FileAttachmentGetQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OperationResult<FileAttachmentDto>> Handle(FileAttachmentGetQuery request, CancellationToken cancellationToken)
    {
        var value = await _dbContext.FileAttachments.AsNoTracking()
            .Where(entity => entity.FileId == request.Id)
            .Select(entity => new FileAttachmentDto
            {
                EventTitle = entity.Event.Title,
                FileId = entity.FileId,
                EventId = entity.EventId,
                Filename = entity.Filename,
                FileType = entity.FileType,
                UploadTime = entity.UploadTime
            })
            .FirstOrDefaultAsync(cancellationToken);

        return new OperationResult<FileAttachmentDto>(value);
    }
}
