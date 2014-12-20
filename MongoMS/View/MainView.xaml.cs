using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace MongoMS.View
{
    /// <summary>
    ///     Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void UIElement_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle & e.ButtonState == MouseButtonState.Released)
            {
                var p = sender as Panel;
                Button b = p.Children.OfType<Button>().First();
                b.Command.Execute(b.CommandParameter);
            }
        }
    }
}