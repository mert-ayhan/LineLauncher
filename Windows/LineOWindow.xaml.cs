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
    public partial class LineOWindow : Window
    {
        private Window loadingWindow = new LoadingWindow();

        public LineOWindow()
        {
            InitializeComponent();
            Logger.Debug("LineO window initialized.");
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
            webView.CoreWebView2.Settings.AreDevToolsEnabled = false;
            webView.Source = new Uri(LineOInfo.newsWebUrl);
            Logger.Debug("LineO WebView initialized.");
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && !Website.IsMouseOver && !Discord.IsMouseOver && !Play.IsMouseOver)
                this.DragMove();
        }

        private void CloseBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Logger.Debug("Application closed on LineO window.");
            System.Windows.Application.Current.Shutdown();
        }

        private void Minimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Logger.Debug("Application minimized on LineO window.");
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
                Logger.Debug("LineO window showed.");
            }
        }

        private void Website_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LauncherManager.LaunchWebUrl(LineOInfo.webUrl);
        }

        private void Discord_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LauncherManager.LaunchDiscord(LineOInfo.discordLink);
        }
    }
}
