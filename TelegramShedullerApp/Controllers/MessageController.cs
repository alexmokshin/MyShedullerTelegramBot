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

namespace TelegramShedullerApp.Controllers
{
    [Route(@"api/message")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IOptions<TelegramShedullerApp.DB.MongoSettings> _options;

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

            TelegramShedullerApp.DB.MongoDbContext context = new DB.MongoDbContext();

            

            try
            {
                await context.InsertNewTask(newTask, _options);
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
            _options = settings;
        }
    }
}
