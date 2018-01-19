using Classroom.sdk_wrap;
using Classroom.Views;
using System.Windows;
using ZOOM_SDK_DOTNET_WRAP;

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
            SDKError err = SdkWrap.Instacne.Initialize();

            if (err != SDKError.SDKERR_SUCCESS)
            {
                MessageBox.Show("初始化服务失败！");
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            
            SDKError err = SdkWrap.Instacne.CleanUp();

            if (err != SDKError.SDKERR_SUCCESS)
            {
                MessageBox.Show("清理服务失败！");
            }
        }
    }
}
