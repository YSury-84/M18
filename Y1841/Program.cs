using YoutubeExplode;
using YoutubeExplode.Converter;

namespace Y1841
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            // создадим отправителя
            var sender = new Sender();
            // создадим получателя
            var receiver = new Receiver();

            Console.WriteLine("Нажмите любую кнопку для продолжения...");Console.ReadKey();
            // создадим команду
            VideoOne video_o = new VideoOne(receiver);
            // инициализация команды
            sender.SetCommand(video_o);
            //  выполнение
            sender.Run();

            Console.WriteLine("Нажмите любую кнопку для продолжения..."); Console.ReadKey();
            VideoTwo video_t = new VideoTwo(receiver);
            // инициализация команды
            sender.SetCommand(video_t);
            //  выполнение
            sender.Run();

            Console.WriteLine("Нажмите любую кнопку для завершеня..."); Console.ReadKey();
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
            public async void Operation(string youlink)
            {
                Console.WriteLine("\nПолучение сведений о видео...");
                var youtube = new YoutubeClient();
                var video = youtube.Videos.GetAsync(youlink);
                Console.WriteLine("Название ролика: "+video.Result.Title);
                Console.WriteLine("Автор/канал: "+video.Result.Author);
                Console.WriteLine("Сведения получены!");
                Console.WriteLine("\nЗагрузка...");
                try
                {
                    await youtube.Videos.DownloadAsync(youlink,"c:\\temp\\", builder => builder.SetPreset(ConversionPreset.UltraFast));
                    }
                catch 
                { 
                    Console.WriteLine("Ошибка скачивания: Видео в данный момент недоступно (Video is not available)"); 
                }
            }
        }

        class VideoOne : Command
        {
            Receiver receiver;

            public VideoOne(Receiver receiver)
            {
                this.receiver = receiver;
            }

            // Выполнить
            public override void Run()
            {
                Console.WriteLine("Команда отправлена");
                receiver.Operation("https://youtu.be/yMRLnIPrPhQ");
            }

            // Отменить
            public override void Cancel()
            { }
        }

        class VideoTwo : Command
        {
            Receiver receiver;

            public VideoTwo(Receiver receiver)
            {
                this.receiver = receiver;
            }

            // Выполнить
            public override void Run()
            {
                Console.WriteLine("Команда отправлена");
                receiver.Operation("https://www.youtube.com/watch?v=0JEz-3QY_BY");
            }

            // Отменить
            public override void Cancel()
            { }
        }


    }
}