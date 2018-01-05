// zoom_sdk_wrap.cpp: 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "zoom_sdk_wrap.h"
#include "zoom_sdk_client.h"

bool Init(func_callback callback)
{
	return zoom_sdk_client::Instance()->Init(callback);
}
