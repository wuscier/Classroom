using Classroom.Events;
using Classroom.Helpers;
using Classroom.Models;
using Classroom.sdk_wrap;
using Classroom.Services;
using Classroom.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using ZOOM_SDK_DOTNET_WRAP;

namespace Classroom.ViewModels
{
    public class Main2ViewModel:BindableBase
    {
        public Main2ViewModel()
        {

            SubscribeEvents();

            InitData();

            IsMainCardSelected = true;
        }

        private void InitData()
        {
            JoinCommand = new DelegateCommand<CourseModel>((course) =>
            {
                ulong uint_meeting_number;
                if (!ulong.TryParse(course.MeetingNumber, out uint_meeting_number))
                {
                    MessageBox.Show("无效的课堂号！");
                    return;
                };

                if (course.HostId == App.UserModel.UserName)
                {
                    CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().Add_CB_onLoginRet((loginStatus, accountInfo) =>
                    {
                        switch (loginStatus)
                        {
                            case LOGINSTATUS.LOGIN_IDLE:
                                break;
                            case LOGINSTATUS.LOGIN_PROCESSING:
                                break;
                            case LOGINSTATUS.LOGIN_SUCCESS:

                                SDKError joinError = CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().Join(new JoinParam()
                                {
                                    userType = SDKUserType.SDK_UT_NORMALUSER,
                                    normaluserJoin = new JoinParam4NormalUser()
                                    {
                                        hDirectShareAppWnd = new HWNDDotNet() { value = 0 },
                                        isAudioOff = false,
                                        isDirectShareDesktop = false,
                                        isVideoOff = false,
                                        meetingNumber = uint_meeting_number,
                                        participantId = string.Empty,
                                        psw = string.Empty,
                                        userName = "主持人",
                                        webinarToken = string.Empty,
                                    }
                                });

                                if (joinError == SDKError.SDKERR_SUCCESS)
                                {
                                    MeetingView meetingView = new MeetingView();
                                    meetingView.Show();
                                }
                                else
                                {
                                    MessageBox.Show(joinError.ToString());
                                }



                                break;
                            case LOGINSTATUS.LOGIN_FAILED:
                                break;
                            default:
                                break;
                        }
                    });

                    SDKError loginError = CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().Login(new LoginParam()
                    {
                        loginType = LoginType.LoginType_Email,
                        emailLogin = new LoginParam4Email()
                        {
                            bRememberMe = true,
                            password = "justlucky",
                            userName = course.HostId,
                        }
                    });
                }
                else
                {
                    SDKError joinError = CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().Join(new JoinParam()
                    {
                        userType = SDKUserType.SDK_UT_APIUSER,
                        apiuserJoin = new JoinParam4APIUser()
                        {
                            userName = "我是API用户",
                            meetingNumber = uint_meeting_number,
                            psw = string.Empty,
                            hDirectShareAppWnd = new HWNDDotNet() { value = 0 },
                            isAudioOff = false,
                            isDirectShareDesktop = false,
                            isVideoOff = false,
                            participantId = string.Empty,
                            toke4enfrocelogin = string.Empty,
                            webinarToken = string.Empty,
                        }
                    });

                    if (joinError == SDKError.SDKERR_SUCCESS)
                    {
                        MeetingView meetingView = new MeetingView();
                        meetingView.Show();
                    }
                    else
                    {
                        MessageBox.Show(joinError.ToString());
                    }
                }
            });

            CourseList = new ObservableCollection<CourseModel>();
            CourseList.Add(new CourseModel()
            {
                Duration = "8:00 - 9:00",
                Name = "语文",
                TeacherName = "马云",
                MeetingNumber = "286683782",
                HostId = "justlucky@126.com",
                JoinCommand = JoinCommand,
            });
            CourseList.Add(new CourseModel()
            {
                Duration = "11:00 - 12:00",
                Name = "数学",
                TeacherName = "刘强东",
                MeetingNumber = "286683782",
                HostId = "justlucky@126.com",
                JoinCommand = JoinCommand,

            });
            CourseList.Add(new CourseModel()
            {
                Duration = "14:00 - 15:00",
                Name = "生物",
                TeacherName = "李海波",
                MeetingNumber = "286683782",
                HostId = "justlucky@126.com",
                JoinCommand = JoinCommand,

            });
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

        public ICommand JoinCommand { get; set; }

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
