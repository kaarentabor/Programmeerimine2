using System.Collections.Generic;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.FileAttachments;

public class FileAttachmentListQuery : IRequest<OperationResult<IList<FileAttachmentDto>>>
{
}
