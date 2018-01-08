#pragma once

#include "auth_service_interface.h"
#include "meeting_service_interface.h"
#include "meeting_service_components\meeting_video_interface.h"
#include "ui_hook_interface.h"

typedef void(*func_callback)(int callback_id, void* data);

typedef struct _tagAuthenticationResult {
	ZOOM_SDK_NAMESPACE::AuthResult result;
}AuthenticationResult;

typedef struct _tagLoginResult {
	ZOOM_SDK_NAMESPACE::LOGINSTATUS status;
	const wchar_t* displayName;
	ZOOM_SDK_NAMESPACE::LoginType loginType;
}LoginResult;

typedef struct _tagMeetingStatusReslut {
	ZOOM_SDK_NAMESPACE::MeetingStatus status;
	int result;
}MeetingStatusResult;

typedef struct _tagWarningResult {
	ZOOM_SDK_NAMESPACE::StatisticsWarningType type;
}WarningResult;

typedef struct _tagUINotifyResult {
	ZOOM_SDK_NAMESPACE::UIHOOKHWNDTYPE type;
	void* handle;
	unsigned int messageId;
}UINotifyResult;

typedef struct _tagVideoStatusResult {
	unsigned int userId;
	ZOOM_SDK_NAMESPACE::VideoStatus status;
}VideoStatusResult;

typedef struct _tagDeviceInfoResult {
	const wchar_t* deviceId;
	const wchar_t* deviceName;
	bool isSelected;
}DeviceInfoResult;


enum CallbackID {
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