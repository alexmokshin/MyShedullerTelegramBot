using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramShedullerApp.Models.Commands
{
    public class HelloCommand : Command
    {
        public override string Name => "hello";


        public override async System.Threading.Tasks.Task Execute(Message message, TelegramBotClient client)
        {

            var chatId = message.Chat.Id;


            await client.SendTextMessageAsync(chatId: chatId, text: "Hello");
        }
    }
}
