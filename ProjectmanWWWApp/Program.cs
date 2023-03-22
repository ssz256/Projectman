global using ProjectmanApi;

namespace ProjectmanWWWApp;

class Program
{
    static Server server = new Server(80);

    public static void Main()
    {
        Console.WriteLine("Server Starting");
        Start();
        Thread.Sleep(-1);
    }

    public static async Task Start()
    {
        server.Listener.Start();
        Console.WriteLine("Server Started");
        await server.RequestHandler();
    }
}

