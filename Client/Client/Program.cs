using System;
using System.Linq;
using System.Text;
using System.Threading;
using ZeroMQ;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ZContext context;
            ZSocket client;
            context = new ZContext();
            client = new ZSocket(context, ZSocketType.PAIR);
            client.Connect("tcp://localhost:5556");
            while (true)
            {
                client.Send(new ZFrame("Clientsend"));
                Console.WriteLine("Send Ready!");
                Thread.Sleep(50);
            }
        }
    }
}
