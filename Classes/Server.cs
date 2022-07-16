using System;
using System.Net;
using System.Text;
using LineLauncher.Utils;
using Newtonsoft.Json;

namespace LineLauncher.Classes
{
	public class Server
	{
		public ServerInfo ServerInfo { get; set; }

		public Server(string server)
		{
			try
			{
				using (WebClient webClient = new WebClient())
				{
					if (server == LauncherInfo.LineV)
					{
						webClient.Encoding = Encoding.UTF8;
						string status = webClient.DownloadString(LineVInfo.statusUrl);
						string date = webClient.DownloadString(LineVInfo.dateUrl);
						this.ServerInfo = new LineVServerInfo(status, date);
						this.ServerInfo.IP = LineVInfo.serverIP;
						this.ServerInfo.Port = LineVInfo.serverPort;
						this.ServerInfo.Code = LineVInfo.serverCode;
						this.ServerInfo.Server = server;
					}
					else if (server == LauncherInfo.LineO)
					{
						webClient.Encoding = Encoding.UTF8;
						string response = webClient.DownloadString(LineOInfo.APIUrl + "?action=" + LineOInfo.serverInfoAPIAction);
						this.ServerInfo = JsonConvert.DeserializeObject<LineOServerInfo>(response);
						this.ServerInfo.Code = LineOInfo.serverCode;
						this.ServerInfo.Server = server;
					}
				}
			}
			catch (Exception e)
			{
				Logger.Error(string.Format("Cannot get server({0}) info - {1}", server, e.Message));
			}
		}
	}
}
