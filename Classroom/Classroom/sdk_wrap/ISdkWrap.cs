using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZOOM_SDK_DOTNET_WRAP;

namespace Classroom.sdk_wrap
{
    public interface ISdkWrap
    {
        SDKError Initialize();
        SDKError CleanUp();

    }
}
