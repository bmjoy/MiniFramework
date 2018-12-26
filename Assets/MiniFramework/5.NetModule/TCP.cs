using System;
using System.Net;
using System.Net.Sockets;

public class TCP
{
    private TcpClient tcp;
    public string IP { get; set; }
    public int Port { get; set; }
    private byte[] recvBuffer;
    public int MaxBufferSize { get; set; }
    
    public void Init()
    {
        tcp = new TcpClient();
        recvBuffer = new byte[MaxBufferSize];
    }

    private void Connect()
    {
        tcp.BeginConnect(IPAddress.Parse(IP), Port, ConnectResult, null);
    }
    private void ConnectResult(IAsyncResult ar)
    {
        tcp.EndConnect(ar);
        tcp.GetStream().BeginRead(recvBuffer, 0, recvBuffer.Length, ReadResult, null);
    }

    private void ReadResult(IAsyncResult ar)
    {
        NetworkStream stream = tcp.GetStream();
        stream.EndRead(ar);
    }
}
