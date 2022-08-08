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

	public partial class ServerInfo
	{
		public string Server { get; set; }
		public string IP { get; set; }
		public string Port { get; set; }
		public string Code { get; set; }
	}

	public partial class LineVServerInfo : ServerInfo
	{
		public string Status { get; set; }
		public string Date { get; set; }

		public LineVServerInfo(string status, string date)
		{
			this.Status = status;
			this.Date = date;
		}
	}

	public partial class LineOServerInfo : ServerInfo
	{
		[JsonProperty("ip")]
		public new string IP { get; set; }

		[JsonProperty("port")]
		public new string Port { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("onlinePlayers")]
		public int OnlinePlayers { get; set; }

		[JsonProperty("maxPlayers")]
		public string MaxPlayers { get; set; }

		[JsonProperty("maintenance")]
		public bool Maintenance { get; set; }
	}
}
