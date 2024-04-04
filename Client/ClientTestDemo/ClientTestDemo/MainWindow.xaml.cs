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

namespace ClientTestDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TcpClient client;

        public int startX, startY;

        public MainWindow()
        {
            InitializeComponent();

            StartClient();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            client.Send("UP:");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            client.Send("DOWN:");
        }

        private void StartClient()
        {
            startX = 0;
            startY = 0;

            try
            {
                client = new TcpClient("192.168.2.165", 1024);
                client.Connect();
                //client.Send("mouseMove:");
                //client.SendMessage("Hello");
                //Toast.makeText(getApplicationContext(), "Logged on", Toast.LENGTH_LONG).show();
            }
            catch (Exception e)
            {
                //Toast.makeText(getApplicationContext(), e.getMessage(), Toast.LENGTH_LONG).show();
            }

        }
    }
}
