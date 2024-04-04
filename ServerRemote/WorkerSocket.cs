using ServerRemote.Platforms.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerRemote
{
    class WorkerSocket
    {
        private Server server;
        private Socket socket;
        private NetworkStream stream;
        private BinaryWriter writer;
        private AsyncCallback workerCallback;

        public WorkerSocket(Socket workerSocket, Server server)
        {
            this.server = server;
            this.socket = workerSocket;
            this.stream = new NetworkStream(socket);
            this.writer = new BinaryWriter(stream, Encoding.UTF8);

            WaitForData();
        }

        private void WaitForData()
        {
            try
            {
                workerCallback = new AsyncCallback(OnDataReceived);

                byte[] dataBuffer = new byte[1024];
                socket.BeginReceive(dataBuffer, 0, dataBuffer.Length, SocketFlags.None, workerCallback, dataBuffer);
            }
            catch (SocketException se)
            {
                //Debug.WriteLine(se.Message);
            }
        }

        private void OnDataReceived(IAsyncResult asyn)
        {
            byte[] dataBuffer = (byte[])asyn.AsyncState;
            try
            {
                int CharCount = socket.EndReceive(asyn);
                if (CharCount > 0)
                {
                    char[] chars = new char[CharCount];

                    String text = Encoding.UTF8.GetString(dataBuffer);

                    StringReader reader = new StringReader(text);
                    while (reader.Peek() != -1)
                    {
                        string line = reader.ReadLine();
                        if (line.IndexOf(":") != -1)
                            ExecuteCommand(line);

                    }
                    //AddIncomingPacket(packet);
                    //Debug.WriteLine(String.Format("Packet from {0} UserId: {1}! CharLength: {2} Data: {3}", this.socket.RemoteEndPoint.ToString(), UserID.ToString(), charLen.ToString(), data));
                    WaitForData();
                }
                else
                {
                    throw new SocketException(10054);
                }
            }
            catch (ObjectDisposedException)
            {
               // Debug.WriteLine("OnDataReceived: Socket has been closed");
                //server.RemoveWorkerSocket(this);
            }
            catch (SocketException se)
            {
                if (se.ErrorCode == 10054) // Connection reset by peer
                {
                    string msg = "Client Disconnected";
                    //Debug.WriteLine(msg);
                    //server.RemoveWorkerSocket(this);
                }
                else
                {
                    //Debug.WriteLine(se.Message);
                    //server.RemoveWorkerSocket(this);
                }
            }
            catch (Exception e)
            {
                //Debug.WriteLine(e.Message);
                //server.RemoveWorkerSocket(this);
            }
        }

        public async void ExecuteCommand(String data)
        {
            //TODO: Per RegEx groups
            string command = data.Substring(0, data.IndexOf(":"));
            string[] parameters = data.Replace(command + ":", "").Split(';');

            Console.WriteLine(command);
            switch (command)
            {
                case "UP":
                    try
                    {
                        //await Flashlight.Default.TurnOnAsync();
                        server.mainPage.Up();
                        //var volumeControl = DependencyService.Get<IVolumeControl>();
                        //volumeControl?.IncreaseVolume();
                    }
                    catch (FeatureNotSupportedException ex)
                    {
                        // Handle not supported on device exception
                    }
                    catch (PermissionException ex)
                    {
                        // Handle permission exception
                    }
                    catch (Exception ex)
                    {
                        // Unable to turn on/off flashlight
                    }
                    break;
                case "DOWN":
                    try
                    {
                        server.mainPage.Down();
                        //await Flashlight.Default.TurnOffAsync();

                        // Get an instance of IVolumeControl
                        //var volumeControl = DependencyService.Get<IVolumeControl>();
                        //volumeControl?.DecreaseVolume();
                    }
                    catch (FeatureNotSupportedException ex)
                    {
                        // Handle not supported on device exception
                    }
                    catch (PermissionException ex)
                    {
                        // Handle permission exception
                    }
                    catch (Exception ex)
                    {
                        // Unable to turn on/off flashlight
                    }
                    break;
                case "leftClick":
                   // Utils.MouseClick(MouseButtons.Left, Cursor.Position.X, Cursor.Position.Y);
                    break;
                case "rightClick":
                    //Utils.MouseClick(MouseButtons.Right, Cursor.Position.X, Cursor.Position.Y);
                    break;
                case "sendKey":
                    //Utils.SendKey(parameters[0]);
                    break;
            }
        }
    }
}
