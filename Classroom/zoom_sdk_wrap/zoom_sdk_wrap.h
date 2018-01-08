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
	ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError GetMeetingUIWnd(HWND* firstViewHandle, HWND* secondViewHandle);

}