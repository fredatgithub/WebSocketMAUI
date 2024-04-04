using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientTestDemo
{
    public class TcpClient
    {
        private string pServerIp;
        private int pServerPort;
        private byte[] pBuffer = new byte[1024];

        private Socket socket;
       // private StreamWriter bw;
        //private StreamReader br;

        public TcpClient(string aServerIp, int aServerPort)
        {
            pServerIp = aServerIp;
            pServerPort = aServerPort;
        }

        public void Connect()
        {
            try
            {
                IPAddress serverAddr = IPAddress.Parse(pServerIp);
                //Log.Debug("TCP", "C: Connecting...");
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(new IPEndPoint(serverAddr, pServerPort));
                try
                {
                    socket.BeginReceive(pBuffer, 0, pBuffer.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);

                    //Log.Error("TCP", "Connecting...");
                    //NetworkStream networkStream = socket.GetStream();
                    //bw = new StreamWriter(networkStream);
                    //br = new StreamReader(networkStream);

                    //Log.Error("TCP", "Connected...");
                }
                catch (Exception e)
                {
                    //Log.Error("TCP", "S: Error", e);
                    socket.Close();
                }
            }
            catch (Exception e)
            {
                //Log.Error("TCP", "C: Error", e);
            }
        }

        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                int receivedBytes = socket.EndReceive(ar);
                if (receivedBytes > 0)
                {
                    string messageReceived = Encoding.ASCII.GetString(pBuffer, 0, receivedBytes);
                    Console.WriteLine($"Message received from server: {messageReceived}");

                    // Continue receiving data from the server asynchronously
                    socket.BeginReceive(pBuffer, 0, pBuffer.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Error receiving data from server: {ex.Message}");
            }
        }

        public void Send(string message)
        {
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            try
            {
                socket.Send(messageBytes);
                Console.WriteLine($"Sent message to server: {message}");
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Error sending message to server: {ex.Message}");
            }
        }

        public void Disconnect()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            Console.WriteLine("Disconnected from server.");
        }


        //public void SendMessage(string message)
        //{
        //    try
        //    {
        //        bw.WriteLine(message);
        //        bw.Flush();
        //    }
        //    catch (IOException e)
        //    {
        //        Console.WriteLine(e.StackTrace);
        //    }
        //}

        //public void Close()
        //{
        //    if (socket != null)
        //    {
        //        try
        //        {
        //            socket.Close();
        //        }
        //        catch (Exception e)
        //        {
        //            //Log.Error("TCP", "C: Error", e);
        //        }
        //    }
        //}

    }
}
