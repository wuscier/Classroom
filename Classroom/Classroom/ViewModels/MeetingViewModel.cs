using Classroom.Events;
using Classroom.Services;
using MaterialDesignThemes.Wpf;
using Prism.Events;
using Prism.Mvvm;

namespace Classroom.ViewModels
{
    public class UiStatusModel:BindableBase
    {
        public const string MicOnText = "静音";
        public const string MicOffText = "解除静音";

        public const string CameraOnText = "停止视频";
        public const string CameraOffText = "启动视频";

        private string _micStatus;

        public string MicStatus
        {
            get { return _micStatus; }
            set { SetProperty(ref _micStatus, value); }
        }

        private string _micIcon;

        public string MicIcon
        {
            get { return _micIcon; }
            set { SetProperty(ref _micIcon, value); }
        }


        private string _cameraStatus;

        public string CameraStatus
        {
            get { return _cameraStatus; }
            set { SetProperty(ref _cameraStatus, value); }
        }

        private string _cameraIcon;

        public string CameraIcon
        {
            get { return _cameraIcon; }
            set { SetProperty(ref _cameraIcon, value); }
        }



    }

    public class MeetingViewModel
    {
        public UiStatusModel UiStatusModel { get; set; }

        public MeetingViewModel()
        {
            SubscribeEvents();

            UiStatusModel = new UiStatusModel()
            {
                CameraIcon = PackIconKind.Video.ToString(),
                MicIcon = PackIconKind.Microphone.ToString(),
                CameraStatus = UiStatusModel.CameraOnText,
                MicStatus = UiStatusModel.MicOnText,
            };
        }


        private SubscriptionToken _micToken;
        private SubscriptionToken _cameraToken;
        private void SubscribeEvents()
        {
            _micToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<MicStatusChangeEvent>().Subscribe((argument) =>
            {
                switch (UiStatusModel.MicStatus)
                {
                    case UiStatusModel.MicOnText:
                        UiStatusModel.MicStatus = UiStatusModel.MicOffText;
                        UiStatusModel.MicIcon = PackIconKind.MicrophoneOff.ToString();


                        break;
                    case UiStatusModel.MicOffText:
                        UiStatusModel.MicStatus = UiStatusModel.MicOnText;
                        UiStatusModel.MicIcon = PackIconKind.Microphone.ToString();

                        break;
                }
            }, ThreadOption.PublisherThread, true, filter => { return filter.Target == Target.MeetingViewModel; });

            _cameraToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<CameraStatusChangeEvent>().Subscribe((argument) =>
            {
                switch (UiStatusModel.CameraStatus)
                {
                    case UiStatusModel.CameraOnText:
                        UiStatusModel.CameraStatus = UiStatusModel.CameraOffText;
                        UiStatusModel.CameraIcon = PackIconKind.CameraOff.ToString();


                        break;
                    case UiStatusModel.CameraOffText:
                        UiStatusModel.CameraStatus = UiStatusModel.CameraOnText;
                        UiStatusModel.CameraIcon = PackIconKind.Camera.ToString();

                        break;
                }
            }, ThreadOption.PublisherThread, true, filter => { return filter.Target == Target.MeetingViewModel; });

        }

        public void UnsubscribeEvents()
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<MicStatusChangeEvent>().Unsubscribe(_micToken);
            EventAggregatorManager.Instance.EventAggregator.GetEvent<CameraStatusChangeEvent>().Unsubscribe(_cameraToken);
        }
    }
}
