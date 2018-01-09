#include "stdafx.h"
#include "zoom_sdk_client.h"

zoom_sdk_client* zoom_sdk_client::m_instance = NULL;

void zoom_sdk_client::InvokeCallback(int calbackId, void * data)
{
	if (m_cb)
	{
		m_cb(calbackId, data);
	}
}

zoom_sdk_client::zoom_sdk_client()
{
	m_bInited = false;

	m_pMeetingService = NULL;
	m_pAuthService = NULL;
	m_pConfigurationService = NULL;
	m_pSettingService = NULL;

	m_pAudioCtrl = NULL;
	m_pVideoCtrl = NULL;
	m_pParticipantsCtrl = NULL;
	m_pRecordingCtrl = NULL;
	m_pShareCtrl = NULL;
	m_pUICtrl = NULL;

	m_pUIHooker = NULL;
	m_pNetworkHelper = NULL;
}


zoom_sdk_client::~zoom_sdk_client()
{
	m_bInited = false;
}

zoom_sdk_client * zoom_sdk_client::Instance()
{
	if (m_instance == NULL)
	{
		m_instance = new zoom_sdk_client();
	}

	return m_instance;
}

bool zoom_sdk_client::InitSdkWrap(func_callback cb)
{
	if (m_bInited)
	{
		return true;
	}

	m_cb = cb;

	if (CreateMeetingService(&m_pMeetingService) != ZOOM_SDK_NAMESPACE::SDKERR_SUCCESS)
	{
		return false;
	}

	if (m_pMeetingService == NULL)
	{
		return false;
	}

	if (m_pMeetingService->SetEvent(this) != ZOOM_SDK_NAMESPACE::SDKERR_SUCCESS)
	{
		return false;
	}


	if (CreateAuthService(&m_pAuthService) != ZOOM_SDK_NAMESPACE::SDKERR_SUCCESS)
	{
		return false;
	}
	

	if (m_pAuthService == NULL)
	{
		return false;
	}

	if (m_pAuthService->SetEvent(this) != ZOOM_SDK_NAMESPACE::SDKERR_SUCCESS)
	{
		return false;
	}

	if (CreateSettingService(&m_pSettingService) != ZOOM_SDK_NAMESPACE::SDKERR_SUCCESS)
	{
		return false;
	}

	if (m_pSettingService == NULL)
	{
		return false;
	}

	if (CreateNetworkConnectionHelper(&m_pNetworkHelper) != ZOOM_SDK_NAMESPACE::SDKERR_SUCCESS)
	{
		return false;
	}

	if (m_pNetworkHelper == NULL)
	{
		return false;
	}

	m_pNetworkHelper->RegisterNetworkConnectionHandler(this);

	if (RetrieveUIHooker(&m_pUIHooker) != ZOOM_SDK_NAMESPACE::SDKERR_SUCCESS)
	{
		return false;
	}

	if (m_pUIHooker == NULL)
	{
		return false;
	}

	m_pUIHooker->SetEvent(this);

	m_pAudioCtrl = m_pMeetingService->GetMeetingAudioController();
	if (m_pAudioCtrl)
	{
		m_pAudioCtrl->SetEvent(this);
	}

	m_pVideoCtrl = m_pMeetingService->GetMeetingVideoController();
	if (m_pVideoCtrl)
	{
		m_pVideoCtrl->SetEvent(this);
	}

	m_pConfigurationService = m_pMeetingService->GetMeetingConfiguration();
	if (m_pConfigurationService)
	{
		m_pConfigurationService->SetEvent(this);
	}

	m_pParticipantsCtrl = m_pMeetingService->GetMeetingParticipantsController();
	if (m_pParticipantsCtrl)
	{
		m_pParticipantsCtrl->SetEvent(this);
	}

	m_pRecordingCtrl = m_pMeetingService->GetMeetingRecordingController();
	if (m_pRecordingCtrl)
	{
		m_pRecordingCtrl->SetEvent(this);
	}

	m_pShareCtrl = m_pMeetingService->GetMeetingShareController();
	if (m_pShareCtrl)
	{
		m_pShareCtrl->SetEvent(this);
	}

	m_pUICtrl = m_pMeetingService->GetUIController();
	if (m_pUICtrl)
	{
		m_pUICtrl->SetEvent(this);
	}

	m_bInited = true;

	return true;
}

