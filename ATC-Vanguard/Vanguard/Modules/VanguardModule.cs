using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC_Vanguard.Vanguard.Modules
{
    public class VanguardModule : BaseCommandModule
    {
        [Command("h")]
        public async Task NoArguementCommands(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"Type `!help` to get help.");
        }
    }
}
