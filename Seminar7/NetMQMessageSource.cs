using NetMQ;
using NetMQ.Sockets;
using Seminar7;
using System;

public class NetMQMessageSource : IMessageSource<string>
{
    private readonly ResponseSocket _responseSocket;

    public NetMQMessageSource(string address)
    {
        _responseSocket = new ResponseSocket();
        _responseSocket.Bind(address);
    }

    public void Send(ChatMessage message, string toAddr)
    {
        _responseSocket.SendFrame(message.Serialize());
    }

    public ChatMessage Receive(ref string fromAddr)
    {
        var messageString = _responseSocket.ReceiveFrameString();
        return ChatMessage.Deserialize(messageString);
    }

    public string CreateNewT()
    {
        return string.Empty;
    }

    public string CopyT(string t)
    {
        return string.Copy(t);
    }
}

