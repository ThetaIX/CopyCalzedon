#nullable enable

using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;

namespace CalzedoniaHRFeed.Models
{
    public record ProcessedPerson(
        string? StaffId,
        bool Succeeded,
        string? ErrorMessage,
        [Required] DateTime TimeStamp);
}