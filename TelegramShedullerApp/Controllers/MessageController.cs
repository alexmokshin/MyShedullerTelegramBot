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
        /*[HttpPost]
        [Route("update")]
        public async Task<OkResult> Post([FromBody]Update update)
        {
            if (update == null) return Ok();
            var commands = Bot.Commands;
            var message = update.Message;
            var client = await Bot.Get();
            foreach(var command in commands)
            {
                if (command.Contains(message.Text))
                {
                    await command.Execute(message, client);
                    break;
                }
            }

            return Ok();
        } */

        [HttpPost]
        [Route("CreateTask")]
        public async Task<OkObjectResult> CreateTask([FromBody] Models.Task newTask)
        {
            
           // var p = new Models.Task();
           // var newTask = p.CreateTask(UserId, TaskText, TaskDate);
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
                //result.Value = ex.Message;
                return result;
            }
        }

        public MessageController(IOptions<TelegramShedullerApp.DB.MongoSettings> settings)
        {
            _options = settings;
        }
    }
}
