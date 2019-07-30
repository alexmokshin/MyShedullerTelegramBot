using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramShedullerApp.Models.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract System.Threading.Tasks.Task Execute(Message message, TelegramBotClient client);

        public bool Contains(string command)
        {
            return command.Contains(this.Name);
        }
    }
}
