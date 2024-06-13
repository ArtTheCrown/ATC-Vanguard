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
        [Group("math")]
        public class MathCommands
        {
            [Command("add")]
            public Task AddAsync(CommandContext context, int a, int b) => context.RespondAsync($"{a} + {b} = {a + b}");

            [Command("subtract")]
            public Task SubtractAsync(CommandContext context, int a, int b) => context.RespondAsync($"{a} - {b} = {a - b}");
        }
    }
}
