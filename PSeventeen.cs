using System;
using Serilog;

public class Subscriber {
    public string Name {get; set;}
    public Subscriber(string name) {

        Name = name;

    }

    public void Notify(string videoTitle) {

        Console.WriteLine($"{Name}: Ого! Нове відео '{videoTitle}'!");
        Log.Information($"{Name} отримав(ла) сповіщення про відео '{videoTitle}'!");

    }
}

public class YouTubeChannel {

    private List <Subscriber> subscribers = new List <Subscriber> ();
    public string ChannelName {get; set;}

    public YouTubeChannel(string channelName) {

        ChannelName = channelName;

    }

    public void AddSubscriber(Subscriber subscriber) {

        subscribers.Add(subscriber);
        Console.WriteLine($"{subscriber.Name} підписався(лась) на {ChannelName}!");
        Log.Information($"{subscriber.Name} підписався(лась) на {ChannelName}!");

    }

    public void RemoveSubscriber(Subscriber subscriber) {

        subscribers.Remove(subscriber);
        Console.WriteLine($"{subscriber.Name} відписався(лась) від {ChannelName}:(");
        Log.Information($"{subscriber.Name} відписався(лась) від {ChannelName}:(");

    }

    public void NotifySubscribers(string videoTitle) {

        foreach (var subscriber in subscribers) {

            subscriber.Notify(videoTitle);

        }
    }

    public void UploadVideo(string title) {

        Console.WriteLine($"Канал {ChannelName} завантажив нове відео: '{title}'!");
        Log.Information($"Канал {ChannelName} завантажив відео '{title}'!");
        NotifySubscribers(title);

    }
}

class Program {

    static void Main(string[] args) {

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        YouTubeChannel channel = new YouTubeChannel("П31");

        Subscriber sub1 = new Subscriber("Аня");
        Subscriber sub2 = new Subscriber("Даня");

        channel.AddSubscriber(sub1);
        channel.AddSubscriber(sub2);

        channel.UploadVideo("Як випросити халявні кристали в Дениса?");

        channel.RemoveSubscriber(sub2);

        channel.UploadVideo("Як пояснити минуле відео учбовій частині?...");

        Log.CloseAndFlush();

    }
}
