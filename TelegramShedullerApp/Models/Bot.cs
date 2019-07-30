using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramShedullerApp.Models.Commands;

namespace TelegramShedullerApp.Models
{
    public static class Bot
    {
        private static TelegramBotClient client;
        private static List<Command> commandList;

        public static IReadOnlyList<Command> Commands { get => commandList.AsReadOnly(); }


        public static WebProxy GetProxy()
        {
            var proxy = new WebProxy();
            proxy.Address = new Uri($"http://103.89.253.246:3128");
            proxy.BypassProxyOnLocal = false;
            proxy.UseDefaultCredentials = true;
            return proxy;
        }

        //TODO: Переписать эту хуйню, чтобы коннект к базе поднимал
        public static async Task<TelegramBotClient> Get()
        {
            if (client != null)
            {
                return client;
            }
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.Proxy = GetProxy();

            var proxyClient = new HttpClient(httpClientHandler);
            

            commandList = new List<Command>();
            commandList.Add(new HelloCommand());
            //TODO: Add more commands eg /write /remind

            client = new TelegramBotClient(BotSettings.Key, proxyClient);

            //var hook = string.Format(BotSettings.Url, "api/message/update");
            //await client.SetWebhookAsync(hook);
            await client.SetWebhookAsync("");

            return client;
            

        }
    }
}
