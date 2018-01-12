#pragma once

#include "zoom_sdk_wrap_def.h"
#include "zoom_sdk.h"
#include "zoom_sdk_ext.h"
#include "zoom_sdk_def.h"
#include "auth_service_interface.h"
#include "meeting_service_interface.h"
#include "setting_service_interface.h"
#include "network_connection_handler_interface.h"
#include "ui_hook_interface.h"
#include "meeting_service_components\meeting_configuration_interface.h"
#include "meeting_service_components\meeting_audio_interface.h"
#include "meeting_service_components\meeting_video_interface.h"
#include "meeting_service_components\meeting_participants_ctrl_interface.h"
#include "meeting_service_components\meeting_recording_interface.h"
#include "meeting_service_components\meeting_sharing_interface.h"
#include "meeting_service_components\meeting_ui_ctrl_interface.h"

class zoom_sdk_client:
	public ZOOM_SDK_NAMESPACE::IMeetingServiceEvent,
	public ZOOM_SDK_NAMESPACE::IAuthServiceEvent,
	public ZOOM_SDK_NAMESPACE::IMeetingConfigurationEvent,
	public ZOOM_SDK_NAMESPACE::IMeetingAudioCtrlEvent,
	public ZOOM_SDK_NAMESPACE::IMeetingVideoCtrlEvent,
	public ZOOM_SDK_NAMESPACE::IMeetingParticipantsCtrlEvent,
	public ZOOM_SDK_NAMESPACE::IMeetingRecordingCtrlEvent,
	public ZOOM_SDK_NAMESPACE::IMeetingShareCtrlEvent,
	public ZOOM_SDK_NAMESPACE::IMeetingUIControllerEvent,
	public ZOOM_SDK_NAMESPACE::IUIHookerEvent,
	public ZOOM_SDK_NAMESPACE::INetworkConnectionHandler
{
private:
	bool m_bMeetingServiceInited;
	bool m_bAuthServiceInited;

	static zoom_sdk_client* m_instance;
	func_callback m_cb;

	ZOOM_SDK_NAMESPACE::IAuthService* m_pAuthService;


	ZOOM_SDK_NAMESPACE::IMeetingService* m_pMeetingService;
	ZOOM_SDK_NAMESPACE::IMeetingConfiguration* m_pConfigurationService;
	ZOOM_SDK_NAMESPACE::ISettingService* m_pSettingService;

	ZOOM_SDK_NAMESPACE::IMeetingAudioController* m_pAudioCtrl;
	ZOOM_SDK_NAMESPACE::IMeetingVideoController* m_pVideoCtrl;
	ZOOM_SDK_NAMESPACE::IMeetingParticipantsController* m_pParticipantsCtrl;
	ZOOM_SDK_NAMESPACE::IMeetingRecordingController* m_pRecordingCtrl;
	ZOOM_SDK_NAMESPACE::IMeetingShareController* m_pShareCtrl;
	ZOOM_SDK_NAMESPACE::IMeetingUIController* m_pUICtrl;

	ZOOM_SDK_NAMESPACE::IUIHooker* m_pUIHooker;
	ZOOM_SDK_NAMESPACE::INetworkConnectionHelper* m_pNetworkHelper;

	void InvokeCallback(int calbackId, void* data);

public:
	zoom_sdk_client();
	~zoom_sdk_client();

	static zoom_sdk_client* Instance();


public:

	bool InitMeetingService();
	bool InitAuthService();

	ZOOM_SDK_NAMESPACE::SDKError InitSdk(ZOOM_SDK_NAMESPACE::InitParam initParam, func_callback cb);
	ZOOM_SDK_NAMESPACE::SDKError UninitSdk();

	ZOOM_SDK_NAMESPACE::SDKError SDKAuth(ZOOM_SDK_NAMESPACE::AuthParam authParam);
	ZOOM_SDK_NAMESPACE::SDKError Login(ZOOM_SDK_NAMESPACE::LoginParam loginParam);
	ZOOM_SDK_NAMESPACE::SDKError Start(ZOOM_SDK_NAMESPACE::StartParam startParam);
	ZOOM_SDK_NAMESPACE::SDKError Leave(ZOOM_SDK_NAMESPACE::LeaveMeetingCmd leaveCmd);


	ZOOM_SDK_NAMESPACE::SDKError MuteAudio(unsigned int userId, bool allowUnmuteBySelf);
	ZOOM_SDK_NAMESPACE::SDKError UnMuteAudio(unsigned int userId);

	ZOOM_SDK_NAMESPACE::SDKError MuteVideo();
	ZOOM_SDK_NAMESPACE::SDKError UnMuteVideo();

	int GetMicList(DeviceInfoResult* mics);
	int GetSpeakerList(DeviceInfoResult* speakers);
	int GetCameraList(DeviceInfoResult* cameras);

	ZOOM_SDK_NAMESPACE::SDKError SelectMic(const wchar_t* deviceId, const wchar_t* deviceName);
	ZOOM_SDK_NAMESPACE::SDKError SelectSpeaker(const wchar_t* deviceId, const wchar_t* deviceName);
	ZOOM_SDK_NAMESPACE::SDKError SelectCamera(const wchar_t* deviceId);

	ZOOM_SDK_NAMESPACE::SDKError StartRecording(unsigned int startTime, wchar_t* recordPath);
	ZOOM_SDK_NAMESPACE::SDKError StopRecording(unsigned int stopTimestamp);

	ZOOM_SDK_NAMESPACE::SDKError GetMeetingUIWnd(HWND* firstViewHandle, HWND* secondViewHandle);

	ZOOM_SDK_NAMESPACE::SDKError MonitorWndMessage(unsigned int msgId, bool add);
	ZOOM_SDK_NAMESPACE::SDKError MonitorWnd(const wchar_t* className, bool add);
	ZOOM_SDK_NAMESPACE::SDKError Start();
	ZOOM_SDK_NAMESPACE::SDKError Stop();

	void CustomizeUI();


public:
	//IMeetingServiceEvent
	virtual void onMeetingStatusChanged(ZOOM_SDK_NAMESPACE::MeetingStatus status, int iResult = 0);
	virtual void onMeetingStatisticsWarningNotification(ZOOM_SDK_NAMESPACE::StatisticsWarningType type);
	virtual void onMeetingSecureKeyNotification(const char* key, int len, ZOOM_SDK_NAMESPACE::IMeetingExternalSecureKeyHandler* pHandler);

	//IAuthServiceEvent
	virtual void onAuthenticationReturn(ZOOM_SDK_NAMESPACE::AuthResult ret);
	virtual void onLoginRet(ZOOM_SDK_NAMESPACE::LOGINSTATUS ret, ZOOM_SDK_NAMESPACE::IAccountInfo* pAccountInfo);
	virtual void onLogout();

	//IMeetingConfigurationEvent
	virtual void onInputMeetingPasswordAndScreenNameNotification(ZOOM_SDK_NAMESPACE::IMeetingPasswordAndScreenNameHandler* pHandler);
	virtual void onAirPlayInstructionWndNotification(bool bShow, const wchar_t* airhostName);


	//IMeetingAudioCtrlEvent

	virtual void onUserActiveAudioChange(unsigned int userId);

	virtual void onUserAudioStatusChange(ZOOM_SDK_NAMESPACE::IList<ZOOM_SDK_NAMESPACE::IUserAudioStatus* >* lstAudioStatusChange, const wchar_t* strAudioStatusList = NULL);
	//virtual void onUserActiveAudioChange(ZOOM_SDK_NAMESPACE::IList<unsigned int >* plstActiveAudio);

	//IMeetingVideoCtrlEvent
	virtual void onUserVideoStatusChange(unsigned int userId, ZOOM_SDK_NAMESPACE::VideoStatus status);
	virtual void onSpotlightVideoChangeNotification(bool bSpotlight, unsigned int userid);

	//IMeetingParticipantsCtrlEvent
	virtual void onUserJoin(ZOOM_SDK_NAMESPACE::IList<unsigned int >* lstUserID, const wchar_t* strUserList = NULL);
	virtual void onUserLeft(ZOOM_SDK_NAMESPACE::IList<unsigned int >* lstUserID, const wchar_t* strUserList = NULL);
	virtual void onHostChangeNotification(unsigned int userId);
	virtual void onLowOrRaiseHandStatusChanged(bool bLow, unsigned int userid);
	virtual void onUserNameChanged(unsigned int userId, const wchar_t* userName);

	//IMeetingRecordingCtrlEvent
	virtual void onRecording2MP4Done(bool bsuccess, int iResult, const wchar_t* szPath);
	virtual void onRecording2MP4Processing(int iPercentage);
	virtual void onRecordingStatus(ZOOM_SDK_NAMESPACE::RecordingStatus status);
	virtual void onRecordPriviligeChanged(bool bCanRec);

	//IMeetingShareCtrlEvent
	virtual void onSharingStatus(ZOOM_SDK_NAMESPACE::SharingStatus status, unsigned int userId);
	virtual void onLockShareStatus(bool bLocked);
	virtual void onShareContentNotification(ZOOM_SDK_NAMESPACE::ShareInfo& shareInfo);

	//IMeetingUIControllerEvent
	virtual void onInviteBtnClicked();
	virtual void onStartShareBtnClicked();
	virtual void onEndMeetingBtnClicked();

	//IUIHookerEvent
	virtual void onUIActionNotify(ZOOM_SDK_NAMESPACE::UIHOOKHWNDTYPE type, MSG msg);


	//INetworkConnectionHandler
	virtual void onProxyDetectComplete();
	virtual void onProxySettingNotification(ZOOM_SDK_NAMESPACE::IProxySettingHandler* handler);
	virtual void onSSLCertVerifyNotification(ZOOM_SDK_NAMESPACE::ISSLCertVerificationHandler* handler);

};

