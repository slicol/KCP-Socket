using System.Net;
using System.Net.Sockets;
using System.Text;
using SGF;
using SGF.Network.KCP;
using UnityEngine;


namespace Assets.UnityTest.KCPTest
{
    public class KCPPlayer
    {
        public string LOG_TAG = "KCPPlayer";

        private KCPSocket m_Socket;
        private string m_Name;
        private int m_MsgId = 0;
        private IPEndPoint m_RemotePoint;

        public void Init(string name, int localPort, int remotePort)
        {
            m_Name = name;
            LOG_TAG = "KCPPlayer[" + m_Name + "]";

            IPAddress ipa = IPAddress.Parse(Network.player.ipAddress);
            m_RemotePoint = new IPEndPoint(ipa, remotePort);

            m_Socket = new KCPSocket(localPort, 1, AddressFamily.InterNetwork);
            m_Socket.AddReceiveListener(KCPSocket.IPEP_Any, OnReceiveAny);
            m_Socket.AddReceiveListener(m_RemotePoint, OnReceive);

            this.Log("Init() name:{0}, localPort:{1}, remotePort:{2}",name, localPort, remotePort );
        }

        private void OnReceiveAny(byte[] buffer, int size, IPEndPoint remotePoint)
        {
            string str = Encoding.UTF8.GetString(buffer, 0, size);
            this.Log("OnReceiveAny() " + remotePoint + ":" + str);
        }

        private void OnReceive(byte[] buffer, int size, IPEndPoint remotePoint)
        {
            string str = Encoding.UTF8.GetString(buffer, 0, size);
            this.Log("OnReceive() " + remotePoint + ":" + str);
        }

        public void OnUpdate()
        {
            if (m_Socket != null)
            {
                m_Socket.Update();
            }
        }

        public void SendMessage()
        {
            if (m_Socket != null)
            {
                m_MsgId++;
                m_Socket.SendTo(m_Name + "_" + "Message" + m_MsgId, m_RemotePoint);
            }
        }
    }
}
