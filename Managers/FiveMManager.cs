using System;
using System.Diagnostics;
using LineLauncher.Classes;
using LineLauncher.Utils;

namespace LineLauncher.Managers
{
	public static class FiveMManager
	{
		public static void KillFiveM()
		{
			foreach (Process process in Process.GetProcessesByName("fivem"))
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
				LineVServerInfo serverInfo = (LineVServerInfo)server.ServerInfo;
				if (serverInfo.Status == "Aktif")
				{
					Process.Start("explorer.exe", "fivem://connect/" + serverInfo.IP + ":" + serverInfo.Port);
					return;
				}
			}
			else if (server.ServerInfo.Server == LauncherInfo.LineO)
			{
				LineOServerInfo serverInfo = (LineOServerInfo)server.ServerInfo;
				if (serverInfo.Status == "online" && !serverInfo.Maintenance)
				{
					Process.Start("explorer.exe", "fivem://connect/cfx.re/join/" + serverInfo.Code);
				}
			}
		}
	}
}
