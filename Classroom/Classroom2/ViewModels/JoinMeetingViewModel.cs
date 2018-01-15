using System;
using Classroom.Events;
using Classroom.sdk_wrap;
using Classroom.Services;
using Prism.Events;
using Prism.Mvvm;

namespace Classroom.ViewModels
{
    public class JoinMeetingViewModel : BindableBase
    {

        public JoinMeetingViewModel()
        {
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

                uint uint_meeting_number;
                if (uint.TryParse(MeetingNumber, out uint_meeting_number))
                {
                    //SdkInterop.
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
