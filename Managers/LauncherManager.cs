using LineLauncher.Classes;
using LineLauncher.Utils;
using System.Diagnostics;

namespace LineLauncher.Managers
{
    public static class LauncherManager
    {
        public static void LaunchWebUrl(string webUrl)
        {
            ProcessStartInfo psInfo = new ProcessStartInfo
            {
                FileName = webUrl,
                UseShellExecute = true
            };
            Process.Start(psInfo);
        }

        public static void LaunchDiscord(string dcLink)
        {
            ProcessStartInfo psInfo = new ProcessStartInfo
            {
                FileName = dcLink,
                UseShellExecute = true
            };
            Process.Start(psInfo);
        }

        public static void LaunchTeamSpeak3()
        {
            ProcessStartInfo psInfo = new ProcessStartInfo
            {
                FileName = "ts3server://" + LineVInfo.teamspeakIP + "?port=" + LineVInfo.teamspeakPort + "&password=" + LineVInfo.teamspeakPassword,
                UseShellExecute = true
            };
            Process.Start(psInfo);
        }

        public static string GetServerStatusText(string status, bool maintenance)
        {
            return (status.Equals("online") ? !maintenance ? "Aktif" : "Bakımda" : "Kapalı");
        }
    }
}
