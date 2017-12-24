using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZOOM_SDK_DOTNET_WRAP;

namespace Classroom.sdk_wrap
{
    public class SdkWrap : ISdkWrap
    {
        private SdkWrap()
        {

        }

        public static readonly SdkWrap Instacne = new SdkWrap();

        public SDKError Initialize()
        {
            InitParam param = new InitParam();
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
    }
}
