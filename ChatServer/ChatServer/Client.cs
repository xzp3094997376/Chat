using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace ChatServer
{
    //处理客户端通信
    class Client
    {
        private Socket mSoket;
        Thread t;
        byte[] data = new byte[1024];
        public Client(Socket s)
        {
            mSoket=s;
            t = new Thread(ReceiveMsg);
            t.Start();
        }
        private void ReceiveMsg()
        {
            while (true)
            {
                if (mSoket.Poll(10,SelectMode.SelectRead))
                {
                    mSoket.Close();
                    mSoket.Close();
                    Console.WriteLine("客户端断开");
                    break;
                }
                int length= mSoket.Receive(data);
                string msg = Encoding.UTF8.GetString(data, 0, length);           
                //TODO:收到数据分发到客户端
                Console.WriteLine("收到消息:  "+msg);
                Program.BroadcastMsg(msg);
            }
        }

       public void SendMsg(string msg)
        {
            byte[] data = new byte[1024];
            data = Encoding.UTF8.GetBytes(msg);
            mSoket.Send(data);
            Console.WriteLine(msg + "   send to client");
        }
        public bool Conected
        {
            get
            {
                return mSoket.Connected;
            }
        }
    }
}
