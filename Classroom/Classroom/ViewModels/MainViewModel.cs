using Classroom.Events;
using Classroom.sdk_wrap;
using Classroom.Services;
using Prism.Events;
using Prism.Mvvm;
using ZOOM_SDK_DOTNET_WRAP;

namespace Classroom.ViewModels
{
    public class MainViewModel:BindableBase
    {
        public MainViewModel()
        {
            SubscribeEvents();

            RegisterCallbacks();

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

        private SubscriptionToken _cardSelectedToken;
        private SubscriptionToken _startClassToken;

        private void SubscribeEvents()
        {
            _cardSelectedToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<CardSelectedEvent>().Subscribe((argument) =>
            {
                switch (argument.Value)
                {
                    case Value.MainCard:
                        IsMainCardSelected = true;
                        IsHistoryCardSelected = false;
                        break;
                    case Value.HistoryCard:
                        IsMainCardSelected = false;
                        IsHistoryCardSelected = true;
                        break;
                }
            }, ThreadOption.PublisherThread, true, filter => { return filter.Target == Target.MainViewModel; });


            _startClassToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<StartClassEvent>().Subscribe((argument) =>
            {
                CustomMeetingUI();

                StartParam startParam = new StartParam()
                {
                    apiuserStart = new StartParam4APIUser()
                    {
                        userID = "704311",
                        userToken = "t0zVp2Doi3PxKpIEkE3YH4iQbfDID8VaoNIl",
                        userName = "big cash",
                        meetingNumber = 3398415968,
                    },
                    userType = SDKUserType.SDK_UT_APIUSER,
                };

                SDKError error = SdkWrap.Instacne.Start(startParam);

                if (error == SDKError.SDKERR_SUCCESS)
                {
                    EventAggregatorManager.Instance.EventAggregator.GetEvent<WindowHideEvent>().Publish(new EventArgument()
                    {
                        Target = Target.MainView,
                    });
                }
                else
                {

                }

            }, ThreadOption.PublisherThread, true, filter => { return filter.Target == Target.MainViewModel; });
        }

        private void UnsubscribeEvents()
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<CardSelectedEvent>().Unsubscribe(_cardSelectedToken);
        }


        private void RegisterCallbacks()
        {
            IMeetingServiceDotNetWrap meetingServiceDotNetWrap = CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap();

            IMeetingParticipantsControllerDotNetWrap meetingParticipantsController = meetingServiceDotNetWrap.GetMeetingParticipantsController();

            meetingServiceDotNetWrap.Add_CB_onMeetingStatusChanged((status,result)=>
            {

            });
            meetingParticipantsController.Add_CB_onHostChangeNotification((userId) =>
            {

            });
            meetingParticipantsController.Add_CB_onLowOrRaiseHandStatusChanged((bLow, userId)=>
            {

            });
            meetingParticipantsController.Add_CB_onUserJoin((userIds)=>
            {
            });
            meetingParticipantsController.Add_CB_onUserLeft((userIds)=>
            {

            });
            meetingParticipantsController.Add_CB_onUserNameChanged((userId,userName)=>
            {

            });

        }

        private void CustomMeetingUI()
        {
            CMeetingConfigurationDotNetWrap.Instance.HideMeetingInfoFromMeetingUITitle(true);
            CMeetingConfigurationDotNetWrap.Instance.SetBottomFloatToolbarWndVisibility(false);
            CMeetingConfigurationDotNetWrap.Instance.EnableEnterAndExitFullScreenButtonOnMeetingUI(false);
            CMeetingConfigurationDotNetWrap.Instance.EnableLButtonDBClick4SwitchFullScreenMode(false);
        }
    }
}
