using Classroom.Events;
using Classroom.Services;
using Classroom.ViewModels;
using System.Windows;

namespace Classroom.Views
{
    /// <summary>
    /// JoinMeetingView.xaml 的交互逻辑
    /// </summary>
    public partial class JoinMeetingView : Window
    {
        public JoinMeetingView()
        {
            InitializeComponent();
            DataContext = new JoinMeetingViewModel();
        }

        private void join_card_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<JoinMeetingEvent>().Publish(new EventArgument()
            {
                Target = Target.JoinMeetingViewModel,
            });
        }
    }
}
