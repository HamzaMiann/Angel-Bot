using System;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Google.Cloud.Language.V1;

namespace AngelBot
{
    class Program
    {
        private DiscordSocketClient client;
        private CommandService commands;
        private IServiceProvider services;
        private static string botToken = "";
        private static string credentials = "";
        public static List<string> cachedMessages = new List<string>();

        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        public async Task RunBotAsync()
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"..\..\token.txt");
            if ((line = file.ReadLine()) != null)
                botToken = line;
            if ((line = file.ReadLine()) != null)
                credentials = line;
            file.Close();

            //System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentials);
            //// The text to analyze.
            //string text = "Hello World!";
            //var client1 = LanguageServiceClient.Create();
            //var response = client1.AnalyzeSentiment(new Document()
            //{
            //    Content = text,
            //    Type = Document.Types.Type.PlainText
            //});
            //var sentiment = response.DocumentSentiment;
            //Console.WriteLine($"Score: {sentiment.ToString()}");
            //Console.WriteLine($"Magnitude: {sentiment.Magnitude}");


            client = new DiscordSocketClient();
            commands = new CommandService();

            services = new ServiceCollection().AddSingleton(client).AddSingleton(commands).BuildServiceProvider();

            client.Log += Log;

            await RegisterCommandsAsync();

            await client.LoginAsync(TokenType.Bot, botToken);

            await client.StartAsync();

            await Task.Delay(-1);

        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);

            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            client.MessageReceived += HandleCommandsAync;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandsAync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot) return;

            int argPos = 0;

            if (message.HasStringPrefix("god ", ref argPos) || message.HasStringPrefix("GOD ", ref argPos) || message.HasStringPrefix("God ", ref argPos))
            {
                var context = new SocketCommandContext(client, message);

                var result = await commands.ExecuteAsync(context, argPos, services);

                if (!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
            else
            {
                cachedMessages.Add(message.Author.Username.ToString() + ": " + message.ToString());
                if (cachedMessages.Count > 20)
                {
                    cachedMessages.RemoveAt(0);
                }
            }
        }
    }
}
