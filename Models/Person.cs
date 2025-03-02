#nullable enable

using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;

namespace CalzedoniaHRFeed.Models
{
    public record PersonalCode(
            [Required] string Dictionary,
            [Required] string Value);


    public record Person(
        [Required] bool IsActive,
        [Required] string StaffId,
        [Required] string LastName,
        [Required] string FirstName,
        string? MiddleName = null,
        List<PersonalCode>? PersonalCodes = null);
}