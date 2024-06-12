using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Entities;

namespace ATC_Vanguard.Vanguard.Modules
{
    internal class UtilityModule : BaseCommandModule
    {
        [Command("pollv4")]
        public async Task PollV4(CommandContext ctx, string op1, string op2, string op3, string op4, [RemainingText] string pollTitle)
        {
            var interactivity = Program.Client.GetInteractivity();
            var pollTime = TimeSpan.FromSeconds(10);

            DiscordEmoji[] emojiOptions = { DiscordEmoji.FromName(Program.Client, ":one:"),
                                            DiscordEmoji.FromName(Program.Client, ":two:"),
                                            DiscordEmoji.FromName(Program.Client, ":three:"),
                                            DiscordEmoji.FromName(Program.Client, ":four:") };

            string optionsDescription = $"{emojiOptions[0]} | {op1}\n" +
                                        $"{emojiOptions[1]} | {op2}\n" +
                                        $"{emojiOptions[2]} | {op3}\n" +
                                        $"{emojiOptions[3]} | {op4}\n";

            var pollMessage = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Red,
                Title = pollTitle,
                Description = optionsDescription
            };

            var sentPoll = await ctx.Channel.SendMessageAsync(embed: pollMessage);

            foreach (var emoji in emojiOptions)
            {
                await sentPoll.CreateReactionAsync(emoji);
            }

            var totalReactions = await interactivity.CollectReactionsAsync(sentPoll, pollTime);

            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            int count4 = 0;

            foreach(var emoji in totalReactions)
            {
                if (emoji.Emoji == emojiOptions[0]) count1++;
                if (emoji.Emoji == emojiOptions[1]) count2++;
                if (emoji.Emoji == emojiOptions[2]) count3++;
                if (emoji.Emoji == emojiOptions[3]) count4++;
            }

            int totalVotes = count1 + count2 + count3 + count4;
            
            string resultDescription = $"{emojiOptions[0]} | {count1}\n" +
                                        $"{emojiOptions[1]} | {count2}\n" +
                                        $"{emojiOptions[2]} | {count3}\n" +
                                        $"{emojiOptions[3]} | {count4}\n" +
                                        $"Total votes: {totalVotes}";

            var resultEmbed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Green,
                Title = "Results of the poll",
                Description = resultDescription
            };

            await ctx.Channel.SendMessageAsync(embed: resultEmbed);
        }
        
        [Command("pollv3")]
        public async Task PollV3(CommandContext ctx, string op1, string op2, string op3, [RemainingText] string pollTitle)
        {
            var interactivity = Program.Client.GetInteractivity();
            var pollTime = TimeSpan.FromSeconds(10);

            DiscordEmoji[] emojiOptions = { DiscordEmoji.FromName(Program.Client, ":one:"),
                                            DiscordEmoji.FromName(Program.Client, ":two:"),
                                            DiscordEmoji.FromName(Program.Client, ":three:") };

            string optionsDescription = $"{emojiOptions[0]} | {op1}\n" +
                                        $"{emojiOptions[1]} | {op2}\n" +
                                        $"{emojiOptions[2]} | {op3}\n";

            var pollMessage = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Red,
                Title = pollTitle,
                Description = optionsDescription
            };

            var sentPoll = await ctx.Channel.SendMessageAsync(embed: pollMessage);

            foreach (var emoji in emojiOptions)
            {
                await sentPoll.CreateReactionAsync(emoji);
            }

            var totalReactions = await interactivity.CollectReactionsAsync(sentPoll, pollTime);

            int count1 = 0;
            int count2 = 0;
            int count3 = 0;

            foreach (var emoji in totalReactions)
            {
                if (emoji.Emoji == emojiOptions[0]) count1++;
                if (emoji.Emoji == emojiOptions[1]) count2++;
                if (emoji.Emoji == emojiOptions[2]) count3++;
            }

            int totalVotes = count1 + count2 + count3;

            string resultDescription = $"{emojiOptions[0]} | {count1}\n" +
                                        $"{emojiOptions[1]} | {count2}\n" +
                                        $"{emojiOptions[2]} | {count3}\n" +
                                        $"Total votes: {totalVotes}";

            var resultEmbed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Green,
                Title = "Results of the poll",
                Description = resultDescription
            };

            await ctx.Channel.SendMessageAsync(embed: resultEmbed);
        }
    }
}
