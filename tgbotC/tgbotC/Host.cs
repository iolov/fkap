using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;
using System.Xml.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;

class Host
{
    TelegramBotClient bot;
    Program program = new Program();
    int stap=0;
    string masage;
    public Host(string token)
    {
        bot = new TelegramBotClient(token);
    }

    public void start()
    {
        bot.StartReceiving(UpdateHandler, ErrorHandler);
        Console.WriteLine("HostStart");
    }

    private async Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        Console.WriteLine("ERR:" + exception.Message);

        await Task.CompletedTask;
    }

    private async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken token)
    {
        switch (update)
        {
            case { Message.Text: "/start" }:
                if (stap == 0)
                {
                    await bot.SendTextMessageAsync(update.Message.Chat.Id, "Hallo!,if you need buy coins print /buy");
                    stap += 1;
                }
                break;
        }
        if (stap == 1)
        {
            switch (update)
            {
                case { Message.Text: "/buy" }:
                    await bot.SendInvoiceAsync(update.Message.Chat.Id,
                       "Unlock feature X", "Will give you access to feature X of this bot", "unlock_X", "",
                       "XTR", new[] { new LabeledPrice("Digital Item", 1) }, photoUrl: "https://cdn-icons-png.flaticon.com/512/891/891386.png");
                    Console.WriteLine("one");
                    break;
                case { PreCheckoutQuery: { } preCheckoutQuery }:
                    if (preCheckoutQuery is { InvoicePayload: "unlock_X", Currency: "XTR", TotalAmount: 1 })
                    {
                        await bot.AnswerPreCheckoutQueryAsync(preCheckoutQuery.Id);
                        Console.WriteLine("two");
                    }
                    else
                    {
                        await bot.AnswerPreCheckoutQueryAsync(preCheckoutQuery.Id, "Invalid order");
                        Console.WriteLine("twoFail");
                    }
                    break;
                case { Message.SuccessfulPayment: { } successfulPayment }:
                    Console.WriteLine("three");
                    System.IO.File.AppendAllText("payments.log", $"\n{DateTime.Now}: " +
                       $"User {update.Message.From} paid for {successfulPayment.InvoicePayload}: " +
                       $"{successfulPayment.TelegramPaymentChargeId} {successfulPayment.ProviderPaymentChargeId}");
                    if (successfulPayment.InvoicePayload is "unlock_X")
                    {
                        await bot.SendTextMessageAsync(update.Message.Chat.Id, "print your nick");
                        stap++;
                        Console.WriteLine("four");
                    }
                    break;
            };
            if(stap == 2)
            {
                if(update.Message.Text != "/start")
                {
                    if (update.Message.Text != "/buy")
                    {
                        Console.WriteLine(update.Message.Text);
                    }
                }
            }
        }
        await Task.CompletedTask;
    }
}
