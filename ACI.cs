using System;
using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;

namespace TGBot
{
     class ACI
     {
         public static void Main(string[] args)
         {
             var Client = new TelegramBotClient("INSERT YOUR TELEGRAM BOT API KEY HERE");
             Client.StartReceiving(Update, Error);
             Console.ReadLine();
         }
         public static int count = -1;
         public static int urlcode = 0;
         public static int videonamecode = 0;
         const string key = "INSERT HERE YOUR PASSWORD WITH WHICH YOU WANT TO UNLOCK THE BOT";

         async static Task Update(ITelegramBotClient client, Update update, CancellationToken token)
         {
             var message = update.Message;

             if (message != null)
             {
                 if (count == -1)
                 {
                     await client.SendTextMessageAsync(message.Chat.Id, "Enter key to activate bot:");
                     count = 0;
                     return;
                 }

                 if (count == 0)
                 {
                     while (message.Text != key)
                     {
                         if (message.Text != null && message != null)
                         {
                             await client.SendTextMessageAsync(message.Chat.Id, "Invalid key. Please try again:");
                             return;
                         }
                     }

                 await client.SendTextMessageAsync(message.Chat.Id, "Key is correct! Welcome to ACI:");
                 }

                 if (urlcode == 1)
                 {
                     await UpdateUrl(client, update, token);
                     return;
                 }

                 if (videonamecode == 1)
                 {
                     await UpdateVideoName(client, update, token);
                     return;
                 }
                
                 count = 1;
                 await UpdateNew(client, update, token);
             }
         }

