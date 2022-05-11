using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace gbelenky.ToDo
{
    public static class GetToDo
    {
        [FunctionName("GetToDo")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string id = req.Query["id"];
            log.LogInformation($"getting content of ToDo Id: {id}");

            string responseMessage = string.IsNullOrEmpty(id)
                ? "Pass a ToDo id in the query string to get the ToDo content"
                : $"This is your ToDo id: {id} content ... not implemented yet";
            return new OkObjectResult(responseMessage);
        }
    }
}
