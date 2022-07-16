using LineLauncher.Classes;
using LineLauncher.Managers;
using LineLauncher.Utils;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace LineLauncher
{
    public partial class LineVWindow : Window
    {
        private Character character;
        private Server server;
        private Window loadingWindow = new LoadingWindow();

        public LineVWindow()
        {
            InitializeComponent();
            Logger.Debug("LineV window initialized.");
            webView.Visibility = Visibility.Collapsed;
            MainGrid.Visibility = Visibility.Hidden;
            InitializeWebView();

            this.Hide();
            loadingWindow.Show();
            Logger.Debug("Loading window showed.");
        }

        private async void InitializeWebView()
        {
            string _cacheFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LineLauncher");
            var webView2Environment = await CoreWebView2Environment.CreateAsync(null, _cacheFolderPath);
            await webView.EnsureCoreWebView2Async(webView2Environment);
            webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            webView.CoreWebView2.Settings.IsStatusBarEnabled = false;
            webView.Source = new Uri(LineVInfo.newsWebUrl);
            Logger.Debug("LineV WebView initialized.");
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && !this.Website.IsMouseOver && !this.Discord.IsMouseOver && !this.Teamspeak.IsMouseOver && !this.Play.IsMouseOver)
            {
                base.DragMove();
            }
        }

        private void CloseBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Logger.Debug("Application closed on LineV window.");
            System.Windows.Application.Current.Shutdown();
        }

        private void Minimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Logger.Debug("Application minimized on LineV window.");
            WindowState = WindowState.Minimized;
        }

        private void webView_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                ((WebView2)sender).ExecuteScriptAsync("document.querySelector('body').style.overflow='scroll';var style=document.createElement('style');style.type='text/css';style.innerHTML='::-webkit-scrollbar{display:none}';document.getElementsByTagName('body')[0].appendChild(style)");
                webView.Visibility = Visibility.Visible;
                MainGrid.Visibility = Visibility.Visible;
                loadingWindow.Hide();
                this.Show();
                Logger.Debug("LineV window showed.");
            }
        }

        private void Website_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Logger.Debug("LineV WebUrl pressed.");
            LauncherManager.LaunchWebUrl(LineVInfo.webUrl);
        }

        private void Discord_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Logger.Debug("LineV Discord pressed.");
            LauncherManager.LaunchDiscord(LineVInfo.discordLink);
        }

        private void Teamspeak_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Logger.Debug("LineV TeamSpeak3 pressed.");
            LauncherManager.LaunchTeamSpeak3();
        }

        private void Play_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Logger.Debug("LineV Play pressed.");
            FiveMManager.LaunchFiveM(this.server);
            base.WindowState = WindowState.Minimized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string identifier = SteamManager.ConvertSteamIDHex(SteamManager.ConvertSteamID64(SteamManager.GetSteamID3()));
            this.character = new Character(identifier);
            this.server = new Server(LauncherInfo.LineV);
            LineVServerInfo lineVServerInfo = (LineVServerInfo)this.server.ServerInfo;
            this.CharacterName.Text = this.character.CharacterInfo.Name;
            this.CharacterJob.Text = this.character.CharacterInfo.Job;
            Logger.Debug(string.Format("LineV Character Name: {0} - Character Job: {1}", this.character.CharacterInfo.Name, this.character.CharacterInfo.Job));
            this.ServerStatus.Text = lineVServerInfo.Status;
            this.ServerDate.Text = lineVServerInfo.Date;
            Logger.Debug(string.Format("LineV Server Status: {0}", lineVServerInfo.Status));
            Logger.Debug(string.Format("LineV Server Date: {0}", lineVServerInfo.Date));
        }
    }
}