         async static Task UpdateNew(ITelegramBotClient client, Update update, CancellationToken token)
         {
             var message = update.Message;
             if (message != null)
             {
                 if (message.Text != null)
                 {
                     Console.WriteLine($"{message.Chat.Username} | {message.Text}");

                     //System

                     //Helper that outputs all possible bot commands
                     if (message.Text.ToLower().Contains("/help"))
                     {
                         await client.SendTextMessageAsync(message.Chat.Id, $"Commands I can run:\n" +

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
                             $"\n/close_steam - Close Steam" +
                             $"\n/open_csgo - open CS:GO\n" +

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
                             $"\n/turn_off_all_processes - end all processes(Temporarily not working)" +
                             $"\n/turn_off_pc - turn off the computer\n");
                            
                         return;
                     }

                     //Stop the current bot session
                     if (message.Text.ToLower().Contains("/stop"))
                     {
                         count = -1;
                         await client.SendTextMessageAsync(message.Chat.Id, "Ending your session...");
                         return;
                     }

                     //Open explorer
                     if (message.Text.ToLower().Contains("/open_explorer"))
                     {
                         Process.Start("explorer.exe");
                         await client.SendTextMessageAsync(message.Chat.Id, "Opened File Explorer!");
                         return;
                     }

                     //End all processes
                     if (message.Text.ToLower().Contains("/turn_off_all_processes"))
                     {
                         try
                         {
                             count = 0;
                             foreach(Process proc in Process.GetProcesses())
                             {
                                 //proc.Kill();
                                 count++;
                             }
                         }
                         catch (Exception e)
                         {
                             Console.WriteLine(e.Message);
                         }
                         if (count == 0)
                         {
                             await client.SendTextMessageAsync(message.Chat.Id, "There were no active processes!");
                             return;
                         }
                         await client.SendTextMessageAsync(message.Chat.Id, "Ending all active processes...");
                         return;
                     }

                     //Computer shutdown forcibly
                     if (message.Text.ToLower().Contains("/turn_off_pc"))
                     {
                         Process.Start("cmd", "/c shutdown -s -f -t 00");
                         await client.SendTextMessageAsync(message.Chat.Id, "Forcing my computer to shut down...");
                         return;
                     }

                     //Google

                     //Open Google
                     if (message.Text.ToLower().Contains("/open_google"))
                     {
                         Process.Start(@"INSERT YOUR PATH TO THE REQUIRED APP HERE");
                         await client.SendTextMessageAsync(message.Chat.Id, "Got Google!");
                         return;
                     }

                     //Close Google
                     if (message.Text.ToLower().Contains("/close_google"))
                     {
                         var c = 0;
                         try
                         {
                             foreach(Process proc in Process.GetProcessesByName("chrome"))
                             {
                                 proc. Kill();
                                 c++;
                             }
                         }
                         catch (Exception e)
                         {
                             Console.WriteLine(e.Message);
                         }
                         if(c==0)
                         {
                             await client.SendTextMessageAsync(message.Chat.Id, "There were no active processes from this application!");
                             return;
                         }
                         await client.SendTextMessageAsync(message.Chat.Id, "Google shut down!");
                         return;
                     }

                     //Open the url that the user will reset
                     if (message.Text.ToLower().Contains("/open_url"))
                     {
                         try
                         {
                             urlcode = 1;
                             await client.SendTextMessageAsync(message.Chat.Id, "Enter your URL:");
                             return;
                         }
                         catch (Exception e)
                         {
                             Console.WriteLine(e.Message);
                         }
                     }

                     //Open YouTube
                     if (message.Text.ToLower().Contains("/open_youtube"))
                     {
                         try
                         {
                             Process.Start(new ProcessStartInfo { FileName = @"https://www.youtube.com", UseShellExecute = true });
                             await client.SendTextMessageAsync(message.Chat.Id, "Opened YouTube!");
                             return;
                         }
                         catch (Exception e)
                         {
                             Console.WriteLine(e.Message);
                         }
                     }

                    
                     //Open the url that the user will reset
                     if (message.Text.ToLower().Contains("/open_video_by_name"))
                     {
                         try
                         {
                             videonamecode = 1;
                             await client.SendTextMessageAsync(message.Chat.Id, "Enter the name of the video you want to watch:");
                             return;
                         }
                         catch (Exception e)
                         {
                             Console.WriteLine(e.Message);
                         }
                     }

                     //Open Yandex music
                     if (message.Text.ToLower().Contains("/open_ya_mus"))
                     {
                         try
                         {
                             Process.Start(new ProcessStartInfo { FileName = @"https://music.yandex.ru/home", UseShellExecute = true });
                             await client.SendTextMessageAsync(message.Chat.Id, "Yandex opened music!");
                             return;
                         }
                         catch (Exception e)
                         {
                             Console.WriteLine(e.Message);
                         }
                     }

                     //Open Vkontakte
                     if (message.Text.ToLower().Contains("/open_vk"))
                     {
                         try
                         {
                             Process.Start(new ProcessStartInfo { FileName = @"https://vk.com/feed", UseShellExecute = true });
                             await client.SendTextMessageAsync(message.Chat.Id, "Opened Vkontakte!");
                             return;
                         }
                         catch (Exception e)
                         {
                             Console.WriteLine(e.Message);
                         }
                     }

                     //Faceit

                     //Open faceit
                     if (message.Text.ToLower().Contains("/open_faceit"))
                     {
                         Process.Start(@"INSERT YOUR PATH TO THE REQUIRED APP HERE");
                         await client.SendTextMessageAsync(message.Chat.Id, "Opened Faceit!");
                         return;
                     }

                     //Close FaceIt
                     if (message.Text.ToLower().Contains("/close_faceit"))
                     {
                         var c = 0;
                         try
                         {
                             foreach(Process proc in Process.GetProcessesByName("faceit"))
                             {
                                 proc. Kill();
                                 c++;
                             }
                             foreach (Process proc in Process.GetProcessesByName("faceitclient"))
                             {
                                 proc. Kill();
                                 c++;
                             }
                         }
                         catch (Exception e)
                         {
                             Console.WriteLine(e.Message);
                         }
                         if(c==0)
                         {
                             await client.SendTextMessageAsync(message.Chat.Id, "There were no active processes from this application!");
                             return;
                         }
                         await client.SendTextMessageAsync(message.Chat.Id, "Closed Faceit!");
                         return;
                     }


                     //Steam

                     //Open Steam
                     if (message.Text.ToLower().Contains("/open_steam"))
                     {
                         Process.Start(@"INSERT YOUR PATH TO THE REQUIRED APP HERE");
                         await client.SendTextMessageAsync(message.Chat.Id, "Opened Steam!");
                         return;
                     }

                     //Close Steam
                     if (message.Text.ToLower().Contains("/close_steam"))
                     {
                         var c = 0;
                         try
                         {
                             foreach (Process proc in Process.GetProcessesByName("steam"))
                             {
                                 proc. Kill();
                                 c++;
                             }
                         }
                         catch (Exception e)
                         {
                             Console.WriteLine(e.Message);
                         }
                         if(c==0)
                         {
                             await client.SendTextMessageAsync(message.Chat.Id, "There were no active processes from this application!");
                             return;
                         }
                         await client.SendTextMessageAsync(message.Chat.Id, "Closed Steam!");
                         return;
                     }

                     //Open CS:GO
                     if (message.Text.ToLower().Contains("/open_csgo"))
                     {
                         Process.Start(@"INSERT YOUR PATH TO THE REQUIRED APP HERE");
                         await client.SendTextMessageAsync(message.Chat.Id, "Opened CS:GO!");
                         return;
                     }

                     //discord

                     //Open Discord
                     if (message.Text.ToLower().Contains("/open_discord"))
                     {
                         Process.Start(@"INSERT YOUR PATH TO THE REQUIRED APP HERE");
                         await client.SendTextMessageAsync(message.Chat.Id, "Opened Discord!");
                         return;
                     }

                     //Close Discord
                     if (message.Text.ToLower().Contains("/close_discord"))
                     {
                         var c = 0;
                         try
                         {
                            
                             foreach (Process proc in Process.GetProcessesByName("discord"))
                             {
                                 proc. Kill();
                                 c++;
                             }
                         }
                         catch (Exception e)
                         {
                             Console.WriteLine(e.Message);
                         }
                         if(c==0)
                         {
                             await client.SendTextMessageAsync(message.Chat.Id, "There were no active processes from this application!");
                             return;
                         }
                         await client.SendTextMessageAsync(message.Chat.Id, "Closed Discord!");
                         return;
                     }
                 }

                 if (message.Photo != null)
                 {
                     await client.SendTextMessageAsync(message.Chat.Id, "Don't send photos! I don't know how to work with them yet");
                 }

                 if (message.Sticker != null)
                 {
                     await client.SendStickerAsync(message.Chat.Id, InputFile.FromUri("https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp"));
                 }

                 else
                 {
                     if (message.Text == key) await client.SendTextMessageAsync(message.Chat.Id, $"Hello, my name is ACI! I remember you until the end of this session" +
                         $"\n/help - all my commands" +
                         $"\n/stop - end session");
                     else await client.SendTextMessageAsync(message.Chat.Id, "Sorry, I didn't understand you! Try using hints");
                     return;
                 }
             }
         }

         async static Task UpdateUrl(ITelegramBotClient client, Update update, CancellationToken token)
         {
             var message = update.Message;
             if (message != null)
             {
                 try
                 {
                     var url = message.Text;
                     Process.Start(new ProcessStartInfo { FileName = $@"{url}", UseShellExecute = true });
                     await client.SendTextMessageAsync(message.Chat.Id, "Opened your URL!");
                 }
                 catch (Exception e)
                 {
                     Console.WriteLine(e.Message);
                 }

             }
             urlcode = 0;
         }
         async static Task UpdateVideoName(ITelegramBotClient client, Update update, CancellationToken token)
         {
             var message = update.Message;
             if (message != null)
             {
                 try
                 {
                     var video_name = message.Text.Replace(' ', '+');
                     Process.Start(new ProcessStartInfo { FileName = $@"https://www.youtube.com/results?search_query={video_name}", UseShellExecute = true });
                     await client.SendTextMessageAsync(message.Chat.Id, $"Opened YouTube with a request: {message.Text}!");
                 }
                 catch (Exception e)
                 {
                     Console.WriteLine(e.Message);
                 }

             }
             videonamecode = 0;
         }

         private static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
         {
             throw new NotImplementedException();
         }
     }
}
