using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class MagezBot
    {
        private DiscordClient bot;

        public MagezBot()
        {
            bot = new DiscordClient();
            bot.MessageReceived += Bot_MessageReceived;

            bot.UsingCommands(x => {
                x.PrefixChar = '-';
                x.HelpMode = HelpMode.Public;
            });

            bot.GetService<CommandService>().CreateGroup("do", cgb =>
            {
                cgb.CreateCommand("greet")
                        .Alias(new string[] { "gr", "hi" })
                        .Description("Greets a person.")
                        .Parameter("GreetedPerson", ParameterType.Required)
                        .Do(async e =>
                        {
                            await e.Channel.SendMessage($"{e.User.Name} greets {e.GetArg("GreetedPerson")}");
                        });

                cgb.CreateCommand("bye")
                        .Alias(new string[] { "bb", "gb" })
                        .Description("Greets a person.")
                        .Parameter("GreetedPerson", ParameterType.Required)
                        .Do(async e =>
                        {
                            await e.Channel.SendMessage($"{e.User.Name} says goodbye to {e.GetArg("GreetedPerson")}");
                        });
            });

            bot.ExecuteAndWait(async () =>
            {
                await bot.Connect("MTg3NzQ1MDU1MjE2MjM4NTky.CjEitQ.svJ1w00pZxj5_VuURsIP8IMqXQ0");
            });
        
        }


    private void Bot_MessageReceived(object sender, MessageEventArgs e)
        {
            if (e.Message.IsAuthor) return;

            if (e.Message.Text == "-whoami")
            {
                e.Channel.SendMessage(e.User.Mention + " ```Magez-Bot v0.1.0```");
            }

            if (e.Message.Text == "-version")
            {
                e.Channel.SendMessage(e.User.Mention + " ``` Currently running Discord.Net v0.9.2 API Wrapper```");
            }

        }
    }
}
