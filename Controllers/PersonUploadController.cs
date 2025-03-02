using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using CalzedoniaHRFeed.Models;
using CalzedoniaHRFeed.Services;

namespace CalzedoniaHRFeed.Controllers
{
    [Route("v1/[controller]/{clientToken}")]
    [ApiController]
    public class PersonUploadController : ControllerBase
    {
        private readonly PersonProcessingService _service;

        public PersonUploadController(PersonProcessingService service)
        {
            _service = service;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<UploadResponse>> UploadPersons(string clientToken, [FromBody] UploadRequest request)
        {
            try
            {
                var response = await _service.EnqueueUploadAsync(clientToken, request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("status")]
        public async Task<ActionResult<JobStatusResponse>> GetJobStatus(string clientToken, [FromBody] JobStatusRequest request)
        {
            try
            {
                var response = await _service.GetJobStatusAsync(clientToken, request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}