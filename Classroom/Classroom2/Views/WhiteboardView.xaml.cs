using Classroom.ViewModels;
using System.Windows;

namespace Classroom.Views
{
    /// <summary>
    /// WhiteboardView.xaml 的交互逻辑
    /// </summary>
    public partial class WhiteboardView : Window
    {
        public WhiteboardView()
        {
            InitializeComponent();
            DataContext = new WhiteboardViewModel();
        }

        private void note_card_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (note_detail.Visibility == Visibility.Visible)
            {
                note_detail.Visibility = Visibility.Collapsed;
            }
            else
            {
                note_detail.Visibility = Visibility.Visible;
            }
        }

        private void thumbnail_card_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (thumbnail_detail.Visibility == Visibility.Visible)
            {
                thumbnail_detail.Visibility = Visibility.Collapsed;
            }
            else
            {
                thumbnail_detail.Visibility = Visibility.Visible;
            }
        }

        private void next_page_card_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void previous_page_card_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
