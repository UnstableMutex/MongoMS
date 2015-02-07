using System.ComponentModel;
using System.Windows;

namespace MongoMS
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Width = WindowPosition.Default.Width;
            Left = WindowPosition.Default.Left;
            Height = WindowPosition.Default.Height;
            Top = WindowPosition.Default.Top;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            WindowPosition.Default.Width = (int) Width;
            WindowPosition.Default.Left = (int) Left;
            WindowPosition.Default.Height = (int) Height;
            WindowPosition.Default.Top = (int) Top;
            WindowPosition.Default.Save();
        }
    }
}