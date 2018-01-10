using System;
using System.Collections.Generic;

namespace Classroom.sdk_wrap
{
    public interface ISdkWrap
    {
        uint UserId { get; set; }

        SDKError InitSdk();

        IList<DeviceInfoResult> GetMicList();
        IList<DeviceInfoResult> GetSpeakerList();
        IList<DeviceInfoResult> GetCameraList();

        SDKError GetMeetingUIWnd(ref IntPtr firstViewHwnd, ref IntPtr secondViewHwnd);
    }
}
