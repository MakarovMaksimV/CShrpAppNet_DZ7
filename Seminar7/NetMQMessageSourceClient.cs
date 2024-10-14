using System;
using NetMQ;
using NetMQ.Sockets;

namespace Seminar7
{
    using NetMQ;
    using NetMQ.Sockets;
    using System;

    public class NetMQMessageSourceClient : IMessageSourceClient<string>
    {
        private readonly RequestSocket _requestSocket;
        private readonly string _serverAddress;

        public NetMQMessageSourceClient(string serverAddress)
        {
            _serverAddress = serverAddress;
            _requestSocket = new RequestSocket();
            _requestSocket.Connect(serverAddress);
        }

        public void Send(ChatMessage message, string toAddr)
        {
            _requestSocket.SendFrame(message.Serialize());
        }

        public ChatMessage Receive(ref string fromAddr)
        {
            var messageString = _requestSocket.ReceiveFrameString();
            return ChatMessage.Deserialize(messageString);
        }

        public string CreateNewT()
        {
            return string.Empty;
        }

        public string GetServer()
        {
            return _serverAddress;
        }
    }
}

