using LineLauncher.Utils;
using System;
using System.Diagnostics;
using System.Net;

namespace LineLauncher.Classes
{
    public static class Updater
    {
        public static bool CheckUpdate()
        {
            return GetVersion() == Launcher.Version;
        }

        public static string GetVersion()
        {
            string response = "";
            try
            {
                using (var webClient = new WebClient())
                {
                    response = webClient.DownloadString(Utils.LauncherInfo.versionUrl);
                }
            }
            catch (Exception e)
            {
                Logger.Error(String.Format("Cannot get launcher version - {0}", e.Message));
            }
            return response;
        }

        public static string LauncherUrl()
        {
            return LauncherInfo.zipUrl + GetVersion() + ".zip";
        }

        public static void RunCommand()
        {
            String strCmdText = String.Format("/C taskkill /IM line-launcher.exe && timeout 1 >nul && tar -xvf update.zip && del update.zip && start line-launcher.exe");
            Logger.Info(String.Format("Updater command - {0}", strCmdText));

            ProcessStartInfo psInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = strCmdText,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(psInfo);
        }
    }
}
