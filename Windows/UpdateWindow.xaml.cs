using LineLauncher.Classes;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.Windows;

namespace LineLauncher
{
    public partial class UpdateWindow : Window
    {
        public UpdateWindow()
        {
            InitializeComponent();
            Logger.Initialize();

            Logger.Info("-----------------------------------------------------------------");
            Logger.Debug("Application launched.");

            Launcher.Version = "1.1.4";
            Logger.Info(String.Format("Version: {0}", Launcher.Version));

            if (Updater.CheckUpdate() == true)
            {
                Logger.Debug("No need to update, showing selection page.");
                this.Hide();
                SelectionWindow window = new SelectionWindow();
                window.Show();
            }
            else
            {
                Logger.Debug(String.Format("Need to update - New Version: {0}", Updater.GetVersion()));
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);

                if (principal.IsInRole(WindowsBuiltInRole.Administrator) == false && principal.IsInRole(WindowsBuiltInRole.User) == true)
                {
                    ProcessStartInfo objProcessInfo = new ProcessStartInfo();
                    objProcessInfo.UseShellExecute = true;
                    objProcessInfo.FileName = Assembly.GetEntryAssembly().CodeBase;
                    objProcessInfo.UseShellExecute = true;
                    objProcessInfo.Verb = "runas";
                    try
                    {
                        Process proc = Process.Start(objProcessInfo);
                        Application.Current.Shutdown();
                    }
                    catch (Exception e)
                    {
                        Logger.Error(String.Format("Cannot start as administrator - {0}", e.Message));
                    }
                }

                try
                {
                    String path = Directory.GetCurrentDirectory();
                    String zipLoc = Path.Combine(path, "update.zip");

                    Logger.Debug(String.Format("Trying to download file from link {0}", Updater.LauncherUrl()));
                    using (var webClient = new WebClient())
                    {
                        webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(FileDownloadedCallback);
                        webClient.DownloadProgressChanged += (s, e) =>
                        {
                            progressBar.Value = e.ProgressPercentage;
                        };
                        webClient.DownloadFileAsync(new Uri(Updater.LauncherUrl()), zipLoc);
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(String.Format("Cannot download file - {0} - {1}", Updater.LauncherUrl(), e.Message));
                }
            }
        }

        private void FileDownloadedCallback(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                Updater.RunCommand();
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Cannot run updater command - {0}", ex.Message));
            }
        }
    }
}