using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramShedullerApp.Models.Commands
{
    public class WriteCommand : Command
    {
        public override string Name => "write";
        private long ChatId { get; set; }
        private DateTime TaskDateTime { get; set; }
        private string TaskText { get; set; }


        public override async System.Threading.Tasks.Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;



            await client.SendTextMessageAsync(chatId: chatId, text: "Task complete added");
        }
    }
}
