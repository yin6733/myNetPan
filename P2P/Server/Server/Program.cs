using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            server.Bind(new IPEndPoint(IPAddress.Parse("0.0.0.0"), 13579));
            EndPoint point = new IPEndPoint(IPAddress.Any, 0);//用来保存发送方的ip和端口号
            Console.WriteLine("Waiting!");
            EndPoint point1, point2;
            //第一次接收数据
            byte[] buffer = new byte[1024];
            int length = server.ReceiveFrom(buffer, ref point);//接收数据报
            string message = Encoding.UTF8.GetString(buffer, 0, length);
            Console.WriteLine(point.ToString() + message);
            point1 = point;
            //第二次接收数据
            length = server.ReceiveFrom(buffer, ref point);//接收数据报
            message = Encoding.UTF8.GetString(buffer, 0, length);
            Console.WriteLine(point.ToString() + message);
            point2 = point;
            //交换数据
            Console.WriteLine("Sending!");
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            client.Bind(new IPEndPoint(IPAddress.Parse("0.0.0.0"), 13579));
            Console.WriteLine("Bind done!");
            client.SendTo(Encoding.UTF8.GetBytes(point1.ToString()), point2);
            client.SendTo(Encoding.UTF8.GetBytes(point2.ToString()), point1);
            Console.WriteLine("Send done!");
            length = server.ReceiveFrom(buffer, ref point);
            Console.WriteLine("hhh");
            client.SendTo(Encoding.UTF8.GetBytes("Send"), point2);
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
