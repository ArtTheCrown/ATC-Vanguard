using ATC_Vanguard.Vanguard.others;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace ATC_Vanguard.Vanguard.Modules
{
    public class GeneralModule : BaseCommandModule
    {
        [Command("hello")]
        public async Task SayHello(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"Hello, {ctx.User.Username}");
        }
    }
}
