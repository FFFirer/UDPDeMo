using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using UDPHelper;

namespace UDPDeMo.UI
{
    /// <summary>
    /// DemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DemoWindow : Window
    {
        public UDPHelper.UDPHelper uDPHelper { get; set; }
        public string DearName { get; set; }
        public IPEndPoint remoteEndPoint { get; set; }
        public DemoWindow()
        {
            InitializeComponent();
            this.Closing += DemoWindow_Closing;
        }

        private void DemoWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void UDPHelper_DataHandle(object sender, string e)
        {
            Dispatcher.Invoke(() =>
            {
                txtbSendInfo.Text += e;
            });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DearName = "demo";
            txtblkLocalIP.Text = uDPHelper.LocalAddress.ToString();
            txtPort.Text = uDPHelper.port.ToString();
            txtRemoteIP.Text = "127.0.0.1";
            txtRemotePort.Text = "55555";
            remoteEndPoint = new IPEndPoint(IPAddress.Parse(txtRemoteIP.Text), int.Parse(txtRemotePort.Text));
            txtDear.Text = DearName;
            txtbInfo.Text = string.Format("用户名：{0}当前配置：本地监听>{1}  发送目标>{2}", DearName, uDPHelper.LocalEndPoint.ToString(), remoteEndPoint.ToString());
            uDPHelper.DataHandle += UDPHelper_DataHandle;

            Task.Run(() => uDPHelper.ReceiveDataAsync());
        }

        private void btnSetPort_Click(object sender, RoutedEventArgs e)
        {
            uDPHelper = new UDPHelper.UDPHelper(int.Parse(txtPort.Text));
            txtbInfo.Text = string.Format("用户名：{0}当前配置：本地监听>{1}  发送目标>{2}:{3}", DearName, uDPHelper.LocalEndPoint.ToString(), txtRemoteIP.Text, txtRemotePort.Text);
        }

        private void btnSetRemote_Click(object sender, RoutedEventArgs e)
        {
            remoteEndPoint = new IPEndPoint(IPAddress.Parse(txtRemoteIP.Text), int.Parse(txtRemotePort.Text));
            txtbInfo.Text = string.Format("用户名：{0}当前配置：本地监听>{1}  发送目标>{2}:{3}", DearName, uDPHelper.LocalEndPoint.ToString(), txtRemoteIP.Text, txtRemotePort.Text);
        }

        private void btnSetDear_Click(object sender, RoutedEventArgs e)
        {
            DearName = txtDear.Text;
            txtbInfo.Text = string.Format("用户名：{0}当前配置：本地监听>{1}  发送目标>{2}:{3}", DearName, uDPHelper.LocalEndPoint.ToString(), txtRemoteIP.Text, txtRemotePort.Text);
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string says = string.Format("{0} Says : {1}\n", DearName, txtbSend.Text);
            uDPHelper.SendMessageAsync(says, remoteEndPoint);
            Dispatcher.Invoke(() =>
            {
                txtbSendInfo.Text += says;
            });
        }
    }
}
