using System;
using System.Windows;
using Classroom.Events;
using Classroom.sdk_wrap;
using Classroom.Services;
using Classroom.Views;
using Prism.Events;
using Prism.Mvvm;

namespace Classroom.ViewModels
{
    public class JoinMeetingViewModel : BindableBase
    {

        public JoinMeetingViewModel()
        {
            MeetingNumber = "286683782";
            SubscribeEvents();
        }

        private SubscriptionToken _joinToken;

        private void SubscribeEvents()
        {
            _joinToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<JoinMeetingEvent>().Subscribe((argument) =>
            {
                if (string.IsNullOrEmpty(MeetingNumber))
                {
                    JoinError = "请输入课堂号！";
                    return;
                }

                ulong uint_meeting_number;
                if (ulong.TryParse(MeetingNumber, out uint_meeting_number))
                {
                    JoinParam joinParam = new JoinParam()
                    {
                        usertype = SDKUserType.SDK_UT_APIUSER,
                        JoinParamStruct = new JoinParamStruct()
                        {
                            apiuserJoin = new JoinParam4APIUser()
                            {
                                meetingNumber = uint_meeting_number,
                                hDirectShareAppWnd = IntPtr.Zero,
                                isAudioOff = 0,
                                isDirectShareDesktop = 0,
                                isVideoOff = 0,
                                participantId = string.Empty,
                                psw = string.Empty,
                                toke4enfrocelogin = string.Empty,
                                userName = "吴叙吴叙",
                                webinarToken = string.Empty,
                                //dumy11 = 0,
                                //dumy12 = 0,
                                //dumy13 = 0,
                                //dumy21 = 0,
                                //dumy22 = 0,
                                //dumy23 = 0,
                                //dumy31 = 0,
                                //dumy32 = 0,
                                //dumy33 =0,
                            },

                            normaluserJoin = new JoinParam4NormalUser()
                            {
                                meetingNumber = uint_meeting_number,
                                hDirectShareAppWnd = IntPtr.Zero,
                                isAudioOff = 0,
                                isDirectShareDesktop = 0,
                                isVideoOff = 0,
                                participantId = string.Empty,
                                psw = string.Empty,
                                userName = "wuxuwuxu",
                                webinarToken = string.Empty,
                                //dumy11 = 0,
                                //dumy12 = 0,
                                //dumy13 = 0,
                                //dumy21 = 0,
                                //dumy22 = 0,
                                //dumy23 = 0,
                                //dumy31 = 0,
                                //dumy32 = 0,
                                //dumy33 = 0,

                            }

                        },
                    };

                    SDKError joinError = SdkInterop.Join(joinParam);

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
                else
                {
                    JoinError = "请输入有效的课堂号！";
                }
                

            }, Prism.Events.ThreadOption.PublisherThread, true, filter => { return filter.Target == Target.JoinMeetingViewModel; });
        }

        private void UnsubscribeEvents()
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<JoinMeetingEvent>().Unsubscribe(_joinToken);
        }

        private string _meetingNumber;

        public string MeetingNumber
        {
            get { return _meetingNumber; }
            set { SetProperty(ref _meetingNumber, value); }
        }



        private string _joinError;
        public string JoinError
        {
            get { return _joinError; }
            set { SetProperty(ref _joinError, value); }
        }


    }
}
