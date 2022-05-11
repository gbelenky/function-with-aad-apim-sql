using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json; 

namespace gbelenky.ToDo
{
    public static class AddToDo
    {
        [FunctionName("AddToDo")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log, 
             [Sql("dbo.ToDo", ConnectionStringSetting = "SqlConnectionString")] IAsyncCollector<ToDoItem> toDoItems)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ToDoItem toDoItem = JsonConvert.DeserializeObject<ToDoItem>(requestBody);

            // generate a new id for the todo item
            toDoItem.id = Guid.NewGuid();

            
            if (toDoItem.title == null)
            {
                toDoItem.title = "no title";
            }
            if (toDoItem.completed == null)
            {
                toDoItem.completed = false;
            }

            await toDoItems.AddAsync(toDoItem);
            await toDoItems.FlushAsync();
            List<ToDoItem> toDoItemList = new List<ToDoItem> { toDoItem };

            return new OkObjectResult(toDoItemList);
        }
    }
}
