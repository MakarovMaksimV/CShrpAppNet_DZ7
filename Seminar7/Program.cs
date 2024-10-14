using System.Net;
namespace Seminar7;

class Program
{
    static void Main(string[] args)
    {

        if (args.Length == 0)
        {
            var server = new Server(new NetMQMessageSource("tcp://*:12345"));
            server.Work();
        }
        else if (args.Length == 2)
        {
            var client = new Client(args[0], new NetMQMessageSourceClient($"tcp://{args[1]}:12345"));
            client.Start();
        }
        else
        {
            Console.WriteLine("Для запуска сервера введите ник-нейм как параметр запуска приложения");
            Console.WriteLine("Для запуска клиента введите ник-нейм и IP сервера как параметры запуска приложения");
        }
    }
}

