using Classroom.Events;
using Classroom.Models;
using Classroom.sdk_wrap;
using Classroom.Services;
using MaterialDesignThemes.Wpf;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using ZOOM_SDK_DOTNET_WRAP;

namespace Classroom.ViewModels
{
    public class UiStatusModel:BindableBase
    {
        public UiStatusModel()
        {
            Microphones = new ObservableCollection<DeviceModel>();
            Speakers = new ObservableCollection<DeviceModel>();
            Cameras = new ObservableCollection<DeviceModel>();
        }

        public const string MicOnText = "静音";
        public const string MicOffText = "解除静音";

        public const string CameraOnText = "停止视频";
        public const string CameraOffText = "启动视频";

        public const string RecordPauseText = "暂停录制";
        public const string RecordResumeText = "恢复录制";

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

        private bool _isRecording;

        public bool IsRecording
        {
            get { return _isRecording; }
            set { SetProperty(ref _isRecording, value); }
        }

        private string _pauseResumeKind;

        public string PauseResumeKind
        {
            get { return _pauseResumeKind; }
            set { SetProperty(ref _pauseResumeKind, value); }
        }


        private string _pauseResumeText;

        public string PauseResumeText
        {
            get { return _pauseResumeText; }
            set { SetProperty(ref _pauseResumeText, value); }
        }




        public ObservableCollection<DeviceModel> Microphones { get; set; }
        public ObservableCollection<DeviceModel> Speakers { get; set; }
        public ObservableCollection<DeviceModel> Cameras { get; set; }
    }

    public class MeetingViewModel
    {
        public UiStatusModel UiStatusModel { get; set; }

        public IntPtr MeetingViewHandle { get; set; }

        public MeetingViewModel()
        {
            SubscribeEvents();

            UiStatusModel = new UiStatusModel()
            {
                CameraIcon = PackIconKind.Video.ToString(),
                MicIcon = PackIconKind.Microphone.ToString(),
                CameraStatus = UiStatusModel.CameraOnText,
                MicStatus = UiStatusModel.MicOnText,
                IsRecording = false,
                PauseResumeKind = PackIconKind.Pause.ToString(),
                PauseResumeText = UiStatusModel.RecordPauseText,
            };

            MeetingViewHandle = IntPtr.Zero;
        }



        private SubscriptionToken _micToken;
        private SubscriptionToken _cameraToken;
        private SubscriptionToken _audioSettingToken;
        private SubscriptionToken _videoSettingToken;
        private SubscriptionToken _deviceSelectedToken;
        private SubscriptionToken _recordToken;
        private SubscriptionToken _shareToken;

