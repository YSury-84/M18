using YoutubeExplode;

namespace YouTube
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string youlink = "https://www.youtube.com/watch?v=d8-3ffuTTZ8";
            Console.WriteLine("Hello, World!");
            YoutubeExplode videos = new YoutubeExplode();
            videos.GetAsync(youlink);
        }

        abstract class Command
        {
            public abstract void Run();
            public abstract void Cancel();
        }

        class Sender
        {
            Command _command;

            public void SetCommand(Command command)
            {
                _command = command;
            }

            // Выполнить
            public void Run()
            {
                _command.Run();
            }

            // Отменить
            public void Cancel()
            {
                _command.Cancel();
            }
        }

        class Receiver
        {
            public void Operation()
            {
                Console.WriteLine("Процесс запущен");
            }
        }



    }
}