using Azure.Storage.Queues;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzDemo.Function
{
    public class MessageSender
    {
        private readonly ILogger<MessageSender> _logger;
        private readonly QueueClient _queueClient;

        public MessageSender(ILogger<MessageSender> logger, QueueClient queueClient)
        {
            _logger = logger;
            _queueClient = queueClient;
        }


        [Function("MessageSender")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            _logger.LogInformation($"Message sent");

            await _queueClient.CreateIfNotExistsAsync();
            await _queueClient.SendMessageAsync(requestBody);


            return new OkResult();
        }
    }
}
