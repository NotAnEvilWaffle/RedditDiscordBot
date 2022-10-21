using System;
using DSharpPlus.CommandsNext;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Reddit;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace DiscordBotV2.Commands
{
    public class SendModule : BaseCommandModule
    {

        ulong botChannel = 0;
        String subreddit = "mildlyinteresting";

        [Command("send"), Cooldown(1,7,CooldownBucketType.User)]
        public async Task Send(CommandContext ctx)
        {
            if (ctx.Message.ChannelId == botChannel)
            {
               
                var currentSubreddit = Program.reddit.Subreddit(subreddit).About();
                List<Reddit.Controllers.Post> topPosts = currentSubreddit.Posts.Hot;

                int r = Program.random.Next(0, 99);

                string permalink = topPosts[r].Permalink;
                string jsonLink = "https://www.reddit.com" + permalink + ".json";

                string link;

                using (WebClient wc = new WebClient())
                {
                    var json = wc.DownloadString(jsonLink);
                    //JArray obj = JArray.Parse(json);

                    dynamic res = JArray.Parse(json);

                    List<Rootobject> obj = JsonConvert.DeserializeObject<List<Rootobject>>(json);
                    // Console.WriteLine(res.data.children[0].data.url_overridden_by_dest);
                    link = obj[0].data.children[0].data.url_overridden_by_dest;

                }

                DiscordEmoji thumbUp = DiscordEmoji.FromName(ctx.Client,":thumbsup:");
                DiscordEmoji okHand = DiscordEmoji.FromName(ctx.Client, ":ok_hand:");
                DiscordEmoji noSign = DiscordEmoji.FromName(ctx.Client, ":no_entry_sign:");
                DiscordEmoji cuteHeart = DiscordEmoji.FromName(ctx.Client, ":cuteheart:");

                var message = await ctx.Message.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    ImageUrl = link,
                    Color = DiscordColor.Black


                }.Build());

                await message.CreateReactionAsync(thumbUp);
                await message.CreateReactionAsync(okHand);
                await message.CreateReactionAsync(noSign);
                await message.CreateReactionAsync(cuteHeart);
            }
        }



    }
}
