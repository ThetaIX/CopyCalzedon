#nullable enable

using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;

namespace CalzedoniaHRFeed.Models
{
    public record UploadRequest(
            [Required, MinLength(1)] List<Person> Persons);
}