using System;
using System.Threading;

using ZeroMQ;

namespace Examples
{
    static partial class Program
    {
        public static void Main(string[] args)
        {

            if (args == null || args.Length < 1)
            {
                Console.WriteLine("This is a server for our chat (kinda)");
                Console.WriteLine();
                args = new string[] { "" };
            }

            string name = args[0];

            // Create
            using (var context = new ZContext())
            using (var responder = new ZSocket(context, ZSocketType.REP))
            {
                // Bind
                responder.Bind("tcp://*:5570");

                while (true)
                {
                    // Receive
                    using (ZFrame request = responder.ReceiveFrame())
                    {
                        Console.WriteLine("Received from user: {0} ", request.ReadString());

                        // Do some work
                        Thread.Sleep(1);

                        // Send
                        responder.Send(new ZFrame(name));
                    }
                }
            }
        }
    }
}