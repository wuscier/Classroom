using System;
using System.Runtime.InteropServices;

namespace Classroom.sdk_wrap
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FuncCallback(int callbackId, IntPtr data);

    enum CallbackID
    {
        AuthenticationReturn,
        LoginRet,
        Logout,
        MeetingStatusChanged,
        MeetingStatisticsWarningNotification,
        MeetingSecureKeyNotification,
        InputMeetingPasswordAndScreenNameNotification,
        AirPlayInstructionWndNotification,
        UserAudioStatusChange,
        UserActiveAudioChange,
        UserVideoStatusChange,
        SpotlightVideoChangeNotification,
        UserJoin,
        UserLeft,
        HostChangeNotification,
        LowOrRaiseHandStatusChanged,
        UserNameChanged,
        Recording2MP4Done,
        Recording2MP4Processing,
        RecordingStatus,
        RecordPriviligeChanged,
        SharingStatus,
        LockShareStatus,
        ShareContentNotification,
        InviteBtnClicked,
        StartShareBtnClicked,
        EndMeetingBtnClicked,
        UIActionNotify,
        ProxyDetectComplete,
        ProxySettingNotification,
        SSLCertVerifyNotification,
    };
    public enum SDKError
    {
        SDKERR_SUCCESS = 0,///< Success Result
        SDKERR_NO_IMPL,///< Not support this feature now 
        SDKERR_WRONG_USEAGE,///< Wrong useage about this feature 
        SDKERR_INVALID_PARAMETER,///< Wrong parameter 
        SDKERR_MODULE_LOAD_FAILED,///< Load module failed 
        SDKERR_MEMORY_FAILED,///< No memory allocated 
        SDKERR_SERVICE_FAILED,///< Internal service error 
        SDKERR_UNINITIALIZE,///< Not initialize before use 
        SDKERR_UNAUTHENTICATION,///< Not Authentication before use
        SDKERR_NORECORDINGINPROCESS,///< No recording in process
        SDKERR_TRANSCODER_NOFOUND,///< can't find transcoder module
        SDKERR_VIDEO_NOTREADY,///< Video service not ready
        SDKERR_NO_PERMISSION,///< No premission to do this
        SDKERR_UNKNOWN,///< Unknown error 
        SDKERR_OTHER_SDK_INSTANCE_RUNNING,
    };


    public enum SDKLanguageId
    {
        LANGUAGE_Unknow = 0,
        LANGUAGE_English,
        LANGUAGE_Chinese_Simplified,
        LANGUAGE_Chinese_Traditional,
        LANGUAGE_Japanese,
        LANGUAGE_Spanish,
        LANGUAGE_German,
        LANGUAGE_French,
    };


    public enum CustomizedLanguageType
    {
        CustomizedLanguage_None,
        CustomizedLanguage_FilePath,
        CustomizedLanguage_Content,
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct CustomizedLanguageInfo
    {
        [MarshalAs(UnmanagedType.LPStr)]
        public string LanguageName;
        [MarshalAs(UnmanagedType.LPStr)]
        public string LanguageInfo;
        public CustomizedLanguageType LanguageType;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ConfigurableOptions
    {
        public CustomizedLanguageInfo LanguageInfo;
    }

    public enum LoginType
    {
        LoginType_Unknown,
        LoginType_Email,///<Login with work email
        LoginType_SSO,///<Login with SSO token
    };


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct LoginParam4Email
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string UserName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Password;

        public bool RememberMe;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct LoginParam4SSO
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string SsoToken;
    }

    public enum SDKUserType
    {
        SDK_UT_APIUSER = 99,///< API User type
        SDK_UT_NORMALUSER = 100,///< Normal user type
    };


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct StartParam4APIUser
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string UserId;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string UserToken;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string UserName;
        public uint MeetingNumber;
        public IntPtr DirectShareAppWnd;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string ParticipantId;
        public bool IsDirectShareDesktop;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct StartParam4NormalUser
    {
        public uint MeetingNumber;
        public IntPtr DirectShareAppWnd;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string ParticipantId;
        public bool IsVideoOff;
        public bool IsAudioOff;
        public bool IsDirectShareDesktop;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct JoinParam
    {
        public SDKUserType usertype;
        public JoinParam4APIUser apiuserJoin;
        public JoinParam4NormalUser normaluserJoin;
    }




    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct InitParam
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string WebDomain;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string BrandingName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string SupportUrl;
        public IntPtr ResourceInstance;
        public uint SmallIconId;
        public uint BigIconId;
        public SDKLanguageId LanguageId;
        public ConfigurableOptions ConfigOptions;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct AuthParam
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string AppKey;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string AppSecret;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct LoginParam
    {
        public LoginType LoginType;
        public LoginParam4Email EmailLogin;
        public LoginParam4SSO SsoLogin;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct StartParam
    {
        public SDKUserType UserType;
        public StartParam4APIUser ApiUserStart;
        public StartParam4NormalUser NormalUserStart;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct JoinParam4APIUser
    {
        public ulong meetingNumber;///< Meeting's number
        public string userName;///< User Name in meeting
        public string psw;///< Meeting's password
        IntPtr hDirectShareAppWnd;///< share application directly
        public string toke4enfrocelogin;///< enforce login when join meeting
        public string participantId;///< for meeting participant report list, need web backend enable.
        public string webinarToken;///< webinar token.
        public bool isDirectShareDesktop;///< share desktop directly
        public bool isVideoOff;
        public bool isAudioOff;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct JoinParam4NormalUser
    {
        public ulong meetingNumber;///< Meeting's number
        public string userName;///< User Name in meeting
        public string psw;///< Meeting's password
        IntPtr hDirectShareAppWnd;///< share application directly
        public string participantId;///< for meeting participant report list, need web backend enable.
        public string webinarToken;///< webinar token.
        public bool isVideoOff;
        public bool isAudioOff;
        public bool isDirectShareDesktop;///< share desktop directly
    }







    public enum AuthResult
    {
        AUTHRET_SUCCESS,///< Auth Success 
        AUTHRET_KEYORSECRETEMPTY,///< Key or Secret is empty
        AUTHRET_KEYORSECRETWRONG,///< Key or Secret is wrong
        AUTHRET_ACCOUNTNOTSUPPORT,///< Client Account does not support
        AUTHRET_ACCOUNTNOTENABLESDK,///< Client account does not enable SDK
        AUTHRET_UNKNOWN,///< Auth Unknown error
        AUTHRET_NONE,///< Initial status
    };


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct AuthenticationResult
    {
        public AuthResult Result;
    }


    public enum LoginStatus
    {
        LOGIN_IDLE,///< Not login
        LOGIN_PROCESSING,///< Login in processing
        LOGIN_SUCCESS,///< Login success
        LOGIN_FAILED,///< Login failed
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct LoginResult
    {
        public LoginStatus Status;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string DisplayName;
        public LoginType LoginType;
    }

    public enum UIHOOKHWNDTYPE
    {
        UIHOOKWNDTYPE_USERDEFIEND,
        UIHOOKWNDTYPE_MAINWND,
        UIHOOKWNDTYPE_BOTTOMTOOLBAR,
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct UINotifyResult
    {
        public UIHOOKHWNDTYPE type;
        public IntPtr haneld;
        public uint messageId;
    }

    public enum MeetingStatus
    {
        MEETING_STATUS_IDLE,///< Idle status, no meeting running
        MEETING_STATUS_CONNECTING,///< Connecting meeting server status
        MEETING_STATUS_WAITINGFORHOST,///< Waiting for host to start meeting
        MEETING_STATUS_INMEETING,///< Meeting is ready, in meeting status
        MEETING_STATUS_DISCONNECTING,///< Disconnecting meeting server status
        MEETING_STATUS_RECONNECTING,///< Reconnecting meeting server status
        MEETING_STATUS_FAILED,///< Meeting connection error
        MEETING_STATUS_ENDED,///< Meeting is ended
        MEETING_STATUS_UNKNOW,
        MEETING_STATUS_LOCKED,
        MEETING_STATUS_UNLOCKED,
        MEETING_STATUS_IN_WAITING_ROOM,
        MEETING_STATUS_WEBINAR_PROMOTE,
        MEETING_STATUS_WEBINAR_DEPROMOTE,
        MEETING_STATUS_JOIN_BREAKOUT_ROOM,
        MEETING_STATUS_LEAVE_BREAKOUT_ROOM,
        MEETING_STATUS_WAITING_EXTERNAL_SESSION_KEY,
    };


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct MeetingStatusResult
    {
        public MeetingStatus MeetingStatus;
        public int result;
    }

    public enum StatisticsWarningType
    {
        Statistics_Warning_None,
        Statistics_Warning_Network_Quality_Bad,
    };


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct WarningResult
    {
        public StatisticsWarningType WarningType;
    }

    public enum VideoStatus
    {
        Video_ON,
        Video_OFF,
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct VideoStatusResult
    {
        public uint UserId;
        public VideoStatus VideoStatus;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct DeviceInfoResult
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string DeviceId;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string DeviceName;
        public byte IsSelected;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct Recording2MP4DoneResult
    {
        public bool IsSucceeded;
        public int Result;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string RecordPath;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct Recording2MP4PercentageResult
    {
        public int Percentage;
    }

    public enum RecordingStatus
    {
        Recording_Start,
        Recording_Stop,
        Recording_DiskFull,
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct RecordingStatusResult
    {
        public RecordingStatus Status;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct RecordPriviligeChangedResult
    {
        public bool CanRecord;
    }


    public enum LeaveMeetingCmd
    {
        LEAVE_MEETING,///< Leave meeting
        END_MEETING,///< End meeting
    };
}
