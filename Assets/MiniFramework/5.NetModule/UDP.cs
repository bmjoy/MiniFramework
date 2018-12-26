using System;
using System.Net;
using System.Net.Sockets;

public class UDP
{
    private UdpClient udp;
    private byte[] recvBuffer;
    public int MaxBufferSize { get; set; }
    public string RemoteIP { get; set; }
    public int Port { get; set; }
    public void Init()
    {
        IPEndPoint ep = new IPEndPoint(IPAddress.Any, Port);
        udp = new UdpClient(ep);
        recvBuffer = new byte[MaxBufferSize];
        udp.EnableBroadcast = true;
    }

    private void BeginReceive()
    {
        udp.BeginReceive(ReceiveResult, null);
    }

    private void ReceiveResult(IAsyncResult ar)
    {
        IPEndPoint remote = null;
        recvBuffer = udp.EndReceive(ar, ref remote);
        udp.BeginReceive(ReceiveResult, null);
    }

    public void BeginSend(byte[] data)
    {
        IPEndPoint ep = new IPEndPoint(IPAddress.Parse(RemoteIP), Port);
        udp.BeginSend(data, data.Length, ep, SendResult, null);
    }

    private void SendResult(IAsyncResult ar)
    {
        udp.EndSend(ar);
    }
}
