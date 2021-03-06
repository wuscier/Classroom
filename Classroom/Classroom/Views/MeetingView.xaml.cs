﻿using Classroom.Events;
using Classroom.Helpers;
using Classroom.Models;
using Classroom.Services;
using Classroom.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using ZOOM_SDK_DOTNET_WRAP;

namespace Classroom.Views
{
    /// <summary>
    /// MeetingView.xaml 的交互逻辑
    /// </summary>
    public partial class MeetingView : Window
    {
        public ProgressingView ProgressingView;

        public MeetingView()
        {
            InitializeComponent();
            DataContext = new MeetingViewModel();
            Loaded += MeetingView_Loaded;
            bottom_menu.Visibility = Visibility.Collapsed;
        }

        private void MeetingView_Loaded(object sender, RoutedEventArgs e)
        {
            Point containerPoint = video_container.PointToScreen(new Point() { X = 0, Y = 0 });

            ProgressingView = new ProgressingView();

            double left = (video_container.ActualWidth - ProgressingView.Width) / 2;
            double right = (video_container.ActualHeight - ProgressingView.Height) / 2;

            ProgressingView.Left = containerPoint.X + left;
            ProgressingView.Top = containerPoint.Y + right;

            ProgressingView.ShowDialog();

            MeetingViewModel meetingViewModel = DataContext as MeetingViewModel;
            meetingViewModel.MeetingViewHandle = new WindowInteropHelper(this).Handle;
        }

        public void SyncVideoUI()
        {
            //Hwnds hwnds = CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetUIController().GetMeetingUIWnds();

            //int w = (int)Math.Round(video_container.ActualWidth);
            //int h = (int)Math.Round(video_container.ActualHeight);

            //Win32APIs.MoveWindow(hwnds.firstViewHandle, 0, 0, w, h, true);
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
            MeetingViewModel meetingViewModel = DataContext as MeetingViewModel;
            meetingViewModel?.UnsubscribeEvents();

            CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().Leave(LeaveMeetingCmd.END_MEETING);

            EventAggregatorManager.Instance.EventAggregator.GetEvent<WindowShowEvent>().Publish(new EventArgument()
            {
                Target = Target.MainView
            });
        }

        private void microphone_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<MicStatusChangeEvent>().Publish(new EventArgument()
            {
                Target = Target.MeetingViewModel,
            });
        }

        private void camera_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<CameraStatusChangeEvent>().Publish(new EventArgument()
            {
                Target = Target.MeetingViewModel,
            });
        }

        private void audio_settings_Opened(object sender, RoutedEventArgs e)
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<AudioSettingsOpenEvent>().Publish(new EventArgument()
            {
                Target = Target.MeetingViewModel,
            });
        }

        private void video_settings_Opened(object sender, RoutedEventArgs e)
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<VideoSettingsOpenEvent>().Publish(new EventArgument()
            {
                Target = Target.MeetingViewModel,
            });
        }

        private void camera_device_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;

            if (element?.DataContext != null)
            {
                DeviceModel device = element.DataContext as DeviceModel;

                EventAggregatorManager.Instance.EventAggregator.GetEvent<SelectedDeviceChangeEvent>().Publish(new EventArgument()
                {
                    Target = Target.MeetingViewModel,
                    Argument = new Argument() { Category = Category.Camera, Value = device }
                });
            }
        }

        private void speaker_device_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;

            if (element?.DataContext != null)
            {
                DeviceModel device = element.DataContext as DeviceModel;

                EventAggregatorManager.Instance.EventAggregator.GetEvent<SelectedDeviceChangeEvent>().Publish(new EventArgument()
                {
                    Target = Target.MeetingViewModel,
                    Argument = new Argument() { Category = Category.Speaker, Value = device }
                });
            }
        }

        private void mic_device_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;

            if (element?.DataContext != null)
            {
                DeviceModel device = element.DataContext as DeviceModel;

                EventAggregatorManager.Instance.EventAggregator.GetEvent<SelectedDeviceChangeEvent>().Publish(new EventArgument()
                {
                    Target = Target.MeetingViewModel,
                    Argument = new Argument() { Category = Category.Mic, Value = device }
                });
            }
        }

        private void share_screen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SharingOptionsView sharingOptionsView = new SharingOptionsView();
            sharingOptionsView.Owner = this;
            sharingOptionsView.ShowDialog();
        }

        private void record_settings_Opened(object sender, RoutedEventArgs e)
        {
            RecordPathView recordPathView = new RecordPathView();

            Point toScreenPoint = record_settings.PointToScreen(new Point() { X = 0, Y = 0 });

            recordPathView.Left = toScreenPoint.X -350;
            recordPathView.Top = toScreenPoint.Y - 80;
            recordPathView.ShowDialog();
        }
    }
}
