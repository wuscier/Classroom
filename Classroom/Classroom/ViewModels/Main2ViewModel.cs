using Classroom.Events;
using Classroom.Helpers;
using Classroom.Models;
using Classroom.sdk_wrap;
using Classroom.Services;
using Classroom.Views;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Interop;
using ZOOM_SDK_DOTNET_WRAP;

namespace Classroom.ViewModels
{
    public class Main2ViewModel:BindableBase
    {
        public Main2ViewModel()
        {
            SubscribeEvents();

            IsMainCardSelected = true;
        }

        private bool _isMainCardSelected;

        public bool IsMainCardSelected
        {
            get { return _isMainCardSelected; }
            set { SetProperty(ref _isMainCardSelected, value); }
        }

        private bool _isHistoryCardSelected;

        public bool IsHistoryCardSelected
        {
            get { return _isHistoryCardSelected; }
            set { SetProperty(ref _isHistoryCardSelected, value); }
        }


        public ObservableCollection<CourseModel> CourseList { get; set; }

        private SubscriptionToken _cardSelectedToken;
        private SubscriptionToken _startClassToken;

        private void SubscribeEvents()
        {
            _cardSelectedToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<CardSelectedEvent>().Subscribe((argument) =>
            {
                switch (argument.Argument.Category)
                {
                    case Category.MainCard:
                        IsMainCardSelected = true;
                        IsHistoryCardSelected = false;
                        break;
                    case Category.HistoryCard:
                        IsMainCardSelected = false;
                        IsHistoryCardSelected = true;
                        break;
                }
            }, ThreadOption.PublisherThread, true, filter => { return filter.Target == Target.MainViewModel; });


            _startClassToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<StartClassEvent>().Subscribe((argument) =>
            {
                RegisterCallbacks();

                CustomMeetingUI();

                StartHook();


                StartParam startParam = new StartParam()
                {
                    userType = SDKUserType.SDK_UT_NORMALUSER,

                };

                //StartParam4APIUser apiuserStart = new StartParam4APIUser()
                //{
                //    userID = "704311",
                //    userToken = "Izi6atSDBiwtIqk3GQX9txjxjj8SZLIa6s7v",
                //    userName = "SDK User",
                //    meetingNumber = 3398415968,

                //};

                StartParam4NormalUser normalUser = new StartParam4NormalUser()
                {
                    meetingNumber = 3398415968,
                };

                startParam.normaluserStart = normalUser;



                SDKError error = SdkWrap.Instacne.Start(startParam);

                if (error == SDKError.SDKERR_SUCCESS)
                {
                    EventAggregatorManager.Instance.EventAggregator.GetEvent<WindowHideEvent>().Publish(new EventArgument()
                    {
                        Target = Target.MainView,
                    });

                    _meetingView = new MeetingView();
                    _meetingView.Show();

                }
                else
                {
                    MessageBox.Show(error.ToString());
                }

            }, ThreadOption.PublisherThread, true, filter => { return filter.Target == Target.MainViewModel; });
        }

        private void UnsubscribeEvents()
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<CardSelectedEvent>().Unsubscribe(_cardSelectedToken);
        }

        private MeetingView _meetingView;
        private bool _wndMsgHandled = false;

        private void RegisterCallbacks()
        {
            IMeetingServiceDotNetWrap meetingServiceDotNetWrap = CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap();

            IMeetingVideoControllerDotNetWrap videoControllerDotNetWrap = meetingServiceDotNetWrap.GetMeetingVideoController();

            videoControllerDotNetWrap.Add_CB_onUserVideoStatusChange((userId, videoStatus) =>
            {
                if (_meetingView.bottom_menu.Visibility != Visibility.Visible && videoStatus == VideoStatus.Video_ON)
                {
                    _meetingView.bottom_menu.Visibility = Visibility.Visible;
                    _meetingView.Height += 1;
                }

                if (_meetingView.ProgressingView != null)
                {
                    _meetingView.ProgressingView.Close();
                    _meetingView.ProgressingView = null;
                }
            });

            IMeetingParticipantsControllerDotNetWrap meetingParticipantsController = meetingServiceDotNetWrap.GetMeetingParticipantsController();

            meetingServiceDotNetWrap.Add_CB_onMeetingStatusChanged((status, result) =>
            {

            });
            meetingParticipantsController.Add_CB_onHostChangeNotification((userId) =>
            {

            });
            meetingParticipantsController.Add_CB_onLowOrRaiseHandStatusChanged((bLow, userId) =>
            {

            });
            meetingParticipantsController.Add_CB_onUserJoin((userIds) =>
            {
            });
            meetingParticipantsController.Add_CB_onUserLeft((userIds) =>
            {

            });
            meetingParticipantsController.Add_CB_onUserNameChanged((userId, userName) =>
            {

            });

            CZoomSDKeDotNetWrap.Instance.GetUIHookControllerWrap().Add_CB_onUIActionNotify((type, msg) =>
            {
                if (type == UIHOOKHWNDTYPE.UIHOOKWNDTYPE_MAINWND)
                {
                    if (!_wndMsgHandled)
                    {
                        _wndMsgHandled = true;

                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            Hwnds hwnds = CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetUIController().GetMeetingUIWnds();

                            Win32APIs.SetWindowLong(hwnds.firstViewHandle, -16, 369164288);
                            Win32APIs.SetParent(hwnds.firstViewHandle, new WindowInteropHelper(_meetingView).Handle);

                            _meetingView.SyncVideoUI();


                        }));
                    }
                }
            });
        }

        private void CustomMeetingUI()
        {
            CMeetingConfigurationDotNetWrap.Instance.SetSharingToolbarVisibility(false);
            CMeetingUIControllerDotNetWrap.Instance.ShowSharingToolbar(false);

            //CMeetingUIControllerDotNetWrap.Instance.ShowSharingToolbar(false);

            CMeetingConfigurationDotNetWrap.Instance.HideMeetingInfoFromMeetingUITitle(true);
            CMeetingConfigurationDotNetWrap.Instance.SetBottomFloatToolbarWndVisibility(false);
            CMeetingConfigurationDotNetWrap.Instance.EnableEnterAndExitFullScreenButtonOnMeetingUI(false);
            CMeetingConfigurationDotNetWrap.Instance.EnableLButtonDBClick4SwitchFullScreenMode(false);
        }

        private void StartHook()
        {
            _wndMsgHandled = false;

            IUIHookControllerDotNetWrap iUIHook = CZoomSDKeDotNetWrap.Instance.GetUIHookControllerWrap();
            iUIHook.MonitorWnd("ZPContentViewWndClass", true);
            iUIHook.Start();
        }

        private void StopHook()
        {
            IUIHookControllerDotNetWrap iUIHook = CZoomSDKeDotNetWrap.Instance.GetUIHookControllerWrap();
            //iUIHook.MonitorWnd("ZPContentViewWndClass", false);
            //iUIHook.Stop();
        }

    }
}
