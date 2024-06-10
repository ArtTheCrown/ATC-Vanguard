using ATC_Vanguard.Vanguard.Modules;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Interactivity;
using System;
using System.Threading.Tasks;

namespace ATC_Vanguard.Vanguard
{
    public class Program
    {
        public static DiscordClient Client { get; set; }
        private static CommandsNextExtension Commands { get; set; }

        public static async Task Main()
        {
            var jsonReader = new JSONReader();
            await jsonReader.ReadJSON();

            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(discordConfig);


            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(2)
            });


            Client.Ready += Client_Ready;

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { jsonReader.prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = true,
                DmHelp = true
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<GeneralModule>();
            Commands.RegisterCommands<MathsModule>();


            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task Client_Ready(DiscordClient sender, ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
