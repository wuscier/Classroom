using Classroom.sdk_wrap;
using Classroom.Views;
using System.Windows;

namespace Classroom
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static MainView MainView;

        protected override void OnStartup(StartupEventArgs e)
        {
            SdkWrap.Instance.InitSdkWrap();
            SDKError err = SdkWrap.Instance.InitSdk();

            if (err != SDKError.SDKERR_SUCCESS)
            {
                MessageBox.Show("初始化服务失败！");
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            SdkWrap.Instance.UninitSdkWrap();
            SDKError err = SdkWrap.Instance.UninitSdk();

            if (err != SDKError.SDKERR_SUCCESS)
            {
                MessageBox.Show("清理服务失败！");
            }
        }
    }
}
