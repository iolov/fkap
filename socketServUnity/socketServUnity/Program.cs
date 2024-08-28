using System.Net.Sockets;
using System.Net;
using System.Text;

class Program
{
    private static void Main(string[] args)
    {
        
        try
        {
            TcpListener servSocket = new TcpListener(IPAddress.Any, 3243);
            Console.WriteLine("serv started");

            servSocket.Start();
            TcpClient client = servSocket.AcceptTcpClient();

            NetworkStream stream = client.GetStream();
            byte[] message = new byte[256];
            int lenth = stream.Read(message, 0, message.Length);
            string mes = Encoding.ASCII.GetString(message);
            Console.WriteLine(mes);
        
            Console.ReadLine();
            client.Close();
            servSocket.Stop();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    //string mes = "hallo";
    //byte[] buffer = Encoding.ASCII.GetBytes(mes);
    //stream.Write(buffer);
}