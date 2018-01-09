using System;
using System.Collections.Generic;

namespace Classroom.sdk_wrap
{
    public interface ISdkWrap
    {
        uint UserId { get; set; }

        bool InitSdkWrap();
        bool UninitSdkWrap();

        SDKError InitSdk();
        SDKError UninitSdk();

        SDKError Login(LoginParam loginParam);
        SDKError SDKAuth(AuthParam authParam);
        SDKError Start(StartParam startParam);
        SDKError Leave(LeaveMeetingCmd cmd);

        SDKError GetMeetingUIWnd(ref IntPtr firstViewHwnd, ref IntPtr secondViewHwnd);

        SDKError MuteAudio(uint userId, bool allowUnmuteBySelf);
        SDKError UnMuteAudio(uint userId);

        SDKError MuteVideo();
        SDKError UnmuteVideo();

        IList<DeviceInfoResult> GetMicList();
        IList<DeviceInfoResult> GetSpeakerList();
        IList<DeviceInfoResult> GetCameraList();

        SDKError SelectMic(string id, string name);
        SDKError SelectSpeaker(string id, string name);
        SDKError SelectCamera(string id);

        SDKError StartRecording(ulong startTime, string path);
        SDKError StopRecording(ulong endTime);

        SDKError MonitorWndMessage(uint msgId, bool add);
        SDKError MonitorWnd(string className, bool add);
        SDKError StartMonitor();
        SDKError StopMonitor();
    }
}
