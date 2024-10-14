using System;

namespace Seminar7
{

    public class Client
    {
        private readonly IMessageSourceClient<string> _messageSourceClient;
        private readonly string _name;

        public Client(string name, IMessageSourceClient<string> messageSourceClient)
        {
            _name = name;
            _messageSourceClient = messageSourceClient;
        }

        public void Start()
        {
            new Thread(ClientListener).Start();
            ClientSender();
        }

        private void ClientListener()
        {
            while (true)
            {
                string fromAddr = _messageSourceClient.CreateNewT();
                ChatMessage message = _messageSourceClient.Receive(ref fromAddr);

                Console.WriteLine($"Получено сообщение от {message.FromName}: {message.Text}");
            }
        }

        private void ClientSender()
        {
            Console.WriteLine("Введите имя получателя и сообщение, разделенные пробелом:");

            while (true)
            {
                string input = Console.ReadLine();
                var parts = input.Split(' ', 2);

                if (parts.Length < 2)
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                    continue;
                }

                var message = new ChatMessage
                {
                    Command = Command.Message,
                    FromName = _name,
                    ToName = parts[0],
                    Text = parts[1]
                };

                _messageSourceClient.Send(message, _messageSourceClient.GetServer());
                Console.WriteLine("Сообщение отправлено.");
            }
        }
    }
    
}


