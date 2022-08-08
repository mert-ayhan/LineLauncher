using LineLauncher.Classes;
using LineLauncher.Managers;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LineLauncher
{
    public partial class SelectionWindow : Window
    {
        private byte state = 0;

        public SelectionWindow()
        { 
            InitializeComponent();
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.state != 0)
            {
                ImageBrush b = new ImageBrush();
                b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/SelectionNew.png"));
                this.Background = b;
                this.state = 0;
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.state != 1 && LineVPolygon.IsMouseOver)
            {
                ImageBrush b = new ImageBrush();
                b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/SelectionNewLineV.png"));
                this.Background = b;
                this.state = 1;
            }
            else if (this.state != 2 && LineZPolygon.IsMouseOver)
            {
                ImageBrush b = new ImageBrush();
                b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/SelectionNewLineZ.png"));
                this.Background = b;
                this.state = 2;
            }
            else if (this.state != 3 && LineOPolygon.IsMouseOver)
            {
                ImageBrush b = new ImageBrush();
                b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/SelectionNewLineO.png"));
                this.Background = b;
                this.state = 3;
            }
            else if (this.state != 4 && LineCPolygon.IsMouseOver)
            {
                ImageBrush b = new ImageBrush();
                b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/SelectionNew.png"));
                this.Background = b;
                this.state = 4;
            }
            else if (this.state != 0 && CloseBtn.IsMouseOver)
            {
                ImageBrush b = new ImageBrush();
                b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/SelectionNew.png"));
                this.Background = b;
                this.state = 0;
            }
        }

        private void LineVPolygon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SteamManager.GetSteamID3() == "0" || !SteamManager.IsRunning())
            {
                Logger.Error("Need to start steam first!");
                MessageBox.Show("LineV Launcher'ı başlatabilmeniz için Steam'in çalışır durumda olması gerekmektedir!", "HATA!");
                return;
            }

            Logger.Debug("LineV selected.");
            this.Hide();
            LineVWindow window = new LineVWindow();
            window.Show();
        }

        private void LineZPolygon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Logger.Debug("LineZ selected.");
            this.Hide();
            LineZWindow window = new LineZWindow();
            window.Show();
        }

        private void LineOPolygon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Logger.Debug("LineO selected.");
            this.Hide();
            LineOWindow window = new LineOWindow();
            window.Show();
        }

        private void LineCPolygon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void CloseBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Logger.Debug("Application closed on Selection window.");
            System.Windows.Application.Current.Shutdown();
        }
    }
}
