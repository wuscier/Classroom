// zoom_sdk_wrap.cpp: 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "zoom_sdk_wrap.h"
#include "zoom_sdk_client.h"


bool InitMeetingService()
{
	return zoom_sdk_client::Instance()->InitMeetingService();
}

bool InitAuthService()
{
	return zoom_sdk_client::Instance()->InitAuthService();
}

ZOOM_SDK_NAMESPACE::SDKError InitSdk(ZOOM_SDK_NAMESPACE::InitParam initParam, func_callback callback)
{
	return zoom_sdk_client::Instance()->InitSdk(initParam,callback);
}

 ZOOM_SDK_NAMESPACE::SDKError UninitSdk()
{
	return zoom_sdk_client::Instance()->UninitSdk();
}

 ZOOM_SDK_NAMESPACE::SDKError SDKAuth(ZOOM_SDK_NAMESPACE::AuthParam authParam)
{
	return zoom_sdk_client::Instance()->SDKAuth(authParam);
}

 ZOOM_SDK_NAMESPACE::SDKError Login(ZOOM_SDK_NAMESPACE::LoginParam loginParam)
{
	return zoom_sdk_client::Instance()->Login(loginParam);
}

 ZOOM_SDK_NAMESPACE::SDKError Start(ZOOM_SDK_NAMESPACE::StartParam startParam)
{
	return zoom_sdk_client::Instance()->Start(startParam);
}

 ZOOM_SDK_NAMESPACE::SDKError Leave(ZOOM_SDK_NAMESPACE::LeaveMeetingCmd leaveCmd) {
	 return zoom_sdk_client::Instance()->Leave(leaveCmd);
 }

 ZOOM_SDK_NAMESPACE::SDKError GetMeetingUIWnd(HWND* firstViewHandle, HWND* secondViewHandle){
	return zoom_sdk_client::Instance()->GetMeetingUIWnd(firstViewHandle, secondViewHandle);
}



ZOOM_SDK_NAMESPACE::SDKError MuteAudio(unsigned int userId, bool allowUnmuteBySelf) {
	return zoom_sdk_client::Instance()->MuteAudio(userId, allowUnmuteBySelf);
}
ZOOM_SDK_NAMESPACE::SDKError UnMuteAudio(unsigned int userId) {
	return zoom_sdk_client::Instance()->UnMuteAudio(userId);
}

ZOOM_SDK_NAMESPACE::SDKError MuteVideo() {
	return zoom_sdk_client::Instance()->MuteVideo();
}
ZOOM_SDK_NAMESPACE::SDKError UnMuteVideo() {
	return zoom_sdk_client::Instance()->UnMuteVideo();
}

int GetMicList(DeviceInfoResult* mics) {

	return zoom_sdk_client::Instance()->GetMicList(mics);
}
int GetSpeakerList(DeviceInfoResult* speakers) {
	return zoom_sdk_client::Instance()->GetSpeakerList(speakers);
}


int GetCameraList(DeviceInfoResult* cameras) {
	return zoom_sdk_client::Instance()->GetCameraList(cameras);
}

ZOOM_SDK_NAMESPACE::SDKError SelectMic(const wchar_t* deviceId, const wchar_t* deviceName) {
	return zoom_sdk_client::Instance()->SelectMic(deviceId, deviceName);
}
ZOOM_SDK_NAMESPACE::SDKError SelectSpeaker(const wchar_t* deviceId, const wchar_t* deviceName) {
	return zoom_sdk_client::Instance()->SelectSpeaker(deviceId, deviceName);
}
ZOOM_SDK_NAMESPACE::SDKError SelectCamera(const wchar_t* deviceId) {
	return zoom_sdk_client::Instance()->SelectCamera(deviceId);
}

ZOOM_SDK_NAMESPACE::SDKError StartRecording(unsigned int startTime, wchar_t* recordPath) {
	return zoom_sdk_client::Instance()->StartRecording(startTime,recordPath);
}
ZOOM_SDK_NAMESPACE::SDKError StopRecording(unsigned int stopTimestamp) {
	return zoom_sdk_client::Instance()->StopRecording(stopTimestamp);
}

ZOOM_SDK_NAMESPACE::SDKError MonitorWndMessage(unsigned int msgId, bool add)
{
	return zoom_sdk_client::Instance()->MonitorWndMessage(msgId, add);
}
ZOOM_SDK_NAMESPACE::SDKError MonitorWnd(const wchar_t* className, bool add)
{
	return zoom_sdk_client::Instance()->MonitorWnd(className, add);
}
ZOOM_SDK_NAMESPACE::SDKError StartMonitor()
{
	return zoom_sdk_client::Instance()->Start();
}
ZOOM_SDK_NAMESPACE::SDKError StopMonitor()
{
	return zoom_sdk_client::Instance()->Stop();
}

void CustomizeUI() {
	zoom_sdk_client::Instance()->CustomizeUI();
}



