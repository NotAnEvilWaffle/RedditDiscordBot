using DSharpPlus.CommandsNext;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Dropbox.Api;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading;

namespace DiscordBotV2.Commands
{
    class ArchiveModule : BaseCommandModule
    {

        CancellationTokenSource tokenSource = null;
        static string token = " ";

        ulong C_HOF = 0;
        ulong C_HOSD = 0;
        ulong C_NFBC = 0;

        [Command("archive")]
        public async Task ArchiveCommand(CommandContext ctx, DiscordChannel channel, int num)
        {
            var Owner = ctx.Guild.Owner;
            if (ctx.User == Owner)
            {
                DropboxClient dbx = new DropboxClient(token);
                tokenSource = new CancellationTokenSource();
                int timeout = 10000;

                Console.WriteLine("Connecting to DropBox...\n");


                var Messages = await channel.GetMessagesAsync(num);
                Console.WriteLine($"Fetching Messages from {channel}\n");

                using (WebClient client = new WebClient())
                {

                    

                    var length = Messages.Count;
                    var notiMSG = await channel.SendMessageAsync("Archiving is in Progress of this channel\nDo not use the bot until finished.");

                    var HOF = await ctx.Client.GetChannelAsync(C_HOF);
                    var HOSD = await ctx.Client.GetChannelAsync(C_HOSD);
                    var NFBC = await ctx.Client.GetChannelAsync(C_NFBC);

                    string folder = null;

                    if(channel == HOF)
                    {
                        folder = "";
                    }
                    else if (channel == HOSD)
                    {
                        folder = "";
                    }
                    else if (channel == NFBC)
                    {
                        folder = "";
                    }

                    int failCtr = 0;

                    foreach (var message in Messages)
                    {
                        if (message.Author == ctx.Client.CurrentUser)
                        {
                            Console.WriteLine("Found Message\n");

                            var URL = message.Embeds[0].Image.Url.ToUri();

                            Console.WriteLine(URL.ToString());
                            //TODO: Catch exception from Stream
                            try
                            {
                                using (Stream stream = client.OpenRead(URL))
                                {
                                    string uriSeg = URL.Segments[1];

                                    var response = await dbx.Files.UploadAsync(path: folder + uriSeg, body: stream);

                                    Console.WriteLine("Archiving: " + URL);


                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Could not upload this image");
                                failCtr++;
                                continue;
                                throw;
                            }
                            
                        }
                    }

                    var finMSG = await channel.SendMessageAsync($"Completed\n{failCtr} images failed to upload\nYou may resume normal bot usage");

                    await Task.Delay(3000);

                    await notiMSG.DeleteAsync();
                    await finMSG.DeleteAsync();
                }
            }
            

        }

    }
}
