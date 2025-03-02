#nullable enable

using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;

namespace CalzedoniaHRFeed.Models
{
    public record JobStatusResponse(
        [Required] string RequestStatus,
        string? StatusMessage,
        DateTime? StartedAt,
        int TotalPersonsReceived,
        List<ProcessedPerson>? ProcessedPersons = null);
}