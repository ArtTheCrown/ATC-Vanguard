using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC_Vanguard.Vanguard.Modules
{
    public class MathsModule : BaseCommandModule
    {
        [Command("sum")]
        public async Task Add(CommandContext ctx, int num1, int num2)
        {
            int result = num1 + num2;

            await ctx.RespondAsync($"{result}");
        }

        [Command("subtract")]
        public async Task Subtract(CommandContext ctx, int num1, int num2)
        {
            int result = num1 - num2;

            await ctx.RespondAsync($"{result}");
        }
    }
}
