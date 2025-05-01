using Threads_Real_World_Example;

class Program
{
    static void Main(string[] args )
    {
        Console.WriteLine("Welcome to the Threads C# Demo Project!");
        Console.WriteLine("Press Enter to start");
        Console.ReadLine();
        string path1 = "Folder1";
        string path2 = "Folder2";
        string path3 = "Folder3";
        
        List<FolderMonitor> folderMonitors = new List<FolderMonitor>();
        folderMonitors.Add(new FolderMonitor(path1, 5));
        folderMonitors.Add(new FolderMonitor(path2, 5));
        folderMonitors.Add(new FolderMonitor(path3, 5));
        

        Console.WriteLine("Starting the folder monitor");
        folderMonitors.ForEach(fm => fm.Start());   
        Console.WriteLine("Press Enter to start");
        Console.ReadLine();
        Console.WriteLine("Stopping the folder monitor");
        folderMonitors.ForEach(fm => fm.Stop());
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine();
        Console.ReadLine();

    }
}