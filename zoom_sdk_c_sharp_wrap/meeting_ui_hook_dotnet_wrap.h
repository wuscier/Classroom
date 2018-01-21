#pragma once
using namespace System;

#include "zoom_sdk_dotnet_wrap_def.h"

namespace ZOOM_SDK_DOTNET_WRAP {

	public enum class UIHOOKHWNDTYPE {
		UIHOOKWNDTYPE_USERDEFIEND,
		UIHOOKWNDTYPE_MAINWND,
		UIHOOKWNDTYPE_BOTTOMTOOLBAR,
	};

	public value class POINT {
		long x;
		long y;
	};

	public value class WNDMSG sealed {
	public:
		IntPtr hwnd;
		int message;
		IntPtr wParam;
		IntPtr lParam;
		int time;
		int pt_x;
		int pt_y;
	};

	public delegate void onUIActionNotify(UIHOOKHWNDTYPE type, int msg);


	public interface class IMeetingUIHookDotNetWrap {
	public:
		SDKError MonitorWndMessage(UInt32 wndMsgId, bool bAdd);
		SDKError MonitorWnd(String^ className, bool bAdd);
		SDKError Start();
		SDKError Stop();

		void Add_CB_onUIActionNotify(onUIActionNotify^ cb);
	};

	public ref class CMeetingUIHookDotNetWrap :public IMeetingUIHookDotNetWrap {
	public:
		static property CMeetingUIHookDotNetWrap^ Instance {
			CMeetingUIHookDotNetWrap^ get() { return m_Instance; }
		}

		virtual SDKError MonitorWndMessage(unsigned int wndMsgId, bool bAdd);
		virtual SDKError MonitorWnd(String^ className, bool bAdd);
		virtual SDKError Start();
		virtual SDKError Stop();

		virtual void Add_CB_onUIActionNotify(onUIActionNotify^ cb) {
			event_onUIActionNotify += cb;
		}

		void BindEvent();
		void procUIActionNotify(UIHOOKHWNDTYPE type, int msg);

	private:
		event onUIActionNotify^ event_onUIActionNotify;
		static CMeetingUIHookDotNetWrap^ m_Instance = gcnew CMeetingUIHookDotNetWrap;
	};
}

