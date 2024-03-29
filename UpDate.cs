using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace UpdateACI {
    public static class UpDate {
        public static string key = "INSERT HERE YOUR PASSWORD WITH WHICH YOU WANT TO UNLOCK THE BOT";
        public static int hostId = "INSERT HERE THE CHAT ID WITH THE HOST WHICH COMPUTER WILL BE CONTROLLED BY ACI, SO ONLY THE HOST CAN TURN OFF THE COMPUTER";
        public static List<string> users = new List<string>();
        public static int waitingText = 0;
        public static int waitingURL = 0;
        async public static void SendMes(ITelegramBotClient client, Message m, string mes) {
            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[] { new KeyboardButton[] { "/help" } }) { ResizeKeyboard = true };
            await client.SendTextMessageAsync(m.Chat.Id, mes, replyMarkup: replyKeyboardMarkup);
        }
        async public static void OpenFile(ITelegramBotClient client, Message m, string mes, string path, string path2 = "null") {
            Process.Start(path);
            if (path2 != "null") Process.Start(path2);
            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[] { new KeyboardButton[] { "/help" } }) { ResizeKeyboard = true };
            await client.SendTextMessageAsync(m.Chat.Id, mes, replyMarkup: replyKeyboardMarkup);
        }

        async public static void CloseProcess(ITelegramBotClient client, Message m, string mes, string process, string process2 = "null") {
            var count = 0;
            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[] { new KeyboardButton[] { "/help" } }) { ResizeKeyboard = true };
            try {
                foreach (Process proc in Process.GetProcessesByName(process)) proc.Kill(); count++;
                
                if (process2 != "null") foreach (Process proc in Process.GetProcessesByName(process2)) proc.Kill(); count++;
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            if (count == 0) await client.SendTextMessageAsync(m.Chat.Id, "There were no active processes from this application!", replyMarkup: replyKeyboardMarkup);
            else await client.SendTextMessageAsync(m.Chat.Id, mes, replyMarkup: replyKeyboardMarkup);
        }

        async public static void OpenReadyUrl(ITelegramBotClient client, Message message, string mes, string url) {
            try {
                ReplyKeyboardMarkup replyKeyboardMarkup = new(new[] { new KeyboardButton[] { "/help" } }) { ResizeKeyboard = true };
                Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
                await client.SendTextMessageAsync(message.Chat.Id, mes, replyMarkup: replyKeyboardMarkup);
                waitingURL = 0;
            }
            catch (Exception e) {
                UpDate.SendMes(client, message, "For some reason, the url could not be opened! Check its correctness and(or) existence.") ;
                Console.WriteLine(e.Message);
            }
        }
    }
}
