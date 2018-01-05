#pragma once

#include "auth_service_interface.h"
#include "meeting_service_interface.h"
#include "ui_hook_interface.h"

typedef void(*func_callback)(int callback_id, void* data);

typedef struct _tagAuthenticationResult {
	ZOOM_SDK_NAMESPACE::AuthResult result;
}AuthenticationResult;

typedef struct _tagLoginResult {
	ZOOM_SDK_NAMESPACE::LOGINSTATUS status;
	ZOOM_SDK_NAMESPACE::IAccountInfo* accountInfo;
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
	MSG msg;
}UINotifyResult;


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