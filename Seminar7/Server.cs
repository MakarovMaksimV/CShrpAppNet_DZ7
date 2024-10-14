using System;
using System.ServiceModel.Channels;

namespace Seminar7
{
    public class Server
    {
        private readonly IMessageSource<string> _messageSource;
        private readonly Dictionary<string, string> _clients = new Dictionary<string, string>();

        public Server(IMessageSource<string> messageSource)
        {
            _messageSource = messageSource;
        }

        public void Work()
        {
            Console.WriteLine("Сервер ожидает сообщений...");

            while (true)
            {
                string fromAddr = _messageSource.CreateNewT();
                ChatMessage message = _messageSource.Receive(ref fromAddr);

                switch (message.Command)
                {
                    case Command.Register:
                        Register(message);
                        break;
                    case Command.Message:
                        RelayMessage(message);
                        break;
                    case Command.Confirmation:
                        ConfirmMessageReceived(message.Id);
                        break;
                }
            }
        }

        private void Register(ChatMessage message)
        {
            Console.WriteLine($"Регистрация клиента: {message.FromName}");
            _clients[message.FromName] = message.FromName;

            var response = new ChatMessage
            {
                Command = Command.Confirmation,
                FromName = "Server",
                ToName = message.FromName,
                Text = "Регистрация успешна"
            };
            _messageSource.Send(response, message.FromName);
        }

        private void RelayMessage(ChatMessage message)
        {
            if (_clients.TryGetValue(message.ToName, out string clientAddress))
            {
                Console.WriteLine($"Пересылка сообщения от {message.FromName} к {message.ToName}");
                _messageSource.Send(message, clientAddress);
            }
            else
            {
                Console.WriteLine("Получатель не найден.");
            }
        }

        private void ConfirmMessageReceived(int? id)
        {
            Console.WriteLine($"Подтверждение получения сообщения с ID: {id}");
        }
    }
}

