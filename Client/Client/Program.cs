using System;
using System.Linq;
using System.Text;
using System.Threading;
using ZeroMQ;
using Req;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            using (var context = ZmqContext.Create())
            {
                using (var socket = context.CreateSocket(SocketType.REQ))
                {
                    socket.Connect("tcp://127.0.0.1:5555");
                    long msgCptr = 0;
                    int msgIndex = 0;
                    while (true)
                    {
                        if (msgCptr == long.MaxValue)
                            msgCptr = 0;
                        msgCptr++;
                        if (options.maxMessage >= 0)
                            if (msgCptr > options.maxMessage)
                                break;
                        if (msgIndex == options.alterMessages.Count())
                            msgIndex = 0;
                        var reqMsg = options.alterMessages[msgIndex++]
                                            .Replace("#nb#", msgCptr.ToString("d2"));
                        Thread.Sleep(options.delay);
                        Console.WriteLine("Sending : " + reqMsg);
                        socket.Send(reqMsg, Encoding.UTF8);
                        var replyMsg = socket.Receive(Encoding.UTF8);
                        Console.WriteLine("Received: " + replyMsg + Environment.NewLine);
                    }
                }
            }
        }
    }
}
