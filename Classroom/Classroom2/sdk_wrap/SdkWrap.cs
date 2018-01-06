using ZOOM_SDK_DOTNET_WRAP;

namespace Classroom.sdk_wrap
{
    public class SdkWrap : ISdkWrap
    {
        private SdkWrap()
        {

        }

        public static readonly SdkWrap Instacne = new SdkWrap();

        public uint UserId { get; set; }

        public SDKError Initialize()
        {
            InitParam param = new InitParam();

            param.brand_name = "云教室";

            param.web_domain = "https://zoom.us";
            param.language_id = SDK_LANGUAGE_ID.LANGUAGE_Chinese_Simplified;

            SDKError err = CZoomSDKeDotNetWrap.Instance.Initialize(param);

            return err;
        }

        public SDKError CleanUp()
        {
            SDKError err = CZoomSDKeDotNetWrap.Instance.CleanUp();

            return err;
        }

        public SDKError SDKAuth(AuthParam authParam)
        {
            SDKError err = CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().SDKAuth(authParam);
            return err;
        }

        public SDKError Start(StartParam startParam)
        {
            SDKError err = CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().Start(startParam);

            return err;
        }

        public SDKError Login(LoginParam loginParam)
        {
            SDKError err = CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().Login(loginParam);

            return err;
        }
    }
}
