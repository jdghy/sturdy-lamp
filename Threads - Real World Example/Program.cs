using Threads_Real_World_Example;

class Program
{
    static void Main(string[] args )
    {
        Console.WriteLine("Welcome to the Threads C# Demo Project!");
        Console.WriteLine("Press Enter to start");
        Console.ReadLine();
        Console.WriteLine("Starting the folder monitor");
        FolderMonitor fm = new FolderMonitor("Test", 5);
        fm.Start();
        Console.WriteLine("Press Enter to start");
        Console.ReadLine();
        Console.WriteLine("Stopping the folder monitor");
        fm.Stop();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine();
        Console.ReadLine();

    }
}