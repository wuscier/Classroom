#pragma once
#include "zoom_sdk_wrap_def.h"

#ifdef ZOOMSDKWRAP_EXPORTS

#define ZOOMWRAP_API __declspec(dllexport)

#else
#define ZOOMWRAP_API __declspec(dllimport)
#endif // ZOOMSDKWRAP_EXPORTS


extern "C" {
	ZOOMWRAP_API bool InitSdkWrap(func_callback callback);
	ZOOMWRAP_API bool UninitSdkWrap();

	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError InitSdk(ZOOM_SDK_NAMESPACE::InitParam initParam);
	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError UninitSdk();

	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError SDKAuth(ZOOM_SDK_NAMESPACE::AuthParam authParam);
	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError Login(ZOOM_SDK_NAMESPACE::LoginParam loginParam);
	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError Start(ZOOM_SDK_NAMESPACE::StartParam startParam);
	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError Leave(ZOOM_SDK_NAMESPACE::LeaveMeetingCmd leaveCmd);
	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError GetMeetingUIWnd(HWND* firstViewHandle, HWND* secondViewHandle);

	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError MuteAudio(unsigned int userId, bool allowUnmuteBySelf);
	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError UnMuteAudio(unsigned int userId);

	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError MuteVideo();
	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError UnMuteVideo();

	ZOOMWRAP_API int GetMicList(DeviceInfoResult* mics);
	ZOOMWRAP_API int GetSpeakerList(DeviceInfoResult* speakers);
	ZOOMWRAP_API int GetCameraList(DeviceInfoResult* cameras);

	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError SelectMic(const wchar_t* deviceId, const wchar_t* deviceName);
	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError SelectSpeaker(const wchar_t* deviceId, const wchar_t* deviceName);
	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError SelectCamera(const wchar_t* deviceId);

	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError StartRecording(unsigned long startTimestamp, wchar_t* recordPath);
	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError StopRecording(unsigned long stopTimestamp);

	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError MonitorWndMessage(unsigned int msgId, bool add);
	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError MonitorWnd(const wchar_t* className, bool add);
	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError StartMonitor();
	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError StopMonitor();

	ZOOMWRAP_API void CustomizeUI();


}