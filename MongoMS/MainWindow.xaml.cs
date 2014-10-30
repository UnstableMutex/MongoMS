using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MongoMS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
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
            WindowPosition.Default.Width = (int)Width;
            WindowPosition.Default.Left = (int)Left;
            WindowPosition.Default.Height = (int)Height;
            WindowPosition.Default.Top = (int)Top;
            WindowPosition.Default.Save();
        }
    }
}
