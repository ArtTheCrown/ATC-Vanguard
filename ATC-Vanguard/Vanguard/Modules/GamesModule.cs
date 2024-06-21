using ATC_Vanguard.Vanguard.others;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Animation;

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
        public async Task FindTheWords(CommandContext ctx, int level = 1)
        {
            if (level >= 7 || level <= 0)
            {
                await ctx.Channel.SendMessageAsync("**Select a level between `1 - 6`**");
            }
            else
            {
                var interactivity = ctx.Client.GetInteractivity();
                await ctx.TriggerTypingAsync();

                FindWordsScore scoreBoard = new FindWordsScore();
                scoreBoard.GetPlayer(ctx.User.Username);

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
                    Title = "**`Find valid english words from the text below:`**",
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


                            scoreBoard.GetPlayer(messageResult.Result.Author.Username).correct += 1;


                            await message.ModifyAsync(embed: GenerateUpdatedMessage(system, scoreBoard, PreservedList, GuessedWords).Build());


                            var emojiString = "<:pinkFlower:1252523784959819787>";

                            var customEmoji = DiscordEmoji.FromGuildEmote(Program.Client, ulong.Parse(emojiString.Split(':')[2].TrimEnd('>')));

                            await messageResult.Result.CreateReactionAsync(customEmoji);


                            DeleteMessage(messageResult, TimeSpan.FromSeconds(4));
                        }
                        else if (userInput == "!endgame")
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
                            scoreBoard.GetPlayer(messageResult.Result.Author.Username).incorrect += 1;


                            // await ctx.Channel.SendMessageAsync("Invalid word. Try again.");
                            await messageResult.Result.CreateReactionAsync(DiscordEmoji.FromName(Program.Client, ":x:"));
                            DeleteMessage(messageResult, TimeSpan.FromSeconds(1));
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

        [Command("guesswords")]
        public async Task GuessTheWords(CommandContext ctx, int level = 1)
        {
            if (level >= 7 || level <= 0)
            {
                await ctx.Channel.SendMessageAsync("**Select a level between `1 - 6`**");
            }
            else
            {
                var interactivity = ctx.Client.GetInteractivity();
                await ctx.TriggerTypingAsync();

                GuessWordsScore scoreBoard = new GuessWordsScore();
                scoreBoard.GetPlayer(ctx.User.Username);

                var MsgEmbed = new DiscordEmbedBuilder
                {
                    Title = $"**Loading..**",
                    Description = $"\n{scoreBoard.Score()}",
                    Color = DiscordColor.MidnightBlue
                };


                var message = await ctx.Channel.SendMessageAsync(embed: MsgEmbed);

                bool quit = false;

                while (true)
                {
                    
                    GuessWordsSystem system = new GuessWordsSystem(level);

                    var MsgEmbedEdited = new DiscordEmbedBuilder
                    {
                        Title = $"**`{system.Result}`**",
                        Description = $"\n{scoreBoard.Score()}",
                        Color = DiscordColor.Green
                    };

                    await message.ModifyAsync(embed: MsgEmbedEdited.Build());
                    

                    while (true)
                    {
                        var messageResult = await interactivity.WaitForMessageAsync(
                            x => x.Channel.Id == ctx.Channel.Id && !x.Author.IsBot,
                            TimeSpan.FromMinutes(5)
                        );


                        if (messageResult.Result.Content != null)
                        {
                            string userInput = messageResult.Result.Content.ToLower();


                            if (userInput == system.Word)
                            {                               
                                scoreBoard.GetPlayer(messageResult.Result.Author.Username).correct += 1;

                                var emojiString = "<:pinkFlower:1252523784959819787>";

                                var customEmoji = DiscordEmoji.FromGuildEmote(Program.Client, ulong.Parse(emojiString.Split(':')[2].TrimEnd('>')));

                                await messageResult.Result.CreateReactionAsync(customEmoji);


                                DeleteMessage(messageResult, TimeSpan.FromSeconds(4));
                                break;
                            }
                            else if (userInput == "!endgame")
                            {
                                await ctx.Channel.SendMessageAsync("**Exiting the game!..**");
                                quit = true;
                                break;
                            }
                            else if (userInput == "!helpgame")
                            {
                                await ctx.Channel.SendMessageAsync($"Hint: `{system.Word}` ");
                            }
                            else if (userInput == "!skipround")
                            {
                                break;
                            }
                            else
                            {
                                scoreBoard.GetPlayer(messageResult.Result.Author.Username).incorrect += 1;

                                await messageResult.Result.CreateReactionAsync(DiscordEmoji.FromName(Program.Client, ":x:"));
                                DeleteMessage(messageResult, TimeSpan.FromSeconds(1));
                            }
                        }
                        else
                        {
                            await ctx.Channel.SendMessageAsync("No response received within 2 minutes. Game ended.");
                            break;
                        }
                    }

                    if (quit) break;
                }
            }
        }



















        private DiscordEmbedBuilder GenerateUpdatedMessage(FindWordsGameSystem system, FindWordsScore scoreBoard, List<string> PreservedList, List<string> GuessedWords)
        {
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

            StringBuilder scoreStr = new StringBuilder();
            foreach (FindWordsPlayer player in scoreBoard.players)
            {
                string temp = $"**`{player.username}`**\n" +
                              $"score: `{player.correct}`\n" +
                              $"\n";
                scoreStr.Append(temp);
            }

            var modifiedMessage = new DiscordEmbedBuilder
            {
                Title = "**`Find valid english words from the text below:`**",
                Description = $"{system.Result}\n\n {str.ToString()}\n\n{scoreStr.ToString()}",
                Color = DiscordColor.Green
            };

            return modifiedMessage;
        }

        private async void DeleteMessage(InteractivityResult<DiscordMessage> ctx, TimeSpan timeSpan)
        {
            await Task.Delay(timeSpan);

            ctx.Result.DeleteAsync();
        }
    }
}
