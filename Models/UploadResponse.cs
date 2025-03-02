#nullable enable

using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;

namespace CalzedoniaHRFeed.Models
{
    public record UploadResponse(
        [Required] string JobId,
        [Required] string StatusCode,
        string? StatusMessage);
}