using Discord;
using Discord.Audio;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// https://discordapp.com/oauth2/authorize?client_id=188196397156139008&scope=bot&permissions=0 //

namespace DiscordBot
{
    public class PlebBot
    {
        private DiscordClient bot;

        public PlebBot()
        {
            bot = new DiscordClient();
            bot.MessageReceived += Bot_MessageReceived;
            bot.UserJoined += Bot_UserJoined;
            bot.UserLeft += Bot_UserLeft;

            bot.UsingCommands(x => {
                x.PrefixChar = '-';
                x.HelpMode = HelpMode.Public;
            });

            bot.GetService<CommandService>().CreateCommand("whoami")
                    .Description("Your status")
                    .Parameter("", ParameterType.Optional)
                    .Do(async e =>
                    {
                        await e.Channel.SendMessage($"{e.User.Mention}```\n UserID: {e.User.Id}\n Status: Online \n Connected to: {e.Server.Name}\n Participating in: {e.Channel.Name}```");
                    });


            bot.GetService<CommandService>().CreateCommand("whois")
                    .Description("User status")
                    .Parameter("anotherUser", ParameterType.Required)
                    .Do(async e =>
                    {
                        await e.Channel.SendMessage($"{e.User.Mention}\n UserID: {e.User.Id}\n Status: {e.User.Status}\n Connected to: {e.Server.Name}\n Participating in: {e.Channel.Name}");
                    });

            bot.GetService<CommandService>().CreateCommand("version")
                    .Description("Bot current version")
                    .Do(async e =>
                    {
                        await e.Channel.SendMessage($"```Pleb-Bot v0.1.0 | Running Discord.Net v0.9.2 API Wrapper | Developed by Jad```");
                    });

            bot.GetService<CommandService>().CreateCommand("ping")
                    .Description("ping? pong?")
                    .Do(async e =>
                    {
                        await e.Channel.SendMessage($"{e.User.Mention} pong");
                    });

            bot.GetService<CommandService>().CreateCommand("say")
                    .Description("Make Pleb say something")
                    .Parameter("Text", ParameterType.Unparsed)
                    .Do(async e =>
                    {
                        await e.Channel.SendMessage($"{e.GetArg("Text")}");
                    });

           bot.GetService<CommandService>().CreateCommand("invite")
                    .Description("Invite Pleb-Bot to your server")
                    .Do(async e =>
                    {
                        await e.User.SendMessage($"Click this link to give permission for Pleb-Bot to join your server! https://discordapp.com/oauth2/authorize?client_id=188196397156139008&scope=bot&permissions=0");
                    });


            bot.ExecuteAndWait(async () =>
            {
                await bot.Connect("MTg4MTk2NDE0MTkzNTMyOTI4.CjLHEQ.djsQz55XbnUfXouIyOBsM3Jmo80");
            });
        
        }

        private void Bot_UserLeft(object sender, UserEventArgs e)
        {
            e.Server.DefaultChannel.SendMessage(e.User.Name + " left the server!");
            Console.WriteLine(e.User.Name + " left the server");
        }

        private void Bot_UserJoined(object sender, UserEventArgs e)
        {
            e.Server.DefaultChannel.SendMessage(e.User.Mention + " joined the server!");
            Console.WriteLine(e.User.Name + " joined the server");
        }

        private void Bot_MessageReceived(object sender, MessageEventArgs e)
        {
            var Server = e.Server.Id;
            var Channel = e.Channel.Id;
            var UserName = e.User.Name;
            var UserId = e.User.Id;
            var Status = e.User.Status;

            Console.WriteLine(e.User.Status);

            if (e.Message.IsAuthor) return;


            if (e.Message.Text == "pleb")
            {
                e.Channel.SendMessage(e.User.Mention + " that's me!");
            }

            if (e.Message.Text == "jad")
            {
                e.Channel.SendMessage(e.User.Mention + " jad is a faggot");
            }

        }

        public async void SendAudio(string filePath)
        {
            Server Serv = bot.GetServer(168822291562627072);
            Channel voiceChannel = Serv.GetChannel(168822292078395392);

            bot.UsingAudio(x =>
            {
                x.Mode = AudioMode.Outgoing;
            });


        }

    }
}
