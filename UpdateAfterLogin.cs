using Telegram.Bot.Types;
using Telegram.Bot;
using System.Diagnostics;

namespace UpdateACI {
    public static class UpdateAfterLogin {
        async public static Task UpdateAfLog(ITelegramBotClient client, Update update, CancellationToken token, Message message) {
            Console.WriteLine($"{message.Chat.Username} | {message.Text}");
            var inputString = message.Text;

            switch (inputString) {
                case "/help":
                    UpDate.SendMes(client, message, $"Commands I can run:\n" +
                            $"\nGoogle:" +
                            $"\n/open_google - open Google" +
                            $"\n/close_google - Close Google" +
                            $"\n/open_youtube - open YouTube" +
                            $"\n/open_video_by_name - open YouTube by video name" +
                            $"\n/open_ya_mus - open Yandex Music" +
                            $"\n/open_vk - open Vkontakte" +
                            $"\n/open_url - open the url you reset\n" +

                            $"\nSteam:" +
                            $"\n/open_steam - open Steam" +
                            $"\n/close_steam - Close Steam\n" +

                            $"\nFaceit:" +
                            $"\n/open_faceit - open Faceit" +
                            $"\n/close_faceit - Close Faceit\n" +

                            $"\nDiscord:" +
                            $"\n/open_discord - open Discord" +
                            $"\n/close_discord - close Discord\n" +

                            $"\nSystem:" +
                            $"\n/open_explorer - open explorer" +
                            $"\n/stop - end active session\n\n\n" +

                            $"----------------------------------------\n" +

                            $"Danger Zone:" +
                            $"\n/turn_off_pc - turn off the computer\n"); break;

                            //YOU CAN ADD YOUR TEAM HERE AND DESCRIPTION OF THEM
                    
                case "/stop": 
                    UpDate.SendMes(client, message, "Ending your session...");
                    try {
                        UpDate.users.Remove(message.Chat.Username); break;
                    }
                    catch (Exception e) {
                        UpDate.SendMes(client, message, "Something went wrong while ending the session! Perhaps your nickname cannot be determined.");
                        Console.WriteLine(e.Message); break;
                    }
                case "/turn_off_pc":
                    if (message.Chat.Id == UpDate.hostId)
                    {
                        Process.Start("cmd", "/c shutdown -s -f -t 00");
                        await client.SendTextMessageAsync(message.Chat.Id, "Forcing my computer to shut down..."); break;
                    }
                    else await client.SendTextMessageAsync(message.Chat.Id, "This command can be executed by a limited number of users!"); break;
                case "/open_explorer":
                    UpDate.OpenFile(client, message, "Opened File Explorer!", "explorer.exe"); break;
                case "/open_google":
                    UpDate.OpenFile(client, message, "Opened Google!", @"INSERT YOUR PATH TO THE REQUIRED APP HERE"); break;
                case "/open_youtube":
                    UpDate.OpenReadyUrl(client, message, "Opened YouTube!", @"https://www.youtube.com"); break;
                case "/open_video_by_name":
                    UpDate.SendMes(client, message, "Enter the name of the video you want to watch:");
                    UpDate.waitingText = 1; break;
                case "/open_vk":
                    UpDate.OpenReadyUrl(client, message, "Opened Vkontakte!", @"https://vk.com/feed"); break;
                case "/open_ya_mus":
                    UpDate.OpenReadyUrl(client, message, "Opened Yandex music!", @"https://music.yandex.ru/home"); break;
                case "/open_url":
                    UpDate.SendMes(client, message, "Enter URL:");
                    UpDate.waitingURL = 1; break;
                case "/open_faceit":
                    UpDate.OpenFile(client, message, "Opened Faceit!", @"INSERT YOUR PATH TO THE REQUIRED APP HERE",
                        @"C:\Program Files\FACEIT AC\faceitclient.exe"); break;
                case "/open_steam":
                    UpDate.OpenFile(client, message, "Opened Steam!", @"INSERT YOUR PATH TO THE REQUIRED APP HERE"); break;
                case "/open_discord":
                    UpDate.OpenFile(client, message, "Opened Discord!", @"INSERT YOUR PATH TO THE REQUIRED APP HERE"); break;
                case "/close_google":
                    UpDate.CloseProcess(client, message, "Closed Google!", "chrome"); break;
                case "/close_faceit":
                    UpDate.CloseProcess(client, message, "Closed Faceit!", "faceit", "faceitclient"); break;
                case "/close_steam":
                    UpDate.CloseProcess(client, message, "Closed Steam!", "steam"); break;
                case "/close_discord":
                    UpDate.CloseProcess(client, message, "Closed Discord!", "discord"); break;
                default:
                    if (inputString == UpDate.key) await client.SendTextMessageAsync(message.Chat.Id, $"Key is correct! Welcome to ACI:" +
                        $"\n\nHello, my name is ACI! I remember you until the end of this session" +
                        $"\n/help - all my commands" +
                        $"\n/stop - end session");
                    else if (message.Sticker != null || message.Photo != null) {
                        Random r = new Random();
                        var rand = r.Next(1, 120).ToString();
                        if (int.Parse(rand) < 100) rand = "0" + rand;
                        if (int.Parse(rand) < 10) rand = "0" + rand;
                        await client.SendStickerAsync(message.Chat.Id, InputFile.FromUri($"https://chpic.su/_data/stickers/k/kisiiiiiii/kisiiiiiii_{rand}.webp?v=1693179002"));
                    }
                    else if (UpDate.waitingText == 1) {
                        var video_name = message.Text.Replace(' ', '+');
                        UpDate.OpenReadyUrl(client, message, $"Opened YouTube with a request: {message.Text}!", $"https://www.youtube.com/results?search_query={video_name}");
                    }
                    else if (UpDate.waitingURL == 1) UpDate.OpenReadyUrl(client, message, "Opened your URL!", inputString);
                    else await client.SendTextMessageAsync(message.Chat.Id, "Sorry, I didn't understand you! Try using hints"); break;
            }    
        }
    }
}
