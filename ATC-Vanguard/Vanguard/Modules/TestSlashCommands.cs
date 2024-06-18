using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC_Vanguard.Vanguard.Modules
{
    public class TestSlashCommands : ApplicationCommandModule
    {
        [SlashCommand("hello", "Bot says hello to a user")]
        public async Task Punish(InteractionContext ctx)
        {
            
            // Defer the interaction to acknowledge it
            await ctx.DeferAsync();

            // Send a response message
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder()
                .WithContent($"Hello, {ctx.Member.Nickname}")
                .AsEphemeral(true));
        }
    }
}
