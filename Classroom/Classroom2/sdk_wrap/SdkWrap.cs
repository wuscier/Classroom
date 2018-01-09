using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Classroom.sdk_wrap
{
    public class SdkWrap : ISdkWrap
    {
        private SdkWrap()
        {

        }

        public static readonly SdkWrap Instance = new SdkWrap();

        private static FuncCallback FUNCCALLBACK;

        public uint UserId { get; set; }

        public SDKError InitSdk()
        {
            InitParam param = new InitParam();

            param.BrandingName = "云课堂";

            param.WebDomain = "https://zoom.us";
            param.LanguageId = SDKLanguageId.LANGUAGE_Chinese_Simplified;

            SDKError err = SdkInterop.InitSdk(param);

            return err;
        }

        public SDKError UninitSdk()
        {
            SDKError err = SdkInterop.UninitSdk();

            return err;
        }

        public SDKError SDKAuth(AuthParam authParam)
        {
            SDKError err = SdkInterop.SDKAuth(authParam);
            return err;
        }

        public SDKError Start(StartParam startParam)
        {
            SDKError err = SdkInterop.Start(startParam);

            return err;
        }

        public SDKError Login(LoginParam loginParam)
        {
            SDKError err = SdkInterop.Login(loginParam);

            return err;
        }

        public bool InitSdkWrap()
        {
            FUNCCALLBACK = Callback;
            return SdkInterop.InitSdkWrap(FUNCCALLBACK);
        }

        public bool UninitSdkWrap()
        {
            return SdkInterop.UninitSdkWrap();
        }

        public delegate void AuthenticationReturn(AuthenticationResult result);
        public event AuthenticationReturn AuthenticationReturnEvent;

        public delegate void LoginRet(LoginResult result);
        public event LoginRet LoginRetEvent;

        public delegate void UIActionNotify(UINotifyResult result);
        public event UIActionNotify UIActionNotifyEvent;

        public event Action LogoutEvent;

        public delegate void MeetingStatusChanged(MeetingStatusResult result);
        public event MeetingStatusChanged MeetingStatusChangedEvent;

        public delegate void MeetingStatisticsWarningNotification(WarningResult result);
        public event MeetingStatisticsWarningNotification WarningNotificationEvent;

        public delegate void UserVideoStatusChange(VideoStatusResult result);
        public event UserVideoStatusChange UserVideoStatusChangeEvent;

        public delegate void Recording2MP4Done(Recording2MP4DoneResult result);
        public event Recording2MP4Done Recording2MP4DoneEvent;

        public delegate void Recording2MP4Processing(Recording2MP4PercentageResult result);
        public event Recording2MP4Processing Recording2MP4ProcessingEvent;

        public delegate void RecordingStatus(RecordingStatusResult result);
        public event RecordingStatus RecordingStatusEvent;

        public delegate void RecordPriviligeChanged(RecordPriviligeChangedResult result);
        public event RecordPriviligeChanged RecordPriviligeChangedEvent;

        public  void Callback(int callbackId, IntPtr data)
        {
            CallbackID callbackID = (CallbackID)callbackId;

            switch (callbackID)
            {
                case CallbackID.AuthenticationReturn:

                    AuthenticationResult authenticationResult = Marshal.PtrToStructure<AuthenticationResult>(data);
                    AuthenticationReturnEvent?.Invoke(authenticationResult);

                    break;
                case CallbackID.LoginRet:

                    LoginResult loginResult = Marshal.PtrToStructure<LoginResult>(data);
                    LoginRetEvent?.Invoke(loginResult);

                    break;
                case CallbackID.Logout:

                    LogoutEvent?.Invoke();

                    break;
                case CallbackID.MeetingStatusChanged:

                    MeetingStatusResult meetingStatusResult = Marshal.PtrToStructure<MeetingStatusResult>(data);
                    MeetingStatusChangedEvent?.Invoke(meetingStatusResult);

                    break;
                case CallbackID.MeetingStatisticsWarningNotification:

                    WarningResult warningResult = Marshal.PtrToStructure<WarningResult>(data);
                    WarningNotificationEvent?.Invoke(warningResult);

                    break;
                case CallbackID.MeetingSecureKeyNotification:
                    break;
                case CallbackID.InputMeetingPasswordAndScreenNameNotification:
                    break;
                case CallbackID.AirPlayInstructionWndNotification:
                    break;
                case CallbackID.UserAudioStatusChange:
                    break;
                case CallbackID.UserActiveAudioChange:
                    break;
                case CallbackID.UserVideoStatusChange:

                    VideoStatusResult videoStatusResult = Marshal.PtrToStructure<VideoStatusResult>(data);
                    UserVideoStatusChangeEvent?.Invoke(videoStatusResult);

                    break;
                case CallbackID.SpotlightVideoChangeNotification:
                    break;
                case CallbackID.UserJoin:
                    break;
                case CallbackID.UserLeft:
                    break;
                case CallbackID.HostChangeNotification:
                    break;
                case CallbackID.LowOrRaiseHandStatusChanged:
                    break;
                case CallbackID.UserNameChanged:
                    break;
                case CallbackID.Recording2MP4Done:
                    Recording2MP4DoneResult recording2MP4DoneResult = Marshal.PtrToStructure<Recording2MP4DoneResult>(data);

                    Recording2MP4DoneEvent?.Invoke(recording2MP4DoneResult);
                    break;
                case CallbackID.Recording2MP4Processing:
                    Recording2MP4PercentageResult recording2MP4PercentageResult = Marshal.PtrToStructure<Recording2MP4PercentageResult>(data);

                    Recording2MP4ProcessingEvent?.Invoke(recording2MP4PercentageResult);
                    break;
                case CallbackID.RecordingStatus:
                    RecordingStatusResult recordingStatusResult = Marshal.PtrToStructure<RecordingStatusResult>(data);

                    RecordingStatusEvent?.Invoke(recordingStatusResult);

                    
                    break;
                case CallbackID.RecordPriviligeChanged:

                    RecordPriviligeChangedResult recordPriviligeChangedResult = Marshal.PtrToStructure<RecordPriviligeChangedResult>(data);

                    RecordPriviligeChangedEvent?.Invoke(recordPriviligeChangedResult);

                    break;
                case CallbackID.SharingStatus:
                    break;
                case CallbackID.LockShareStatus:
                    break;
                case CallbackID.ShareContentNotification:
                    break;
                case CallbackID.InviteBtnClicked:
                    break;
                case CallbackID.StartShareBtnClicked:
                    break;
                case CallbackID.EndMeetingBtnClicked:
                    break;
                case CallbackID.UIActionNotify:

                    UINotifyResult notifyResult = Marshal.PtrToStructure<UINotifyResult>(data);
                    UIActionNotifyEvent?.Invoke(notifyResult);

                    break;
                case CallbackID.ProxyDetectComplete:
                    break;
                case CallbackID.ProxySettingNotification:
                    break;
                case CallbackID.SSLCertVerifyNotification:
                    break;
                default:
                    break;
            }
        }

        public SDKError GetMeetingUIWnd(ref IntPtr firstViewHwnd, ref IntPtr secondViewHwnd)
        {
            firstViewHwnd = IntPtr.Zero;
            secondViewHwnd = IntPtr.Zero;

            IntPtr first = IntPtr.Zero;
            IntPtr second = IntPtr.Zero;

            SDKError err = SDKError.SDKERR_SUCCESS;

            try
            {
                first = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>());
                second = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>());
                err = SdkInterop.GetMeetingUIWnd(first, second);

                IntPtr firstValue = Marshal.PtrToStructure<IntPtr>(first);
                IntPtr secondValue = Marshal.PtrToStructure<IntPtr>(second);

                firstViewHwnd = firstValue;
                secondViewHwnd = secondValue;
            }
            catch (Exception e)
            {
            }
            finally
            {
                if (first != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(first);
                }
                if (second != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(second);
                }
            }

            return err;
        }

        public SDKError MuteAudio(uint userId, bool allowUnmuteBySelf)
        {
            return SdkInterop.MuteAudio(userId, allowUnmuteBySelf);
        }

        public SDKError UnMuteAudio(uint userId)
        {
            return SdkInterop.UnMuteAudio(userId);
        }

        public SDKError MuteVideo()
        {
            return SdkInterop.MuteVideo();
        }

        public SDKError UnmuteVideo()
        {
            return SdkInterop.UnMuteVideo();
        }

        public IList<DeviceInfoResult> GetMicList()
        {
            IntPtr ptr = IntPtr.Zero;
            IList<DeviceInfoResult> mics = new List<DeviceInfoResult>();

            try
            {
                int size = Marshal.SizeOf<DeviceInfoResult>();
                ptr = Marshal.AllocHGlobal(size * 10);

                int count = SdkInterop.GetMicList(ptr);

                for (int i = 0; i < count; i++)
                {
                    IntPtr p = (IntPtr)(ptr.ToInt64() + i * size);

                    DeviceInfoResult device = Marshal.PtrToStructure<DeviceInfoResult>(p);
                    mics.Add(device);
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }

            return mics;
        }

        public IList<DeviceInfoResult> GetSpeakerList()
        {
            IList<DeviceInfoResult> speakers = new List<DeviceInfoResult>();
            IntPtr ptr = IntPtr.Zero;

            try
            {
                int size = Marshal.SizeOf<DeviceInfoResult>();

                ptr = Marshal.AllocHGlobal(size * 10);

                int count = SdkInterop.GetSpeakerList(ptr);

                for (int i = 0; i < count; i++)
                {
                    IntPtr p = (IntPtr)(ptr.ToInt64() + i * size);
                    DeviceInfoResult device = Marshal.PtrToStructure<DeviceInfoResult>(p);
                    speakers.Add(device);
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                if (ptr!=IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }

            return speakers;
        }

        public IList<DeviceInfoResult> GetCameraList()
        {
            IList<DeviceInfoResult> cameras = new List<DeviceInfoResult>();
            IntPtr ptr = IntPtr.Zero;

            try
            {
                int size = Marshal.SizeOf<DeviceInfoResult>();
                ptr = Marshal.AllocHGlobal(size * 10);

                int count = SdkInterop.GetCameraList(ptr);

                for (int i = 0; i < count; i++)
                {
                    IntPtr p = (IntPtr)(ptr.ToInt64() + i * size);
                    DeviceInfoResult device = Marshal.PtrToStructure<DeviceInfoResult>(p);
                    cameras.Add(device);
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }

            return cameras;
        }

        public SDKError SelectMic(string id, string name)
        {
            return SdkInterop.SelectMic(id, name);
        }

        public SDKError SelectSpeaker(string id, string name)
        {
            return SdkInterop.SelectSpeaker(id, name);
        }

        public SDKError SelectCamera(string id)
        {
            return SdkInterop.SelectCamera(id);
        }

        public SDKError StartRecording(ulong startTime, string path)
        {
            return SdkInterop.StartRecording(startTime, path);
        }

        public SDKError StopRecording(ulong endTime)
        {
            return SdkInterop.StopRecording(endTime);
        }

        public SDKError MonitorWndMessage(uint msgId, bool add)
        {
            return SdkInterop.MonitorWndMessage(msgId, add);
        }

        public SDKError MonitorWnd(string className, bool add)
        {
            return SdkInterop.MonitorWnd(className, add);
        }

        public SDKError StartMonitor()
        {
            return SdkInterop.StartMonitor();
        }

        public SDKError StopMonitor()
        {
            return StopMonitor();
        }

        public SDKError Leave(LeaveMeetingCmd cmd)
        {
            return SdkInterop.Leave(cmd);
        }
    }


}
