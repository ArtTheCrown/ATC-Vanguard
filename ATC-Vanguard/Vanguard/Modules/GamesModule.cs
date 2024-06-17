using ATC_Vanguard.Vanguard.others;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC_Vanguard.Vanguard.Modules
{
    [Group("games")]
    public class GamesModule : BaseCommandModule
    {
        [Command("cardgame")]
        public async Task CardGame(CommandContext ctx)
        {
            ctx.TriggerTypingAsync();

            var userCard = new CardSystem();

            var botCard = new CardSystem();

            if (userCard.SelectedNumber > botCard.SelectedNumber)
            {
                // user wins 
                var winMessage = new DiscordEmbedBuilder
                {
                    Title = "Congratulations, You Won!",
                    Color = DiscordColor.Green,
                    Description = $"**Your card is `{userCard.SelectedCard}`\nBot drew a `{botCard.SelectedCard}`**"
                };

                await ctx.Channel.SendMessageAsync(embed: winMessage);
            }
            else if (userCard.SelectedNumber == botCard.SelectedNumber)
            {
                // user wins 
                var winMessage = new DiscordEmbedBuilder
                {
                    Title = "Its a Tie!",
                    Color = DiscordColor.Orange,
                    Description = $"**Your card is `{userCard.SelectedCard}`\nBot drew a `{botCard.SelectedCard}`**"
                };
            }
            else
            {
                // bot wins
                var loseMessage = new DiscordEmbedBuilder
                {
                    Title = "You Lost",
                    Color = DiscordColor.Red,
                    Description = $"**Your card is `{userCard.SelectedCard}`\nBot drew a `{botCard.SelectedCard}`**"
                };

                await ctx.Channel.SendMessageAsync(embed: loseMessage);
            }
        }

        [Command("findwords")]
        public async Task FindTheWords(CommandContext ctx, int level)
        {
            var interactivity = ctx.Client.GetInteractivity();
            await ctx.TriggerTypingAsync();


            FindWordsGameSystem system = new FindWordsGameSystem(level);

            List<string> validWords = system.ValidWords;

            List<string> PreservedList = new List<string>();

            foreach (string word in validWords)
            {
                PreservedList.Add(word);
            }

            List<string> GuessedWords = new List<string>();


            var gibrishEmbed = new DiscordEmbedBuilder
            {
                Title = "Find all the valid words from the text below:",
                Description = $"{system.Result}\n\n ||{string.Join("|| ||", PreservedList)}||",
                Color = DiscordColor.Green
            };


            var message = await ctx.Channel.SendMessageAsync(embed: gibrishEmbed);

            int score = 0;


            while (score < PreservedList.Count)
            {
                var messageResult = await interactivity.WaitForMessageAsync(
                    x => x.Channel.Id == ctx.Channel.Id && !x.Author.IsBot,
                    TimeSpan.FromMinutes(5)
                );


                if (messageResult.Result.Content != null)
                {
                    string userInput = messageResult.Result.Content.ToLower();


                    if (validWords.Contains(userInput))
                    {
                        validWords.RemoveAll(word => word.Equals(userInput));
                        GuessedWords.Add(userInput);
                        score++;
                        //await ctx.Channel.SendMessageAsync($"Correct! Score: {score} Remaining: {validWords.Count}");

                        StringBuilder str = new StringBuilder();
                        foreach (string word in PreservedList)
                        {
                            if (GuessedWords.Contains(word))
                            {
                                str.Append($"{word} ");
                            }
                            else
                            {
                                str.Append($"||{word}|| ");
                            }
                        }

                        var modifiedMessage = new DiscordEmbedBuilder
                        {
                            Title = "Find all the valid words from the text below:",
                            Description = $"{system.Result}\n\n {str.ToString()}",
                            Color = DiscordColor.Green
                        };

                        await message.ModifyAsync(embed: modifiedMessage.Build());
                        await messageResult.Result.CreateReactionAsync(DiscordEmoji.FromName(Program.Client, ":white_check_mark:"));
                    }
                    else if (userInput == "!ahriquitgame")
                    {
                        await ctx.Channel.SendMessageAsync("**Exiting the game!..**");
                        break;
                    }
                    else if (userInput == "!ahrihelp")
                    {
                        await ctx.Channel.SendMessageAsync($"Valid words: {string.Join(", ", validWords)}");
                    }
                    else
                    {
                        // await ctx.Channel.SendMessageAsync("Invalid word. Try again.");
                        await messageResult.Result.CreateReactionAsync(DiscordEmoji.FromName(Program.Client, ":x:"));
                    }
                }
                else
                {
                    await ctx.Channel.SendMessageAsync("No response received within 2 minutes. Game ended.");
                    break;
                }
            }  
        }
    }
}
