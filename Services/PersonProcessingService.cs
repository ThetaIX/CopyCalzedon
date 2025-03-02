using System.Threading.Channels;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using CalzedoniaHRFeed.Models;

namespace CalzedoniaHRFeed.Services
{
    public class PersonProcessingService
    {
        private readonly Channel<UploadRequest> _queue;
        private readonly Dictionary<string, JobStatusResponse> _jobStatuses = new();

        public PersonProcessingService()
        {
            _queue = Channel.CreateUnbounded<UploadRequest>();
            _ = ProcessQueueAsync();
        }

        public async Task<UploadResponse> EnqueueUploadAsync(string clientToken, UploadRequest request)
        {
            if (string.IsNullOrEmpty(clientToken) || clientToken != "valid-token")
                throw new UnauthorizedAccessException("Неверный токен");

            if (request.Persons.Any(p => string.IsNullOrEmpty(p.StaffId)))
                throw new ArgumentException("StaffId обязателен для всех персон");

            string jobId = Guid.NewGuid().ToString();
            _jobStatuses[jobId] = new JobStatusResponse("pending", null, null, request.Persons.Count);
            await _queue.Writer.WriteAsync(request);

            return new UploadResponse(jobId, "accepted", "Запрос принят");
        }

        public Task<JobStatusResponse> GetJobStatusAsync(string clientToken, JobStatusRequest request)
        {
            if (string.IsNullOrEmpty(clientToken) || clientToken != "valid-token")
                throw new UnauthorizedAccessException("Неверный токен");

            if (!_jobStatuses.TryGetValue(request.JobId, out var status))
                throw new KeyNotFoundException("Задание не найдено");

            return Task.FromResult(status);
        }

        private async Task ProcessQueueAsync()
        {
            await foreach (var request in _queue.Reader.ReadAllAsync())
            {
                string jobId = Guid.NewGuid().ToString(); // В реальном коде jobId берётся из Enqueue
                _jobStatuses[jobId] = new JobStatusResponse(
                    "processing",
                    "Upload is in progress.",
                    DateTime.UtcNow,
                    request.Persons.Count);

                var processedPersons = new List<ProcessedPerson>();
                foreach (var person in request.Persons)
                {
                    var processed = await ProcessPersonAsync(person);
                    processedPersons.Add(processed);
                }

                _jobStatuses[jobId] = new JobStatusResponse(
                    "finished",
                    "Обработка завершена",
                    _jobStatuses[jobId].StartedAt,
                    request.Persons.Count,
                    processedPersons);
            }
        }

        private async Task<ProcessedPerson> ProcessPersonAsync(Person person)
        {
            await Task.Delay(100); // Имитация обработки
            // Пример с ошибкой для демонстрации
            if (person.StaffId == "ID46563")
            {
                return new ProcessedPerson(
                    person.StaffId,
                    false,
                    "Ошибка загрузки данных в систему Corteos",
                    DateTime.UtcNow);
            }

            return new ProcessedPerson(
                person.StaffId,
                true,
                null,
                DateTime.UtcNow);
        }
    }
}