using ZOOM_SDK_DOTNET_WRAP;

namespace Classroom.sdk_wrap
{
    public interface ISdkWrap
    {
        SDKError Initialize();
        SDKError CleanUp();

    }
}