bool zoom_sdk_client::UninitSdkWrap()
{
	if (!m_bInited)
	{
		return true;
	}

	m_bInited = false;

	if (ZOOM_SDK_NAMESPACE::DestroyMeetingService(m_pMeetingService) != ZOOM_SDK_NAMESPACE::SDKERR_SUCCESS)
		return false;

	if (ZOOM_SDK_NAMESPACE::DestroySettingService(m_pSettingService) != ZOOM_SDK_NAMESPACE::SDKERR_SUCCESS)
		return false;

	if (ZOOM_SDK_NAMESPACE::DestroyAuthService(m_pAuthService) != ZOOM_SDK_NAMESPACE::SDKERR_SUCCESS)
		return false;
	if (ZOOM_SDK_NAMESPACE::DestroyNetworkConnectionHelper(m_pNetworkHelper) != ZOOM_SDK_NAMESPACE::SDKERR_SUCCESS)
		return false;

	return true;
}

ZOOM_SDK_NAMESPACE::SDKError zoom_sdk_client::InitSdk(ZOOM_SDK_NAMESPACE::InitParam initParam)
{
	return InitSDK(initParam);
}

ZOOM_SDK_NAMESPACE::SDKError zoom_sdk_client::UninitSdk()
{
	return ZOOM_SDK_NAMESPACE::CleanUPSDK();
}

ZOOM_SDK_NAMESPACE::SDKError zoom_sdk_client::SDKAuth(ZOOM_SDK_NAMESPACE::AuthParam authParam)
{
	return m_pAuthService->SDKAuth(authParam);
}

ZOOM_SDK_NAMESPACE::SDKError zoom_sdk_client::Login(ZOOM_SDK_NAMESPACE::LoginParam loginParam)
{
	return m_pAuthService->Login(loginParam);
}

ZOOM_SDK_NAMESPACE::SDKError zoom_sdk_client::Start(ZOOM_SDK_NAMESPACE::StartParam startParam)
{
	return m_pMeetingService->Start(startParam);
}


ZOOM_SDK_NAMESPACE::SDKError zoom_sdk_client::MuteAudio(unsigned int userId, bool allowUnmuteBySelf) {
	return m_pAudioCtrl->MuteAudio(userId, allowUnmuteBySelf);
}
ZOOM_SDK_NAMESPACE::SDKError zoom_sdk_client::UnMuteAudio(unsigned int userId) {
	return m_pAudioCtrl->UnMuteAudio(userId);
}

ZOOM_SDK_NAMESPACE::SDKError zoom_sdk_client::MuteVideo() {
	return m_pVideoCtrl->MuteVideo();
}
ZOOM_SDK_NAMESPACE::SDKError zoom_sdk_client::UnMuteVideo() {
	return m_pVideoCtrl->UnmuteVideo();
}

int zoom_sdk_client::GetMicList(DeviceInfoResult* mics) {

	ZOOM_SDK_NAMESPACE::IList<ZOOM_SDK_NAMESPACE::IMicInfo*>* micList = m_pSettingService->GetAudioSettings()->GetMicList();

	int max = micList->GetCount() > 10 ? 10 : micList->GetCount();

	for (size_t i = 0; i < max; i++)
	{
		ZOOM_SDK_NAMESPACE::IMicInfo* mic = micList->GetItem(i);

		(mics + i)->deviceId = mic->GetDeviceId();
		(mics + i)->deviceName = mic->GetDeviceName();
		(mics + i)->isSelected = mic->IsSelectedDevice();
	}

	return max;
}

int zoom_sdk_client::GetSpeakerList(DeviceInfoResult* speakers) {
	ZOOM_SDK_NAMESPACE::IList<ZOOM_SDK_NAMESPACE::ISpeakerInfo*>* speakerList = m_pSettingService->GetAudioSettings()->GetSpeakerList();

	int max = speakerList->GetCount() > 10 ? 10 : speakerList->GetCount();

	for (size_t i = 0; i < max; i++)
	{
		ZOOM_SDK_NAMESPACE::ISpeakerInfo* speaker = speakerList->GetItem(i);

		(speakers + i)->deviceId = speaker->GetDeviceId();
		(speakers + i)->deviceName = speaker->GetDeviceName();
		(speakers + i)->isSelected = speaker->IsSelectedDevice();
	}
	return max;
}


int zoom_sdk_client::GetCameraList(DeviceInfoResult* cameras) {
	ZOOM_SDK_NAMESPACE::IList<ZOOM_SDK_NAMESPACE::ICameraInfo*>* cameraList = m_pSettingService->GetVideoSettings()->GetCameraList();

	int max = cameraList->GetCount() > 10 ? 10 : cameraList->GetCount();

	for (size_t i = 0; i < cameraList->GetCount(); i++)
	{
		ZOOM_SDK_NAMESPACE::ICameraInfo* camera = cameraList->GetItem(i);

		(cameras + i)->deviceId = camera->GetDeviceId();
		(cameras + i)->deviceName = camera->GetDeviceName();
		(cameras + i)->isSelected = camera->IsSelectedDevice();
	}
	return max;
}

