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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UDPDeMo.UI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DemoWindow d1 = new DemoWindow()
            {
                Title = "客户端1",
                uDPHelper = new UDPHelper.UDPHelper(51666),
            };
            DemoWindow d2 = new DemoWindow()
            {
                Title="客户端2",
                uDPHelper = new UDPHelper.UDPHelper(51667),
            };
            d1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            d2.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Hide();
            d1.Show();
            d2.Show();

        }
    }
}
