# RedditDiscordBot

A quick new repo I made for an updated version of the bot without deprecated modules.

# How to Use

Just send ```.send``` in the main channel. There is a cooldown of 15 seconds per user

### Program.cs
- Replace the following:
  - The Reddit tokens with the appropriate ones for a bot account on Reddit
  - A Dropbox token
  - A Discord bot token (make sure it has proper message managing permissions)
  - The 4 ulong variables for the channel IDs in Discord (look up how to get these)
    - C_MAIN is the main channel where the bot reads messages
    - C_HOF is the "Hall of Fame" for top notch images the bot gets
      - ThumbsUp emoji sends to this channel
    - C_HODF is the "Hall of Somewhat Decent"
      - Ok hand sign sends this channel
    - C_MEH is for "Meh" images
      - Heart emoji sends to this channel
    - No Sign deletes it entirely
- Data1 class in this file can be used for extra info if you want

### ArchiveModule.cs
- Replace the following:
  - token string
    - This is the dropbox token for the account where the images will actually be stored (simple use the same token in Program.cs if you want)
  
### SendModule.cs
- Replace the following:
  -subreddit string
    - Needs to match the exact name of the subreddit without the r/
    
    
# Archiving

The images the bots sends is the full image link hosted by reddit, so deleted message probably won't show and will be stuck loading in Discord.
There is no way around this as far as I know besides simply storing the images somewhere else.
To use you simple need to run ```.archive #whatever-channel num-of-last-messages-to-archive```.
