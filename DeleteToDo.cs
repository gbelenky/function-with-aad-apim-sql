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
    public static class DeleteToDo
    {
        [FunctionName("DeleteToDo")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)] HttpRequest req,
            ILogger log)
        {           
            string id = req.Query["id"];
            log.LogInformation($"Deleting ToDo Id: {id}");

           string responseMessage = string.IsNullOrEmpty(id)
                ? "Pass a ToDo id in the query string to delete the ToDo content"
                : $"This is your ToDo id: {id} to be deleted ... not implemented yet";
            return new OkObjectResult(responseMessage);
        }
    }
}