ZOOM_SDK_NAMESPACE::SDKError zoom_sdk_client::SelectMic(const wchar_t* deviceId, const wchar_t* deviceName) {
	return m_pSettingService->GetAudioSettings()->SelectMic(deviceId, deviceName);
}
ZOOM_SDK_NAMESPACE::SDKError zoom_sdk_client::SelectSpeaker(const wchar_t* deviceId, const wchar_t* deviceName) {
	return m_pSettingService->GetAudioSettings()->SelectSpeaker(deviceId, deviceName);
}
ZOOM_SDK_NAMESPACE::SDKError zoom_sdk_client::SelectCamera(const wchar_t* deviceId) {
	return m_pSettingService->GetVideoSettings()->SelectCamera(deviceId);
}

ZOOM_SDK_NAMESPACE::SDKError zoom_sdk_client::StartRecording(unsigned long startTimestamp, wchar_t* recordPath) {
	time_t startTime = startTimestamp;
	return m_pRecordingCtrl->StartRecording(startTime, recordPath);
}
ZOOM_SDK_NAMESPACE::SDKError zoom_sdk_client::StopRecording(unsigned long stopTimestamp) {
	time_t stopTime = stopTimestamp;
	return m_pRecordingCtrl->StopRecording(stopTime);
}

ZOOM_SDK_NAMESPACE::SDKError zoom_sdk_client::GetMeetingUIWnd(HWND* firstViewHandle, HWND* secondViewHandle) {
	HWND first, second;
	ZOOM_SDK_NAMESPACE::SDKError err = m_pUICtrl->GetMeetingUIWnd(first, second);

	*firstViewHandle = first;
	*secondViewHandle = second;

	return err;
}

ZOOM_SDK_NAMESPACE::SDKError zoom_sdk_client::MonitorWndMessage(unsigned int msgId, bool add)
{
	return m_pUIHooker->MonitorWndMessage(msgId, add);
}
ZOOM_SDK_NAMESPACE::SDKError zoom_sdk_client::MonitorWnd(const wchar_t* className, bool add)
{
	return m_pUIHooker->MonitorWnd(className, add);
}
ZOOM_SDK_NAMESPACE::SDKError zoom_sdk_client::Start()
{
	return m_pUIHooker->Start();
}
ZOOM_SDK_NAMESPACE::SDKError zoom_sdk_client::Stop()
{
	return m_pUIHooker->Stop();
}

void zoom_sdk_client::CustomizeUI() {
	m_pConfigurationService->SetBottomFloatToolbarWndVisibility(false);
	m_pConfigurationService->EnableLButtonDBClick4SwitchFullScreenMode(false);
	m_pConfigurationService->EnableEnterAndExitFullScreenButtonOnMeetingUI(false);
	m_pConfigurationService->HideMeetingInfoFromMeetingUITitle(true);

	m_pConfigurationService->SetSharingToolbarVisibility(false);
	m_pUICtrl->ShowSharingToolbar(false);
}


//IMeetingServiceEvent
void zoom_sdk_client::onMeetingStatusChanged(ZOOM_SDK_NAMESPACE::MeetingStatus status, int iResult)
{
	MeetingStatusResult meetingStatusResult;
	meetingStatusResult.status = status;
	meetingStatusResult.result = iResult;

	InvokeCallback(MeetingStatusChanged, &meetingStatusResult);
}
void zoom_sdk_client::onMeetingStatisticsWarningNotification(ZOOM_SDK_NAMESPACE::StatisticsWarningType type)
{
	WarningResult warningResult;
	warningResult.type = type;

	InvokeCallback(MeetingStatisticsWarningNotification, &warningResult);
}

void zoom_sdk_client::onMeetingSecureKeyNotification(const char* key, int len, ZOOM_SDK_NAMESPACE::IMeetingExternalSecureKeyHandler* pHandler)
{

}

//IAuthServiceEvent
void zoom_sdk_client::onAuthenticationReturn(ZOOM_SDK_NAMESPACE::AuthResult ret)
{
	AuthenticationResult authenticationResult;
	authenticationResult.result = ret;

	InvokeCallback(AuthenticationReturn, &authenticationResult);
}

void zoom_sdk_client::onLoginRet(ZOOM_SDK_NAMESPACE::LOGINSTATUS ret, ZOOM_SDK_NAMESPACE::IAccountInfo* pAccountInfo)
{
	LoginResult loginResult;
	loginResult.status = ret;
	loginResult.displayName = pAccountInfo->GetDisplayName();
	loginResult.loginType = pAccountInfo->GetLoginType();

	InvokeCallback(LoginRet, &loginResult);
}

