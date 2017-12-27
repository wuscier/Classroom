using Classroom.Events;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Classroom.Themes
{
    /// <summary>
    /// MyRecordControl.xaml 的交互逻辑
    /// </summary>
    public partial class MyRecordControl : UserControl
    {
        public MyRecordControl()
        {
            InitializeComponent();
        }

        private void start_record_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<RecordStatusChangeEvent>().Publish(new EventArgument()
            {
                Argument = new Argument() { Category = Category.RecordStart },
                Target = Target.MeetingViewModel,
            });
        }

        private void pause_resume_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;

            if (element?.DataContext != null)
            {
                MeetingViewModel meetingViewModel = element.DataContext as MeetingViewModel;


                Category category = meetingViewModel.UiStatusModel.PauseResumeText == UiStatusModel.RecordPauseText ? Category.RecordPause : Category.RecordResume;

                EventAggregatorManager.Instance.EventAggregator.GetEvent<RecordStatusChangeEvent>().Publish(new EventArgument()
                {
                    Argument = new Argument() { Category = category },
                    Target = Target.MeetingViewModel,
                });

            }
        }

        private void stop_record_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<RecordStatusChangeEvent>().Publish(new EventArgument()
            {
                Argument = new Argument() { Category = Category.RecordStop },
                Target = Target.MeetingViewModel,
            });
        }
    }
}
