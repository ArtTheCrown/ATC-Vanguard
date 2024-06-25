using ATC_Vanguard.Vanguard.Modules;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Interactivity;
using System;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.SlashCommands;

namespace ATC_Vanguard.Vanguard
{
    public class Program
    {
        public static DiscordClient Client { get; set; }
        public static CommandsNextExtension Commands { get; set; }

        public static async Task Main()
        {
            var jsonReader = new JSONReader();
            await jsonReader.ReadJSON();

            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            Client = new DiscordClient(discordConfig);


            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(2)
            });


            Client.Ready += Client_Ready;
            Client.MessageCreated += Message_Created;

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { jsonReader.prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = true,
                DmHelp = false,
                IgnoreExtraArguments = true, 
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            // var slashCommands = Client.UseSlashCommands();
            ulong debugGuildId = 1220427226315620403;

            Commands.RegisterCommands<GeneralModule>();
            //Commands.RegisterCommands<ModerationModule>();
            Commands.RegisterCommands<GamesModule>();
            Commands.RegisterCommands<VanguardModule>();
            

            // slashCommands.RefreshCommands();
            // slashCommands.RegisterCommands<ModerationModuleSL>();
            // slashCommands.RegisterCommands<TestSlashCommands>();


            Commands.RegisterCommands<MathsModule>();
            Commands.RegisterCommands<UtilityModule>();

            

            Commands.CommandErrored += Commands_CommandErrored;


            


            await Client.ConnectAsync();

            await Task.Delay(-1);
        }

        private static Task Commands_CommandErrored(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            if(e.Exception is ChecksFailedException exception)
            {
                string timeLeft = string.Empty;
                foreach(var check in exception.FailedChecks)
                {
                    var coolDown = (CooldownAttribute)check;
                    timeLeft = coolDown.GetRemainingCooldown(e.Context).ToString(@"hh\:mm\:ss");
                }

                var CoolDownMessage = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Red,
                    Title = "Please wait for the cooldown to end",
                    Description = $"Time: {timeLeft}"
                };

                e.Context.RespondAsync(embed: CoolDownMessage);
            }
            return Task.CompletedTask;
        }

        private static Task Message_Created(DiscordClient sender, MessageCreateEventArgs e)
        {
            return Task.CompletedTask;
        }

        private static Task Client_Ready(DiscordClient sender, ReadyEventArgs e)
        {
            //sender.UpdateStatusAsync(new DiscordActivity("!help"), UserStatus.DoNotDisturb);

            sender.UpdateStatusAsync(new DiscordActivity("雷電様の命令に", ActivityType.ListeningTo), UserStatus.DoNotDisturb);

            return Task.CompletedTask;
        }
    }
}
