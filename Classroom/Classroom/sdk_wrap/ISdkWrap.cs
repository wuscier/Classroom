using ZOOM_SDK_DOTNET_WRAP;

namespace Classroom.sdk_wrap
{
    public interface ISdkWrap
    {
        uint UserId { get; set; }


        SDKError Initialize();
        SDKError CleanUp();

        SDKError Login(LoginParam loginParam);
        SDKError SDKAuth(AuthParam authParam);
        SDKError Start(StartParam startParam);
    }
}
