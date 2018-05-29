using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace UDPHelper
{
    public class UDPHelper
    {
        public IPAddress LocalAddress { get; set; }
        public IPEndPoint LocalEndPoint { get; set; }
        public UdpClient udpClient { get; set; }
        public int port { get; set; }   //本地监听的Udp端口

        //初始化，并绑定本地终结点
        public UDPHelper()
        {
            //获取本地IP
            IPAddress[] ips=Dns
        }
    }
}
