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
    public static class UpdateToDo
    {
        [FunctionName("UpdateToDo")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = null)] HttpRequest req,
            ILogger log)
        {

            string id = req.Query["id"];
            log.LogInformation($"Updating ToDo Id: {id}");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string responseMessage = string.IsNullOrEmpty(id)
               ? "Pass a ToDo id in the query string to update the ToDo content"
                : $"This is your updated ToDo id: {id} content ... not implemented yet";

            return new OkObjectResult(responseMessage);
        }
    }
}
