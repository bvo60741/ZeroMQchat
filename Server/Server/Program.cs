using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeroMQ;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ZContext context;
            ZSocket server;
            context = new ZContext();
            server = new ZSocket(context, ZSocketType.PAIR);
            server.Bind("tcp://*:5556");
            while (true)
            {
                ZError error;
                ZFrame frame = server.ReceiveFrame(ZSocketFlags.DontWait, out error);
                if (frame != null)
                {
                    Console.WriteLine(frame.ToString());
                }
                else
                {
                    Console.WriteLine("null");
                }
                Thread.Sleep(50);
            }
        }
    }
}
