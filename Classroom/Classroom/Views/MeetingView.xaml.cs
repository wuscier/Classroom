using Classroom.Events;
using Classroom.Helpers;
using Classroom.Services;
using Classroom.ViewModels;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using ZOOM_SDK_DOTNET_WRAP;

namespace Classroom.Views
{
    /// <summary>
    /// MeetingView.xaml 的交互逻辑
    /// </summary>
    public partial class MeetingView : Window
    {
        public MeetingView()
        {
            InitializeComponent();
            DataContext = new MeetingViewModel();
        }

        public void SyncVideoUI()
        {
           Hwnds hwnds = CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetUIController().GetMeetingUIWnds();

            int w = (int)Math.Round(video_container.ActualWidth);
            int h = (int)Math.Round(video_container.ActualHeight);

            Win32APIs.MoveWindow(hwnds.firstViewHandle, 0, 0, w, h, true);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SyncVideoUI();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            SyncVideoUI();
        }

        protected override void OnClosed(EventArgs e)
        {
            CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().Leave(LeaveMeetingCmd.LEAVE_MEETING);

            EventAggregatorManager.Instance.EventAggregator.GetEvent<WindowShowEvent>().Publish(new EventArgument()
            {
                Target = Target.MainView
            });
        }
    }
}
