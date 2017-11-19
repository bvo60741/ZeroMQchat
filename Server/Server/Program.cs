using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeroMQ;
using Req;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            using (var context = ZmqContext.Create())
            {
                using (var socket = context.CreateSocket(SocketType.REP))
                {
                    socket.Bind("tcp://*:5555");
                    while (true)
                    {
                        Thread.Sleep(options.delay);
                        var rcvdMsg = socket.Receive(Encoding.UTF8);
                        Console.WriteLine("Received: " + rcvdMsg);
                        var replyMsg = options.replyMessage.Replace("#msg#", rcvdMsg);
                        Console.WriteLine("Sending : " + replyMsg + Environment.NewLine);
                        socket.Send(replyMsg, Encoding.UTF8);
                    }
                }
            }
        }
    }
}
