using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerRemote
{
    public class Server
    {
        private Socket socket;
        private List<WorkerSocket> workerSockets;
        public MainPage mainPage;

        public Server(MainPage mainPage)
        {
            this.mainPage = mainPage;
        }

        public void Start(int port)
        {
            try
            {
                workerSockets = new List<WorkerSocket>();
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, port);
                socket.Bind(ipLocal);
                socket.Listen(int.MaxValue);
                socket.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(ex.StackTrace);
            }
        }
        public void OnClientConnect(IAsyncResult asyn)
        {
            try
            {
                Console.WriteLine("Connectedddddddddddddddddddddddddddddddddddddddddddddddddddddddd");
                Socket workerSocket = socket.EndAccept(asyn);
                workerSockets.Add(new WorkerSocket(workerSocket, this));
                socket.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debug.WriteLine("Socket has been closed");
            }
            catch (SocketException se)
            {
                System.Diagnostics.Debug.WriteLine(se.Message);
            }
        }
    }
}
