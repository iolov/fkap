using SocketIOClient;
using System.Net.Sockets;
using Telegram.Bot.Types;
using System.Threading;
using Telegram.Bot;
using System.ComponentModel.Design;
using System.Text;
using System.IO;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            TelegramBotClient bot;
            Host tgbot = new Host("5749086938:AAHlP5Rg63kqmdHprVu4GOvLFBMY00e6oss");
            tgbot.start();
        }
        catch  (Exception ex)
        {
        Console.WriteLine(ex.Message);
        }

        Console.ReadLine(); 
    }

    public void sendMessage(string mes)
    {
        TcpClient tcp = new TcpClient("26.250.75.88", 3243);
        Console.WriteLine("connected");

        NetworkStream stream = tcp.GetStream();
        byte[] message = new byte[256];
        byte[] buffer = Encoding.ASCII.GetBytes(mes);
        stream.Write(buffer);
    }
    //int lenth = stream.Read(message, 0, message.Length);
    //string mes = Encoding.ASCII.GetString(message);
    //Console.WriteLine(mes);
}