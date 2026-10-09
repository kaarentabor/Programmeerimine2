using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.FileAttachments;

public class FileAttachmentListQueryHandler : IRequestHandler<FileAttachmentListQuery, OperationResult<IList<FileAttachmentDto>>>
{
    private readonly ApplicationDbContext _dbContext;

    public FileAttachmentListQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OperationResult<IList<FileAttachmentDto>>> Handle(FileAttachmentListQuery request, CancellationToken cancellationToken)
    {
        var value = await _dbContext.FileAttachments.AsNoTracking()
            .OrderBy(entity => entity.FileId)
            .Select(entity => new FileAttachmentDto
            {
                FileId = entity.FileId,
                EventId = entity.EventId,
                Filename = entity.Filename,
                FileType = entity.FileType,
                UploadTime = entity.UploadTime
            })
            .ToListAsync(cancellationToken);

        return new OperationResult<IList<FileAttachmentDto>>(value);
    }
}
