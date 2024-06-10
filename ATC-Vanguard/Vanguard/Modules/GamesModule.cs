using ATC_Vanguard.Vanguard.others;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC_Vanguard.Vanguard.Modules
{
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
    }
}
