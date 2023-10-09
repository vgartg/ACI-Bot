using Telegram.Bot;
using Telegram.Bot.Types;
using UpdateACI;

namespace TGBot {
    class ACI {
        public static void Main(string[] args) {
            var Client = new TelegramBotClient("6642792498:AAH0FLLbrjzoTz6fyw1AG1q3fi87yfYiPs4");
            Client.StartReceiving(Update, Error);
            Console.ReadLine();
        }
        async static Task Update(ITelegramBotClient client, Update update, CancellationToken token) {
            var message = update.Message;
            if (message != null) {
                if (!UpDate.users.Contains(message.Chat.Username)) {
                    if (message.Text != UpDate.key) await client.SendTextMessageAsync(message.Chat.Id, "Enter key to activate bot:" +
                        "\n\nIf you see this message for the second time, then the key was entered incorrectly.");
                    else {
                        UpDate.users.Add(message.Chat.Username);
                        await UpdateAfterLogin.UpdateAfLog(client, update, token, message);
                    }
                }
                else {
                    await UpdateAfterLogin.UpdateAfLog(client, update, token, message);
                }
            }
        }
        private static Task Error(ITelegramBotClient client, Exception e, CancellationToken token) => throw e;
    }
}
