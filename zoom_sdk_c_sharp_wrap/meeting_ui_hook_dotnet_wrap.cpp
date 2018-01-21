#include "stdafx.h"
#include "meeting_ui_hook_dotnet_wrap.h"
#include "zoom_sdk_dotnet_wrap_util.h"
#include "wrap/sdk_wrap.h"

namespace ZOOM_SDK_DOTNET_WRAP {

	class MeetingUIHookControllerEventHandler {
	public:
		static MeetingUIHookControllerEventHandler& GetInst() {
			static MeetingUIHookControllerEventHandler inst;
			return inst;
		}

		virtual void onUIActionNotify(ZOOM_SDK_NAMESPACE::UIHOOKHWNDTYPE type, MSG msg) {
			if (CMeetingUIHookDotNetWrap::Instance)
			{
				CMeetingUIHookDotNetWrap::Instance->procUIActionNotify((UIHOOKHWNDTYPE)type, msg.message);
			}
		}

	private:
		MeetingUIHookControllerEventHandler() {}
	};



	SDKError CMeetingUIHookDotNetWrap::MonitorWndMessage(unsigned int wndMsgId, bool bAdd)
	{
		SDKError err = (SDKError)ZOOM_SDK_NAMESPACE::CSDKExtWrap::GetInst().GetUIHookerWrap().MonitorWndMessage(wndMsgId, bAdd);

		return err;
	}

	SDKError CMeetingUIHookDotNetWrap::MonitorWnd(String ^ className, bool bAdd)
	{
		SDKError err = (SDKError)ZOOM_SDK_NAMESPACE::CSDKExtWrap::GetInst().GetUIHookerWrap().MonitorWnd(const_cast<wchar_t*>(PlatformString2WChar(className)), bAdd);

		return err;
	}

	SDKError CMeetingUIHookDotNetWrap::Start()
	{
		SDKError err = (SDKError)ZOOM_SDK_NAMESPACE::CSDKExtWrap::GetInst().GetUIHookerWrap().Start();

		return err;
	}

	SDKError CMeetingUIHookDotNetWrap::Stop()
	{
		SDKError err = (SDKError)ZOOM_SDK_NAMESPACE::CSDKExtWrap::GetInst().GetUIHookerWrap().Stop();

		return err;
	}

	void CMeetingUIHookDotNetWrap::BindEvent()
	{
		ZOOM_SDK_NAMESPACE::CSDKExtWrap::GetInst().GetUIHookerWrap().m_cbonUIActionNotify = std::bind(&MeetingUIHookControllerEventHandler::onUIActionNotify,
			&MeetingUIHookControllerEventHandler::GetInst(), std::placeholders::_1, std::placeholders::_2);
	}

	void CMeetingUIHookDotNetWrap::procUIActionNotify(UIHOOKHWNDTYPE type, int msg)
	{
		event_onUIActionNotify(type, msg);
	}

}