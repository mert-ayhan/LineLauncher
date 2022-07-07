using LineLauncher.Utils;
using System;
using System.Net;
using System.Text;

namespace LineLauncher.Classes
{
    public class Server
    {
        public ServerInfo ServerInfo { get; set; }

        public Server(string server)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    if (server == LauncherInfo.LineV)
                    {
                        webClient.Encoding = Encoding.UTF8;
                        string status = webClient.DownloadString(LineVInfo.statusUrl);
                        string date = webClient.DownloadString(LineVInfo.dateUrl);
                        this.ServerInfo = new LineVServerInfo(status, date);
                        this.ServerInfo.Server = server;
                        this.ServerInfo.IP = LineVInfo.serverIP;
                        this.ServerInfo.Port = LineVInfo.serverPort;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(String.Format("Cannot get server({0}) info - {1}", server, e.Message));
            }
        }
    }

    public partial class ServerInfo
    {
        public string Server { get; set; }
        public string IP { get; set; }
        public string Port { get; set; }
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
}
