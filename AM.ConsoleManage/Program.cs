﻿using AM.SuperWebSocket;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;

namespace AM.ConsoleManage
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start the server!");

            Console.ReadKey();
            Console.WriteLine();

            
            var bootstrap = BootstrapFactory.CreateBootstrap();
            if (!bootstrap.Initialize())
            {
                Console.WriteLine("Failed to initialize!");
                Console.ReadKey();
                return;
            }
            var result = bootstrap.Start();
            Console.WriteLine("Start result: {0}!", result);
            if (result == StartResult.Failed)
            {
                Console.WriteLine("Failed to start!");
                Console.ReadKey();
                return;
            }
            

            /**
            var appServer = new AMAppServer();
            if (!appServer.Setup(9090))
            {
                Console.WriteLine("Failed to setup!");
                Console.ReadKey();
                return;
            }
            Console.WriteLine();
            if (!appServer.Start())
            {
                Console.WriteLine("Failed to start!");
                Console.ReadKey();
                return;
            }
            **/

            Console.WriteLine("Press key 'q' to stop it!");
            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }
            Console.WriteLine();

            //Stop the appServer
            bootstrap.Stop();
            //appServer.Stop();


            Console.WriteLine("The server was stopped!");
            Console.ReadKey();
        }
    }
}
