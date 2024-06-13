using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC_Vanguard.Vanguard.Modules
{
    [Group("Maths")]
    [Description("A module for commands related to Mathematics!")]
    public class MathsModule : BaseCommandModule
    {
        [Command("add")]
        public Task AddAsync(CommandContext context, int a, int b) => context.RespondAsync($"{a} + {b} = {a + b}");

        [Command("subtract")]
        public Task SubtractAsync(CommandContext context, int a, int b) => context.RespondAsync($"{a} - {b} = {a - b}");
    }
}
