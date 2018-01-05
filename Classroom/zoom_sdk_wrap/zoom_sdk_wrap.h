#pragma once
#include "zoom_sdk_wrap_def.h"

#ifdef ZOOMSDKWRAP_EXPORTS

#define ZOOMWRAP_API __declspec(dllexport)

#else
#define ZOOMWRAP_API __declspec(dllimport)
#endif // ZOOMSDKWRAP_EXPORTS


extern "C" {
	ZOOMWRAP_API bool InitSdkWrap(func_callback callback);
}