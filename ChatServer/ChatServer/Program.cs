using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace ChatServer
{
    class Program
    {
        static List<Client> clientList = new List<Client>();
        static void Main(string[] args)
        {
            Socket serverSockets = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSockets.Bind(new IPEndPoint(IPAddress.Parse("192.168.137.1"), 8888));
            serverSockets.Listen(100);
            Console.WriteLine("listen:   ");
            while (true)
            {
                //Console.WriteLine("监听到信息：");
                Socket client= serverSockets.Accept();
                Console.WriteLine("client connetcted");
                Client mClient = new Client(client);
                clientList.Add(mClient);                                   
            }             
        }
        public static void BroadcastMsg(string msg)
        {
            var notConnectList = new List<Client>();
            foreach (var item in clientList)
            {
                if (item.Conected)
                {                    
                    item.SendMsg(msg);
                }
                else
                {
                    notConnectList.Add(item);
                }             
            }
            foreach (var item in notConnectList)
            {
                clientList.Remove(item);
            }
        }
    }
}
