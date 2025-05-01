using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Threads_Real_World_Example
{
    public class FolderMonitor
    {
        private Thread _thread;
        private string _folder;
        private int _pollingInterval;
        private AutoResetEvent _stopEvent;
        private bool _started;

        public FolderMonitor(string folder, int pollingInterval)
        {
            _folder = folder;
            _pollingInterval = pollingInterval;
            _stopEvent = new AutoResetEvent(false);
            _started = false;
        }
        public void Start()
        {
            if (_started) {  return; }
            _thread = new Thread(MonitorFolder);
            _thread.Start();
            _started = true;
        }

        public void Stop()
        {
            if (!_started) { return; }
            _stopEvent.Set();
            _thread.Join();
            _thread = null;
            _started = false;
        }

        private void MonitorFolder()
        {
            // Perform folder monitoring logic here
            Console.WriteLine($"Monitoring folder: {_folder}");
            while (true)
            {
                // Check and see if we have any files to proces
                string fullpath= Path.Combine(Environment.CurrentDirectory, _folder);//Check this later
                string[] files = Directory.GetFiles(fullpath);
                Console.WriteLine($"{files.Length} files found");

                // Process the files, if any
                foreach (string file in files)
                {
                    if(!_started) { break; }
                    Console.WriteLine($"Processing file {Path.GetFileName(file)}");
                    string data = File.ReadAllText(file);
                    for(int i = 0; i < 100; i++)
                    {
                        using(SHA512 sha512 = SHA512.Create())
                        {
                            byte[] hash = sha512.ComputeHash(Encoding.UTF8.GetBytes(data));
                            string hashString = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                            data += hashString;
                        }
                    }
                    File.Delete(file);
                }



                // Check if the stop event is set
                _stopEvent.WaitOne(TimeSpan.FromSeconds(_pollingInterval));
                if(!_started) { break; }

            }
            Console.WriteLine($"Stopping folder monitor for {_folder}");
        }

    }
}
