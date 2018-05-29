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

        //数据处理事件
        public event EventHandler<string> DataHandle;

        public List<SendMember> SendMembers { get; set; }   //发送队列

        //初始化（默认），并绑定本地终结点
        public UDPHelper()
        {
            StartInit();
            this.port = 51666;
            //创建本地UdpClient,并监听
            LocalEndPoint = new IPEndPoint(LocalAddress, port);
            udpClient = new UdpClient(LocalEndPoint);
        }
        //初始化（指定本地监听端口）

        public UDPHelper(int p)
        {
            StartInit();
            this.port = p;
            //创建本地UdpClient,并监听
            LocalEndPoint = new IPEndPoint(LocalAddress, port);
            udpClient = new UdpClient(LocalEndPoint);
        }

        #region 获取本地IP
        public void StartInit()
        {
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var ip in ips)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    LocalAddress = ip;
                    break;
                }
            }
        }
        #endregion

        #region 异步发送
        public async void SendMessageAsync(string content, IPEndPoint remoteEndPoint)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(content);
            await this.udpClient.SendAsync(bytes, bytes.Length, remoteEndPoint);
        }
        #endregion

        #region 异步接收
        public async void ReceiveDataAsync()
        {
            while (true)
            {
                var result = await this.udpClient.ReceiveAsync();
                string s = Encoding.Unicode.GetString(result.Buffer);
                if(DataHandle != null)
                {
                    DataHandle(null, s);
                }
            }
        }
        #endregion
    }
}

public class SendMember
{
    public int Id { get; set; }
    public int Payload { get; set; }
    public int Status { get; set; }
}