        private void SubscribeEvents()
        {
            _micToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<MicStatusChangeEvent>().Subscribe((argument) =>
            {
                switch (UiStatusModel.MicStatus)
                {
                    case UiStatusModel.MicOnText:
                        UiStatusModel.MicStatus = UiStatusModel.MicOffText;
                        UiStatusModel.MicIcon = PackIconKind.MicrophoneOff.ToString();

                        SDKError muteAudioErr = CMeetingAudioControllerDotNetWrap.Instance.MuteAudio(16778240, true);
                        break;
                    case UiStatusModel.MicOffText:
                        UiStatusModel.MicStatus = UiStatusModel.MicOnText;
                        UiStatusModel.MicIcon = PackIconKind.Microphone.ToString();
                        SDKError unmuteAudioErr = CMeetingAudioControllerDotNetWrap.Instance.UnMuteAudio(16778240);
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

                        ZOOM_SDK_DOTNET_WRAP.CMeetingVideoControllerDotNetWrap.Instance.MuteVideo();

                        break;
                    case UiStatusModel.CameraOffText:
                        UiStatusModel.CameraStatus = UiStatusModel.CameraOnText;
                        UiStatusModel.CameraIcon = PackIconKind.Camera.ToString();

                        ZOOM_SDK_DOTNET_WRAP.CMeetingVideoControllerDotNetWrap.Instance.UnmuteVideo();
                        break;
                }
            }, ThreadOption.PublisherThread, true, filter => { return filter.Target == Target.MeetingViewModel; });

            _audioSettingToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<AudioSettingsOpenEvent>().Subscribe((argument) =>
            {
                UiStatusModel.Microphones.Clear();
                UiStatusModel.Speakers.Clear();

                IMicInfoDotNetWrap[] mics = CAudioSettingContextDotNetWrap.Instance.GetMicList();

                if (mics?.Length > 0)
                {
                    foreach (IMicInfoDotNetWrap mic in mics)
                    {
                        UiStatusModel.Microphones.Add(new DeviceModel(mic.GetDeviceId(), mic.GetDeviceName(), mic.IsSelectedDevice()));
                    }
                }

                ISpeakerInfoDotNetWrap[] speakers = CAudioSettingContextDotNetWrap.Instance.GetSpeakerList();

                if (speakers?.Length > 0)
                {
                    foreach (ISpeakerInfoDotNetWrap speaker in speakers)
                    {
                        UiStatusModel.Speakers.Add(new DeviceModel(speaker.GetDeviceId(), speaker.GetDeviceName(), speaker.IsSelectedDevice()));
                    }
                }


            }, ThreadOption.PublisherThread, true, filter => { return filter.Target == Target.MeetingViewModel; });

            _videoSettingToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<VideoSettingsOpenEvent>().Subscribe((argument) =>
            {
                UiStatusModel.Cameras.Clear();

                ICameraInfoDotNetWrap[] cameras = CVideoSettingContextDotNetWrap.Instance.GetCameraList();

                if (cameras?.Length > 0)
                {
                    foreach (ICameraInfoDotNetWrap camera in cameras)
                    {
                        UiStatusModel.Cameras.Add(new DeviceModel(camera.GetDeviceId(), camera.GetDeviceName(), camera.IsSelectedDevice()));
                    }
                }


            }, ThreadOption.PublisherThread, true, filter => { return filter.Target == Target.MeetingViewModel; });

            _deviceSelectedToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<SelectedDeviceChangeEvent>().Subscribe((argument) =>
            {
                DeviceModel device = argument.Argument.Value as DeviceModel;

                if (device == null)
                {
                    return;
                }

                switch (argument.Argument.Category)
                {
                    case Category.Mic:
                        CAudioSettingContextDotNetWrap.Instance.SelectMic(device.Id, device.Name);
                        break;
                    case Category.Speaker:
                        CAudioSettingContextDotNetWrap.Instance.SelectSpeaker(device.Id, device.Name);
                        break;
                    case Category.Camera:
                        CVideoSettingContextDotNetWrap.Instance.SelectCamera(device.Id);
                        break;
                }
            }, ThreadOption.PublisherThread, true, filter => { return filter.Target == Target.MeetingViewModel; });

            _recordToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<RecordStatusChangeEvent>().Subscribe((argument) =>
            {
                switch (argument.Argument.Category)
                {
                    case Category.RecordStart:

                        UiStatusModel.IsRecording = true;

                        break;
                    case Category.RecordPause:

                        UiStatusModel.PauseResumeKind = PackIconKind.Play.ToString();
                        UiStatusModel.PauseResumeText = UiStatusModel.RecordResumeText;

                        break;
                    case Category.RecordResume:

                        UiStatusModel.PauseResumeKind = PackIconKind.Pause.ToString();
                        UiStatusModel.PauseResumeText = UiStatusModel.RecordPauseText;
                        break;
                    case Category.RecordStop:

                        UiStatusModel.IsRecording = false;

                        break;
                }
            }, ThreadOption.PublisherThread, true, filter => { return filter.Target == Target.MeetingViewModel; });

            _shareToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<OpenShareDialogEvent>().Subscribe((argument) =>
            {
                //CMeetingShareControllerDotNetWrap.Instance.ShowShareOptionDialog();
                //ZPSelAppWndClass



                CMeetingShareControllerDotNetWrap.Instance.StartAppShare(new HWNDDotNet() { value = (uint)MeetingViewHandle.ToInt32() });
            }, ThreadOption.PublisherThread, true, filter => { return filter.Target == Target.MeetingViewModel; });
        }

        public void UnsubscribeEvents()
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<MicStatusChangeEvent>().Unsubscribe(_micToken);
            EventAggregatorManager.Instance.EventAggregator.GetEvent<CameraStatusChangeEvent>().Unsubscribe(_cameraToken);
            EventAggregatorManager.Instance.EventAggregator.GetEvent<AudioSettingsOpenEvent>().Unsubscribe(_audioSettingToken);
            EventAggregatorManager.Instance.EventAggregator.GetEvent<VideoSettingsOpenEvent>().Unsubscribe(_videoSettingToken);
            EventAggregatorManager.Instance.EventAggregator.GetEvent<SelectedDeviceChangeEvent>().Unsubscribe(_deviceSelectedToken);
            EventAggregatorManager.Instance.EventAggregator.GetEvent<OpenShareDialogEvent>().Unsubscribe(_shareToken);
        }
    }
}
