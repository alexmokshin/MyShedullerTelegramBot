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

namespace TelegramShedullerApp.Controllers
{
    [Route(@"api/message")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private MongoSettings mongoSettings;
       

        [HttpPost]
        [Route("CreateTask")]
        public async Task<OkObjectResult> CreateTask([FromBody] Models.Task newTask)
        {

            OkObjectResult result = new OkObjectResult(newTask);

            if (newTask.CheckOnNull())
            {
                result.StatusCode = 400;
                return result;
            }

            MongoDbContext context = new DB.MongoDbContext(mongoSettings);

            

            try
            {
                await context.InsertNewTask(newTask);
                result.StatusCode = 200;
                return result;
                
            }
            catch (Exception)
            {
                result.StatusCode = -500;
                return result;
            }
        }

        public MessageController(IOptions<DB.MongoSettings> settings)
        {
            mongoSettings = settings.Value;
        }
    }
}
