using System.Collections.Generic;
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
            ILogger log,
             [Sql("select Id, title, completed from dbo.ToDo where @id = id", CommandType = System.Data.CommandType.Text, Parameters = "@id={Query.id}", ConnectionStringSetting = "SqlConnectionString")] IEnumerable<ToDoItem> toDo)
        {
            return new OkObjectResult(toDo);
        }
    }
}
