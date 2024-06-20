using ATC_Vanguard.Vanguard.others;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Microsoft.VisualBasic;
using System.Linq;
using System.Threading.Tasks;

namespace ATC_Vanguard.Vanguard.Modules
{
    public class GeneralModule : BaseCommandModule
    {
        [Command("hello")]
        [Cooldown(5, 30, CooldownBucketType.User)]
        public async Task SayHello(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"Hi.");
        }
    }
}
