using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FullyAutomaticLaserJetCoder.MainTask
{
    //
    //客户端
    //
    public class Socket_client
    {
        private static Socket_client Socket_s;
       // Thread serverThread = null; 
        public static Socket_client Instance()
        {
            if (Socket_s == null)
            {
                Socket_s = new Socket_client();
            }
            return Socket_s;
        }
        public const int NET_OK = 0;
        public const int NET_PING_ERROR = 2001;
        public const int NET_CONNECT_TIMEOUT = 2002;
        public const int NET_CONNECT_ERROR = 2003;
        public const int NET_SEND_ERROR = 2004;
        public const int NET_RCEIVE_ERROR = 2004;
        private Socket mClientSocket;
        private bool mConnectSts;

        byte[] rcvBuf = new byte[1024 * 2014 * 3];

        public delegate void RceiveDataDelegate(byte[] data);
        public event RceiveDataDelegate ReciveDataEvent;

        public int ConnectOutTime(string ipAddress, int port, int outTime)   //Connection timeout detection
        {
            int nRet = -1;

            Ping p = new Ping();
            try
            {
                PingReply replay = p.Send(ipAddress);
                if (replay.Status != IPStatus.Success)
                {
                    return NET_PING_ERROR;
                }
            }
            catch (Exception e)
            {
                return NET_PING_ERROR;
            }

            if (mClientSocket != null && mClientSocket.Connected)
            {
                mClientSocket.Close();
                mClientSocket = null;
            }

            try
            {
                mClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IAsyncResult a = mClientSocket.BeginConnect(IPAddress.Parse(ipAddress), port, new AsyncCallback(ConnectCallback), mClientSocket);

                DateTime startTime = DateTime.Now;
                while (!mConnectSts)
                {
                    DateTime endTime = DateTime.Now;
                    TimeSpan span = endTime - startTime;
                    if (span.TotalMilliseconds > outTime)
                    {
                        mClientSocket.Close();
                        mConnectSts = false;
                        nRet = NET_CONNECT_TIMEOUT;
                        return nRet;
                    }
                }
                ReciveDataEvent += ReciveDataEvent_handle;
                nRet = NET_OK;
                return nRet;
            }
            catch (Exception e)
            {
                nRet = NET_CONNECT_ERROR;
                return nRet;
            }

        }
        private void ReciveDataEvent_handle(byte[] data)
        {
            string rcvStr = Encoding.UTF8.GetString(data);
            if (!string.IsNullOrEmpty(rcvStr))
            {
               // ProcessData(rcvStr);
            }
        }
        private void ConnectCallback(IAsyncResult ia)
        {
            mClientSocket = ia.AsyncState as Socket;
            if (mClientSocket.Connected)
            {
                mConnectSts = true;
            }
            else
            {
                mConnectSts = false;
            }

        }

        public int SendData(byte[] sendData)//send data 
        {
            lock (this)
            {
                try
                {
                    mClientSocket.Send(sendData, sendData.Length, SocketFlags.None);
                    return NET_OK;
                }
                catch (Exception e)
                {
                    return NET_SEND_ERROR;
                }
            }
        }

        public int RceiveOutTime(int outTime, ref byte[] data)  //
        {
            try
            {
                mClientSocket.Receive(data, SocketFlags.None);
                return NET_OK;
            }
            catch (Exception e)
            {
                return NET_RCEIVE_ERROR;
            }
        }

        public void RceiveAsny()
        {
            try
            {
                mClientSocket.BeginReceive(rcvBuf, 0, rcvBuf.Length, SocketFlags.None, new AsyncCallback(ReceiveAsynCallback), mClientSocket);
            }
            catch (Exception e)
            {

            }
        }

        private void ReceiveAsynCallback(IAsyncResult ia)
        {
            try
            {
                mClientSocket = ia.AsyncState as Socket;
                int count = mClientSocket.EndReceive(ia);
                mClientSocket.BeginReceive(rcvBuf, 0, rcvBuf.Length, SocketFlags.None, new AsyncCallback(ReceiveAsynCallback), mClientSocket);
                byte[] buf = rcvBuf.Skip(0).Take(count).ToArray();
                if (ReciveDataEvent != null)
                {
                    ReciveDataEvent(buf);
                }
            }
            catch (Exception e)
            {
                //  MessageBox.Show(e.ToString());
            }
        }

        public void Close()//close 
        {
            if (mClientSocket != null && mClientSocket.Connected)
            {
                mClientSocket.Disconnect(true);
            }
        }

        public bool IsConnected()
        {
            if (mClientSocket != null)
            {
                return mClientSocket.Connected;
            }
            else
            {
                return false;
            }
        }

    }
    //
    //服务端
    //
    public class Socket_server//服务端
    {
        Socket serverSocket;
        Socket server;
        public int port = 9876;
        private static Socket_server Socket_s;
        Thread serverThread = null; //服务器线程
        public static Socket_server Instance()
        {
            if (Socket_s == null)
            {
                Socket_s = new Socket_server();
            }
            return Socket_s;
        }
        public Socket_server()
        {
            readList.Add(null);
        }
        public void open()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Blocking = false;
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            serverSocket.Listen(1);
            //isBind = true;
            //  Thread.Sleep(500);

            readList[0] = serverSocket;
            serverThread = new Thread(serverFun);
            serverThread.IsBackground = true;
            serverThread.Start();
        }
        bool versionbool = false;
        List<Socket> readList = new List<Socket>();
        bool isLink = false;
        public string recvDate = "";
        public void serverFun()
        {

            int len;
            byte[] bytes = new byte[10240 * 128];
           // string recvData = "";

            while (true)
            {
                try
                {
                    Thread.Sleep(3);
                    Socket.Select(readList, null, null, -1);
                    if (readList[0].Equals(serverSocket))
                    {
                        server = serverSocket.Accept();
                        isLink = true;
                        readList[0] = (server);
                        if (versionbool)
                        {
                            versionbool = true;

                        }
                        else
                        {
                            versionbool = true;
                        }
                    }
                    else
                    {
                        if ((len = readList[0].Receive(bytes)) > 0)
                        {
                            recvDate = System.Text.Encoding.UTF8.GetString(bytes, 0, len);
                            recvDate = recvDate.Replace(" ", "");
                        }
                        else
                        {
                            readList[0].Close();
                            readList[0] = serverSocket;
                            isLink = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    readList[0].Close();
                    readList[0] = serverSocket;
                    isLink = false;
                }
            }
        }
        Object sendLock = new Object();
        public void sendDataToMac(string msg)
        {

            lock (sendLock)
            {
                try
                {
                    Thread.Sleep(5);
                    server.Send(Encoding.ASCII.GetBytes(msg + "\r\n"));

                }
                catch (Exception ex)
                {
                }
            }
        }

    }
}
