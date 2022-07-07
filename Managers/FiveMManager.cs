using LineLauncher.Classes;
using LineLauncher.Utils;
using System.Diagnostics;

namespace LineLauncher.Managers
{
    public static class FiveMManager
    {
        public static void KillFiveM()
        {
            var fiveMProcesses = Process.GetProcessesByName("fivem");
            foreach (var process in fiveMProcesses)
            {
                try
                {
                    process.Kill();
                }
                catch
                {

                }
            }
        }
        public static void LaunchFiveM(Server server)
        {
            if (server.ServerInfo.Server == LauncherInfo.LineV)
            {
                LineVServerInfo serverInfo = (LineVServerInfo) server.ServerInfo;
                if (serverInfo.Status == "Aktif")
                {
                    System.Diagnostics.Process.Start("explorer.exe", "fivem://connect/" + serverInfo.IP + ":" + serverInfo.Port);
                }
            }
        }
    }
}
