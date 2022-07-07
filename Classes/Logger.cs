using System;
using System.IO;

namespace LineLauncher.Classes
{
    public static class Logger
    {
        private static readonly string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LineLauncher\\app.log");

        public static void Initialize()
        {
            string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LineLauncher");
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        public static void Info(string message)
        {
            Log(String.Format("[INFO] {0}", message));
        }

        public static void Debug(string message)
        {
            Log(String.Format("[DEBUG] {0}", message));
        }

        public static void Error(string message)
        {
            Log(String.Format("[ERROR] {0}", message));
        }

        private static void Log(string message)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath, true))
            {
                streamWriter.WriteLine(String.Format("{0} {1}", String.Format("[{0:HH:mm:ss.fff}]", DateTime.Now), message));
                streamWriter.Close();
            }
        }
    }
}