void zoom_sdk_client::onLogout()
{
	InvokeCallback(Logout, NULL);
}

//IMeetingConfigurationEvent
void zoom_sdk_client::onInputMeetingPasswordAndScreenNameNotification(ZOOM_SDK_NAMESPACE::IMeetingPasswordAndScreenNameHandler* pHandler){}
void zoom_sdk_client::onAirPlayInstructionWndNotification(bool bShow, const wchar_t* airhostName){}


//IMeetingAudioCtrlEvent
void zoom_sdk_client::onUserAudioStatusChange(ZOOM_SDK_NAMESPACE::IList<ZOOM_SDK_NAMESPACE::IUserAudioStatus* >* lstAudioStatusChange, const wchar_t* strAudioStatusList){}
void zoom_sdk_client::onUserActiveAudioChange(ZOOM_SDK_NAMESPACE::IList<unsigned int >* plstActiveAudio){}

//IMeetingVideoCtrlEvent
void zoom_sdk_client::onUserVideoStatusChange(unsigned int userId, ZOOM_SDK_NAMESPACE::VideoStatus status)
{
	VideoStatusResult videoStatusResult;
	videoStatusResult.userId = userId;
	videoStatusResult.status = status;

	InvokeCallback(UserVideoStatusChange, &videoStatusResult);
}

void zoom_sdk_client::onSpotlightVideoChangeNotification(bool bSpotlight, unsigned int userid){}

//IMeetingParticipantsCtrlEvent
void zoom_sdk_client::onUserJoin(ZOOM_SDK_NAMESPACE::IList<unsigned int >* lstUserID, const wchar_t* strUserList){}
void zoom_sdk_client::onUserLeft(ZOOM_SDK_NAMESPACE::IList<unsigned int >* lstUserID, const wchar_t* strUserList){}
void zoom_sdk_client::onHostChangeNotification(unsigned int userId){}
void zoom_sdk_client::onLowOrRaiseHandStatusChanged(bool bLow, unsigned int userid){}
void zoom_sdk_client::onUserNameChanged(unsigned int userId, const wchar_t* userName)
{

}

//IMeetingRecordingCtrlEvent
void zoom_sdk_client::onRecording2MP4Done(bool bsuccess, int iResult, const wchar_t* szPath)
{
	Recording2MP4DoneResult result;
	result.isSucceeded = bsuccess;
	result.result = iResult;
	result.recordPath = szPath;

	InvokeCallback(Recording2MP4Done, &result);
}
void zoom_sdk_client::onRecording2MP4Processing(int iPercentage)
{
	Recording2MP4PercentageResult result;
	result.percentage = iPercentage;

	InvokeCallback(Recording2MP4Processing, &result);
}
void zoom_sdk_client::onRecordingStatus(ZOOM_SDK_NAMESPACE::RecordingStatus status)
{
	RecordingStatusResult result;
	result.status = status;

	InvokeCallback(RecordingStatus, &result);
}
void zoom_sdk_client::onRecordPriviligeChanged(bool bCanRec)
{
	RecordPriviligeChangedResult result;
	result.canRecord = bCanRec;

	InvokeCallback(RecordPriviligeChanged, &result);
}

//IMeetingShareCtrlEvent
void zoom_sdk_client::onSharingStatus(ZOOM_SDK_NAMESPACE::SharingStatus status, unsigned int userId){}
void zoom_sdk_client::onLockShareStatus(bool bLocked){}
void zoom_sdk_client::onShareContentNotification(ZOOM_SDK_NAMESPACE::ShareInfo& shareInfo){}

//IMeetingUIControllerEvent
void zoom_sdk_client::onInviteBtnClicked(){}
void zoom_sdk_client::onStartShareBtnClicked(){}
void zoom_sdk_client::onEndMeetingBtnClicked(){}

//IUIHookerEvent
void zoom_sdk_client::onUIActionNotify(ZOOM_SDK_NAMESPACE::UIHOOKHWNDTYPE type, MSG msg)
{
	UINotifyResult uiNotifyResult;
	uiNotifyResult.type = type;
	uiNotifyResult.handle = msg.hwnd;
	uiNotifyResult.messageId = msg.message;

	InvokeCallback(UIActionNotify, &uiNotifyResult);
}


//INetworkConnectionHandler
void zoom_sdk_client::onProxyDetectComplete(){}
void zoom_sdk_client::onProxySettingNotification(ZOOM_SDK_NAMESPACE::IProxySettingHandler* handler){}
void zoom_sdk_client::onSSLCertVerifyNotification(ZOOM_SDK_NAMESPACE::ISSLCertVerificationHandler* handler){}
