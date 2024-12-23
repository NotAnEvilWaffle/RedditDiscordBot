﻿using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.CommandsNext;

using Reddit;

using System.Threading.Tasks;
using System;

using DiscordBotV2.Commands;
using Dropbox.Api;

namespace DiscordBotV2
{
    public class Program
    {
        // Reddit tokens
        static string appID = "";
        static string refreshToken = "";
        static string appSecret = "";

        // Dropbox Token
        static string dbToken = "";

        // Discord Token
        static string discordToken = "";

        // Channel IDs in Discord
        static ulong C_GFFC = 0;
        static ulong C_HOF = 0;
        static ulong C_HOSD = 0;
        static ulong C_NFBC = 0;



        public static Random random = new Random();
        public static RedditClient reddit = new RedditClient(appId: appID, refreshToken: refreshToken, appSecret: appSecret);





        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {


            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = discordToken,
                TokenType = TokenType.Bot


            });
            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] {"."}


            });

            commands.RegisterCommands<SendModule>();
            commands.RegisterCommands<ArchiveModule>();




            discord.Ready += SetStatic;
            discord.MessageReactionAdded += SendToHall;




            await discord.ConnectAsync();
            await Task.Delay(-1);


        }

        static async Task Run()
        {
            using(var dbx = new DropboxClient(dbToken)) 
            {
                var full = await dbx.Users.GetCurrentAccountAsync();
                Console.WriteLine("Connected to {0}", full.Name.DisplayName);
            }
        }

        private static async Task SetStatic(DiscordClient c, ReadyEventArgs s)
        {

            DiscordClient discord = c;

            DiscordActivity activity = new DiscordActivity("Just a test dont mind me", ActivityType.Streaming);

            await discord.UpdateStatusAsync(activity);


        }

        private static async Task SendToHall(DiscordClient c, MessageReactionAddEventArgs s)
        {

            DiscordEmoji thumbUp = DiscordEmoji.FromName(c, ":thumbsup:");
            DiscordEmoji okHand = DiscordEmoji.FromName(c, ":ok_hand:");
            DiscordEmoji noSign = DiscordEmoji.FromName(c, ":no_entry_sign:");
            DiscordEmoji cuteHeart = DiscordEmoji.FromName(c, ":cuteheart:");

            var MainChannel = await c.GetChannelAsync(C_GFFC);
            var HOF = await c.GetChannelAsync(C_HOF);
            var HOSD = await c.GetChannelAsync(C_HOSD);
            var MEH = await c .GetChannelAsync(C_NFBC);

            var messageBuilder = new DiscordMessageBuilder
            {
                Embed = s.Message.Embeds[0]
            };

            if (s.User != c.CurrentUser)
            {
                if(s.Emoji == thumbUp)
                {
                    await MainChannel.SendMessageAsync("Sent to Hall of Fame");
                    await messageBuilder.SendAsync(HOF);
                }
                if(s.Emoji == okHand)
                {
                    await MainChannel.SendMessageAsync("Sent to Hall of Somewhat Decent");
                    await messageBuilder.SendAsync(HOSD);
                }
                if(s.Emoji == noSign)
                {
                    await MainChannel.SendMessageAsync("The World Has Been Cleansed Once More");
                    await s.Message.DeleteAsync();
                }
                if(s.Emoji == cuteHeart)
                {
                    await MainChannel.SendMessageAsync("Good Enough I Guess");
                    await messageBuilder.SendAsync(MEH);
                }


            }




        }

        


    }

        
}


    public class Rootobject
    {
        public string kind { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string modhash { get; set; }
        public int? dist { get; set; }
        public Child[] children { get; set; }
        public object after { get; set; }
        public object before { get; set; }
    }

    public class Child
    {
        public string kind { get; set; }
        public Data1 data { get; set; }
    }

    public class Data1
    {
        public object approved_at_utc { get; set; }
        public string subreddit { get; set; }
        public string selftext { get; set; }
        public string author_fullname { get; set; }
        public bool saved { get; set; }
        public object mod_reason_title { get; set; }
        public int gilded { get; set; }
        public bool clicked { get; set; }
        public string title { get; set; }
        public Link_Flair_Richtext[] link_flair_richtext { get; set; }
        public string subreddit_name_prefixed { get; set; }
        public bool hidden { get; set; }
        public object pwls { get; set; }
        public string link_flair_css_class { get; set; }
        public int downs { get; set; }
        public int thumbnail_height { get; set; }
        public object top_awarded_type { get; set; }
        public bool hide_score { get; set; }
        public string name { get; set; }
        public bool quarantine { get; set; }
        public string link_flair_text_color { get; set; }
        public float upvote_ratio { get; set; }
        public object author_flair_background_color { get; set; }
        public string subreddit_type { get; set; }
        public int ups { get; set; }
        public int total_awards_received { get; set; }
        public Media_Embed media_embed { get; set; }
        public int thumbnail_width { get; set; }
        public object author_flair_template_id { get; set; }
        public bool is_original_content { get; set; }
        public object[] user_reports { get; set; }
        public object secure_media { get; set; }
        public bool is_reddit_media_domain { get; set; }
        public bool is_meta { get; set; }
        public object category { get; set; }
        public Secure_Media_Embed secure_media_embed { get; set; }
        public string link_flair_text { get; set; }
        public bool can_mod_post { get; set; }
        public int score { get; set; }
        public object approved_by { get; set; }
        public bool author_premium { get; set; }
        public string thumbnail { get; set; }
        public bool edited { get; set; }
        public object author_flair_css_class { get; set; }
        public object[] author_flair_richtext { get; set; }
        public Gildings gildings { get; set; }
        public string post_hint { get; set; }
        public object content_categories { get; set; }
        public bool is_self { get; set; }
        public object mod_note { get; set; }
        public float created { get; set; }
        public string link_flair_type { get; set; }
        public object wls { get; set; }
        public object removed_by_category { get; set; }
        public object banned_by { get; set; }
        public string author_flair_type { get; set; }
        public string domain { get; set; }
        public bool allow_live_comments { get; set; }
        public object selftext_html { get; set; }
        public object likes { get; set; }
        public string suggested_sort { get; set; }
        public object banned_at_utc { get; set; }
        public string url_overridden_by_dest { get; set; }
        public object view_count { get; set; }
        public bool archived { get; set; }
        public bool no_follow { get; set; }
        public bool is_crosspostable { get; set; }
        public bool pinned { get; set; }
        public bool over_18 { get; set; }
        public Preview preview { get; set; }
        public All_Awardings[] all_awardings { get; set; }
        public object[] awarders { get; set; }
        public bool media_only { get; set; }
        public string link_flair_template_id { get; set; }
        public bool can_gild { get; set; }
        public bool spoiler { get; set; }
        public bool locked { get; set; }
        public object author_flair_text { get; set; }
        public object[] treatment_tags { get; set; }
        public bool visited { get; set; }
        public object removed_by { get; set; }
        public object num_reports { get; set; }
        public object distinguished { get; set; }
        public string subreddit_id { get; set; }
        public object mod_reason_by { get; set; }
        public object removal_reason { get; set; }
        public string link_flair_background_color { get; set; }
        public string id { get; set; }
        public bool is_robot_indexable { get; set; }
        public object report_reasons { get; set; }
        public string author { get; set; }
        public object discussion_type { get; set; }
        public int num_comments { get; set; }
        public bool send_replies { get; set; }
        public object whitelist_status { get; set; }
        public bool contest_mode { get; set; }
        public object[] mod_reports { get; set; }
        public bool author_patreon_flair { get; set; }
        public object author_flair_text_color { get; set; }
        public string permalink { get; set; }
        public object parent_whitelist_status { get; set; }
        public bool stickied { get; set; }
        public string url { get; set; }
        public int subreddit_subscribers { get; set; }
        public float created_utc { get; set; }
        public int num_crossposts { get; set; }
        public object media { get; set; }
        public bool is_video { get; set; }
    }

    public class Media_Embed
    {
    }

    public class Secure_Media_Embed
    {
    }

    public class Gildings
    {
        public int gid_1 { get; set; }
    }

    public class Preview
    {
        public Image[] images { get; set; }
        public bool enabled { get; set; }
    }

    public class Image
    {
        public Source source { get; set; }
        public Resolution2[] resolutions { get; set; }
        public Variants variants { get; set; }
        public string id { get; set; }
    }

    public class Source
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Variants
    {
        public Obfuscated obfuscated { get; set; }
        public Nsfw nsfw { get; set; }
    }

    public class Obfuscated
    {
        public Source1 source { get; set; }
        public Resolution[] resolutions { get; set; }
    }

    public class Source1
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Resolution
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Nsfw
    {
        public Source2 source { get; set; }
        public Resolution1[] resolutions { get; set; }
    }

    public class Source2
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Resolution1
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Resolution2
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Link_Flair_Richtext
    {
        public string e { get; set; }
        public string t { get; set; }
    }

    public class All_Awardings
    {
        public int? giver_coin_reward { get; set; }
        public object subreddit_id { get; set; }
        public bool is_new { get; set; }
        public int days_of_drip_extension { get; set; }
        public int coin_price { get; set; }
        public string id { get; set; }
        public int? penny_donate { get; set; }
        public string award_sub_type { get; set; }
        public int coin_reward { get; set; }
        public string icon_url { get; set; }
        public int days_of_premium { get; set; }
        public object tiers_by_required_awardings { get; set; }
        public Resized_Icons[] resized_icons { get; set; }
        public int icon_width { get; set; }
        public int static_icon_width { get; set; }
        public object start_date { get; set; }
        public bool is_enabled { get; set; }
        public object awardings_required_to_grant_benefits { get; set; }
        public string description { get; set; }
        public object end_date { get; set; }
        public int subreddit_coin_reward { get; set; }
        public int count { get; set; }
        public int static_icon_height { get; set; }
        public string name { get; set; }
        public Resized_Static_Icons[] resized_static_icons { get; set; }
        public string icon_format { get; set; }
        public int icon_height { get; set; }
        public int? penny_price { get; set; }
        public string award_type { get; set; }
        public string static_icon_url { get; set; }
    }

    public class Resized_Icons
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Resized_Static_Icons
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }



