using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC_Vanguard.Vanguard.Modules
{
    public class ModerationModule : BaseCommandModule
    {
        [Group("Moderation")]
        public class ModerationCommands
        {
            [Command("punish")]
            public async Task Punish(CommandContext ctx, string mention)
            {
                ctx.TriggerTypingAsync();

                // Check if the command issuer has administrator permissions
                if (!ctx.Member.IsOwner)
                {
                    await ctx.RespondAsync("**You dont have permission to use this.**");
                    return;
                }

                var user = (DiscordMember)ctx.Message.MentionedUsers.FirstOrDefault();

                var roles = user.Roles.ToList();
                foreach (var role in roles)
                {
                    await user.RevokeRoleAsync(role);
                }

                // Add the punished role to the user
                var punishedRole = ctx.Guild.GetRole(1250603278937358437);
                await user.GrantRoleAsync(punishedRole);

                await ctx.Channel.SendMessageAsync($"User [{user.Mention}] has been punished");
            }

        }
    }

    public class ModerationModuleSL : ApplicationCommandModule
    {
        [SlashCommand("punish", "Punish a user")]
        public async Task Punish(InteractionContext ctx,
                                        [Option("User", "User you want to punish")] DiscordMember user,
                                        [Option("Reason", "Reason for the punishment")] string reason = "None")
        {
            // Check if the command issuer has administrator permissions
            if (!ctx.Member.Permissions.HasFlag(Permissions.Administrator))
            {
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder()
                    .WithContent("You do not have the required permissions to use this command.")
                    .AsEphemeral(true));
                return;
            }

            // Defer the interaction to acknowledge it
            await ctx.DeferAsync();

            // Remove all roles from the user
            var roles = user.Roles.ToList();
            foreach (var role in roles)
            {
                await user.RevokeRoleAsync(role, reason);
            }

            // Add the punished role to the user
            var punishedRole = ctx.Guild.GetRole(1250603278937358437);
            await user.GrantRoleAsync(punishedRole, reason);

            // Send a response message
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder()
                .WithContent($"User {user.Mention} has been punished for: {reason}")
                .AsEphemeral(true));
        }
    }
}
