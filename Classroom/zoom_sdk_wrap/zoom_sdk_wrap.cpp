// zoom_sdk_wrap.cpp: 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "zoom_sdk_wrap.h"
#include "zoom_sdk_client.h"

bool InitSdkWrap(func_callback callback)
{
	return zoom_sdk_client::Instance()->InitSdkWrap(callback);
}

ZOOMWRAP_API bool UninitSdkWrap()
{
	return zoom_sdk_client::Instance()->UninitSdkWrap();
}

ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError InitSdk(ZOOM_SDK_NAMESPACE::InitParam initParam)
{
	return zoom_sdk_client::Instance()->InitSdk(initParam);
}

ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError UninitSdk()
{
	return zoom_sdk_client::Instance()->UninitSdk();
}

ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError SDKAuth(ZOOM_SDK_NAMESPACE::AuthParam authParam)
{
	return zoom_sdk_client::Instance()->SDKAuth(authParam);
}

ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError Login(ZOOM_SDK_NAMESPACE::LoginParam loginParam)
{
	return zoom_sdk_client::Instance()->Login(loginParam);
}

ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError Start(ZOOM_SDK_NAMESPACE::StartParam startParam)
{
	return zoom_sdk_client::Instance()->Start(startParam);
}

ZOOMWRAP_API ZOOM_SDK_NAMESPACE::SDKError GetMeetingUIWnd(HWND* firstViewHandle, HWND* secondViewHandle){
	return zoom_sdk_client::Instance()->GetMeetingUIWnd(firstViewHandle, secondViewHandle);
}

