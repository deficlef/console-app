using Data;
using System;
using System.IO;
using System.Threading;

namespace ConsoleApplication
{
    public class Program : Application
    {
        static float scannedSize = 0.0f;
        static int scannedFiles = 0;
        static float totalSize;
        static int numFiles;
        public static string path;
        static Tuple<float, int> result;

        static void Main(string[] args)
        {
            ProcessInput();
        }

        public static void ProcessInput()
        {
            scannedSize = 0.0f;
            scannedFiles = 0;

            Console.WriteLine(LAUNCH_MESSAGE);
            path = Console.ReadLine();

            if (!Directory.Exists(path))
            {
                Console.WriteLine(path + " is not a directory.");
                ProcessInput();
            }
            else
            {
                result = GetDirDetails(path);
                totalSize = result.Item1;
                numFiles = result.Item2;

                Console.WriteLine("{0}, {1}", "Total size: " + totalSize,
                                "Number of files: " + numFiles);
                ProcessRetry();
            }
        }

        public static void ProcessRetry()
        {
            string query;

            Console.WriteLine(RETRY_MESSAGE);
            query = Console.ReadLine();

            switch (query) {
                case YES_STRING:
                    ProcessInput();
                    break;
                case NO_STRING:
                    Environment.Exit(0);
                    break;
                case SAVE_STRING:
                    Thread dbThread = new Thread(ProcessDataSaving);
                    dbThread.Start();
                    ProcessRetry();
                    break;
                /*case DELETE_STRING:
                    ProcessDataDeletion();
                    ProcessRetry();
                    break;*/
                case ALL_STRING:
                    DisplayAllSaveData();
                    ProcessRetry();
                    break;
                default:
                    Console.WriteLine(UNKNOWN_ERROR);
                    ProcessRetry();
                    break;
            }
        }

        public static Tuple<float, int> GetDirDetails(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    return Tuple.Create(scannedSize, scannedFiles);
                }
                else
                {
                    try
                    {
                        foreach (string file in Directory.GetFiles(path))
                        {
                            if (File.Exists(file))
                            {
                                FileInfo finfo = new FileInfo(file);
                                scannedSize += finfo.Length;
                                scannedFiles++;
                            }
                        }

                        foreach (string dir in Directory.GetDirectories(path))
                        {
                            scannedSize += GetDirDetails(dir).Item1;
                        }
                    }
                    catch (NotSupportedException e)
                    {
                        Console.WriteLine(DIR_ERROR, e.Message);
                    }
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(DIR_ERROR, e.Message);
            }
            Console.WriteLine("{0}, {1}", "Scanned size: " + scannedSize, "Scanned files: " + scannedFiles);

            return Tuple.Create(scannedSize, scannedFiles);
        }

        static bool SaveDirectoryDetails(string dir, float total, int length)
        {
            DataClient dataClient = new DataClient();

            if (dataClient.GetSavedData(dir) == null) {
                return dataClient.InsertToDB(dir, total, length);
            }
            else {
                return dataClient.UpdateDB(dir, total, length);
            }
        }

        static void DisplayAllSaveData()
        {
            Console.WriteLine("Loading data...");
            DataClient dataClient = new DataClient();
            dataClient.GetSavedData();
        }

        static void ProcessDataDeletion()
        {
            string id;
            bool success;
            DataClient dataClient;

            Console.WriteLine(DELETE_MESSAGE);
            id = Console.ReadLine();
            Console.WriteLine("Deleting data...");
            dataClient = new DataClient();
            success = dataClient.DeleteFromDB(id);

            if (success)
            {
                Console.WriteLine(DELETE_SUCCESS);
            }
            else
            {
                Console.WriteLine(DELETE_ERROR);
            }
        }

        static void ProcessDataSaving() {
            Console.WriteLine("saving...");
            Console.WriteLine("path " + path);
            Console.WriteLine("totalSize " + totalSize);
            Console.WriteLine("numFiles " + numFiles);

            bool success = SaveDirectoryDetails(@path, totalSize, numFiles);

            if (success)
            {
                Console.WriteLine(SAVE_SUCCESS);
            }
            else
            {
                Console.WriteLine(SAVE_ERROR);
            }
        }
    }
}
