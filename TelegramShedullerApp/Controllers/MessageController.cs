using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TelegramShedullerApp.Models;
using Microsoft.Extensions.Options;
using TelegramShedullerApp.DB;
using Newtonsoft.Json;

namespace TelegramShedullerApp.Controllers
{
    [Route(@"api/message")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private MongoSettings mongoSettings;
       

        [HttpPost]
        [Route("CreateTask")]
        public async Task<OkObjectResult> CreateTask([FromBody] UserTask newUserTask)
        {

            OkObjectResult result = new OkObjectResult(newUserTask);

            

            MongoDbContext context = new DB.MongoDbContext(mongoSettings);

            

            try
            {
                await context.InsertNewTask(newUserTask);
                result.StatusCode = 200;
                return result;
                
            }
            catch (Exception)
            {
                result.StatusCode = -500;
                return result;
            }
        }

        [HttpGet]
        [Route("GetAllTasks")]
        public JsonResult GetAllTask(long id)
        {
            MongoDbContext context = new MongoDbContext(mongoSettings);
            
            try
            {
                var result = context.GetTasksForUser(id).Result;
                return new JsonResult(result);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new JsonResult(e.Message);
            }
        }

        public MessageController(IOptions<DB.MongoSettings> settings)
        {
            mongoSettings = settings.Value;
        }
    }
}
