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
}


zoom_sdk_client::~zoom_sdk_client()
{

}

zoom_sdk_client * zoom_sdk_client::Instance()
{
	if (m_instance == NULL)
	{
		m_instance = new zoom_sdk_client();
	}

	return m_instance;
}

bool zoom_sdk_client::Init(func_callback cb)
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



//IMeetingServiceEvent
void zoom_sdk_client::onMeetingStatusChanged(ZOOM_SDK_NAMESPACE::MeetingStatus status, int iResult)
{

}
void zoom_sdk_client::onMeetingStatisticsWarningNotification(ZOOM_SDK_NAMESPACE::StatisticsWarningType type){}
void zoom_sdk_client::onMeetingSecureKeyNotification(const char* key, int len, ZOOM_SDK_NAMESPACE::IMeetingExternalSecureKeyHandler* pHandler){}

//IAuthServiceEvent
void zoom_sdk_client::onAuthenticationReturn(ZOOM_SDK_NAMESPACE::AuthResult ret){}
void zoom_sdk_client::onLoginRet(ZOOM_SDK_NAMESPACE::LOGINSTATUS ret, ZOOM_SDK_NAMESPACE::IAccountInfo* pAccountInfo){}
void zoom_sdk_client::onLogout(){}

//IMeetingConfigurationEvent
void zoom_sdk_client::onInputMeetingPasswordAndScreenNameNotification(ZOOM_SDK_NAMESPACE::IMeetingPasswordAndScreenNameHandler* pHandler){}
void zoom_sdk_client::onAirPlayInstructionWndNotification(bool bShow, const wchar_t* airhostName){}


//IMeetingAudioCtrlEvent
void zoom_sdk_client::onUserAudioStatusChange(ZOOM_SDK_NAMESPACE::IList<ZOOM_SDK_NAMESPACE::IUserAudioStatus* >* lstAudioStatusChange, const wchar_t* strAudioStatusList){}
void zoom_sdk_client::onUserActiveAudioChange(ZOOM_SDK_NAMESPACE::IList<unsigned int >* plstActiveAudio){}

//IMeetingVideoCtrlEvent
void zoom_sdk_client::onUserVideoStatusChange(unsigned int userId, ZOOM_SDK_NAMESPACE::VideoStatus status){}
void zoom_sdk_client::onSpotlightVideoChangeNotification(bool bSpotlight, unsigned int userid){}

//IMeetingParticipantsCtrlEvent
void zoom_sdk_client::onUserJoin(ZOOM_SDK_NAMESPACE::IList<unsigned int >* lstUserID, const wchar_t* strUserList){}
void zoom_sdk_client::onUserLeft(ZOOM_SDK_NAMESPACE::IList<unsigned int >* lstUserID, const wchar_t* strUserList){}
void zoom_sdk_client::onHostChangeNotification(unsigned int userId){}
void zoom_sdk_client::onLowOrRaiseHandStatusChanged(bool bLow, unsigned int userid){}
void zoom_sdk_client::onUserNameChanged(unsigned int userId, const wchar_t* userName){}

//IMeetingRecordingCtrlEvent
void zoom_sdk_client::onRecording2MP4Done(bool bsuccess, int iResult, const wchar_t* szPath){}
void zoom_sdk_client::onRecording2MP4Processing(int iPercentage){}
void zoom_sdk_client::onRecordingStatus(ZOOM_SDK_NAMESPACE::RecordingStatus status){}
void zoom_sdk_client::onRecordPriviligeChanged(bool bCanRec){}

//IMeetingShareCtrlEvent
void zoom_sdk_client::onSharingStatus(ZOOM_SDK_NAMESPACE::SharingStatus status, unsigned int userId){}
void zoom_sdk_client::onLockShareStatus(bool bLocked){}
void zoom_sdk_client::onShareContentNotification(ZOOM_SDK_NAMESPACE::ShareInfo& shareInfo){}

//IMeetingUIControllerEvent
void zoom_sdk_client::onInviteBtnClicked(){}
void zoom_sdk_client::onStartShareBtnClicked(){}
void zoom_sdk_client::onEndMeetingBtnClicked(){}

//IUIHookerEvent
void zoom_sdk_client::onUIActionNotify(ZOOM_SDK_NAMESPACE::UIHOOKHWNDTYPE type, MSG msg){}


//INetworkConnectionHandler
void zoom_sdk_client::onProxyDetectComplete(){}
void zoom_sdk_client::onProxySettingNotification(ZOOM_SDK_NAMESPACE::IProxySettingHandler* handler){}
void zoom_sdk_client::onSSLCertVerifyNotification(ZOOM_SDK_NAMESPACE::ISSLCertVerificationHandler* handler){}
