using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngelBot;
using System.Data;
using System.Threading;

namespace AngelBot
{
    public class Bless : ModuleBase<SocketCommandContext>
    {
        private static Dictionary<string, int> users = new Dictionary<string, int>();
        private static Random rand = new Random();
        private static Boolean inZone = false;

        [Command("bless")]
        public async Task BlessAsync(string name = null)
        {
            if (!String.IsNullOrWhiteSpace(name))
            {
                await ReplyAsync($"*Blesses {name}*");
            }
            else
            {
                await ReplyAsync("*God blesses*");
            }
        }

        [Command("damn")]
        public async Task DamnAsync(string name = null)
        {
            if (!String.IsNullOrWhiteSpace(name))
            {
                await ReplyAsync($"Damn {name}! >:c");
            }
            else
            {
                await ReplyAsync("Damn you too! >:c");
            }
        }

        [Command("why")]
        public async Task WhyAsync()
        {
            try
            {
                string message = Program.cachedMessages[(new Random()).Next(0, Program.cachedMessages.Count - 1)];
                await ReplyAsync($"God said it's because of this \"{message}\"");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Not enough messages for the 'why' command.");
                await ReplyAsync($"Because...");
            }
        }

        [Command("solve")]
        public async Task SolveAsync([Remainder] string equation = null)
        {
            if (String.IsNullOrWhiteSpace(equation))
            {
                await ReplyAsync("Solve what?");
            }
            else
            {
                try
                {
                    DataTable dt = new DataTable();
                    var v = dt.Compute(equation, "");
                    await ReplyAsync("God solved it! The answer is: " + v.ToString());
                }
                catch (Exception ex)
                {
                    await ReplyAsync("Your equation has some... \"errors\" in it...");
                }
            }
        }

        [Command("pray")]
        public async Task PrayAsync([Remainder] string prayer = null)
        {
            if (!string.IsNullOrWhiteSpace(prayer))
            {
                int percent = (new Random()).Next(0, 100);
                if (users.ContainsKey(Context.User.Username))
                {
                    int value = (users[Context.User.Username] + percent) / 2;
                    users[Context.User.Username] = value;
                }
                else
                {
                    users.Add(Context.User.Username, percent);
                }
                if (percent == 100)
                    await ReplyAsync("Congrats! Your prayer is GUARANTEED to come true!");
                else
                    await ReplyAsync($"Your prayer has about a {percent}% chance.");
            }
            else
            {
                await ReplyAsync("Pray for Paris?");
            }
        }

        [Command("status")]
        public async Task StatusAsync()
        {
            if (users.ContainsKey(Context.User.Username))
            {
                await ReplyAsync($"Status: {users[Context.User.Username]}% chance.");
            }
            else
            {
                users.Add(Context.User.Username, 50);
                await ReplyAsync("Status: 50% chance. You haven't prayed yet!");
            }
        }

        [Command("am")]
        public async Task AmAsync([Remainder] string question = null)
        {
            if (!string.IsNullOrWhiteSpace(question))
            {
                int i = rand.Next(1, 4);
                string s = "";
                switch (i)
                {
                    case 1: s = "Probably."; break;
                    case 2: s = "Who knows? Oh right, God does! Try checking your status."; break;
                    case 3: s = "Nah fam lol"; break;
                }
                await ReplyAsync(s);
            }
        }

        [Command("zone")]
        public async Task ZoneAsync()
        {
            await ReplyAsync("You have made a big mistake.");
            Zone();
        }

        private async Task Zone()
        {
            inZone = true;
            while (inZone)
            {
                await ReplyAsync("Don't let me into my zone...");
                await Task.Delay(2000);
            }
        }

        [Command("stop")]
        public async Task StopAsync()
        {
            if (inZone == true)
            {
                inZone = false;
                await ReplyAsync("I'm definitely in my zone.");
            }
        }
    }
}
