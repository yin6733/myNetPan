using System;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ClientA
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            client.Bind(new IPEndPoint(IPAddress.Parse("0.0.0.0"), new Random(DateTime.Now.Second).Next(10000, 12000)));
            client.SendTo(Encoding.UTF8.GetBytes("Hello!"), new IPEndPoint(IPAddress.Parse("106.14.44.67"), 13579));
            EndPoint point = new IPEndPoint(IPAddress.Any, 0);//用来保存发送方的ip和端口号
            byte[] buffer = new byte[1024];
            int length = client.ReceiveFrom(buffer, ref point);//接收数据报
            string message = Encoding.UTF8.GetString(buffer, 0, length);
            Console.WriteLine(point.ToString() + message);
            //client.ReceiveFrom(buffer, ref point);
            Console.WriteLine(Left(message, ':'));
            Console.WriteLine(int.Parse(Right(message, ':')));
            client.SendTo(Encoding.UTF8.GetBytes("Hello,ClientA!"), new IPEndPoint(IPAddress.Parse(Left(message, ':')), int.Parse(Right(message, ':'))));
            length = client.ReceiveFrom(buffer, ref point);//接收数据报
            message = Encoding.UTF8.GetString(buffer, 0, length);
            Console.WriteLine(point.ToString() + message);
            Console.ReadKey();
        }

        static string Left(string a, char c)
        {
            string b = "";
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == c) break;
                b += a[i];
            }
            return b;
        }

        static string Right(string a, char c)
        {
            string b = "";
            for (int i = a.Length - 1; i >= 0; i--)
            {
                if (a[i] == c) break;
                b = a[i] + b;
            }
            return b;
        }
    }
}
