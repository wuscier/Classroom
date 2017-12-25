using System.Windows;
using System.ComponentModel; // CancelEventArgs
using ZOOM_SDK_DOTNET_WRAP;

namespace zoom_sdk_demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        start_join_meeting start_meeting_wnd = new start_join_meeting();
        public MainWindow()
        {
            InitializeComponent();
            button_auth_Click(null, null);
        }

        //callback
        public void onAuthenticationReturn(AuthResult ret)
        {
            if (ZOOM_SDK_DOTNET_WRAP.AuthResult.AUTHRET_SUCCESS == ret)
            {
                start_meeting_wnd.Show();
            }
            else//error handle.todo
            {
                Show();
            }
        }
        public void onLoginRet(LOGINSTATUS ret, IAccountInfo pAccountInfo)
        {
            //todo
        }
        public void onLogout()
        {
            //todo
        }
        private void button_auth_Click(object sender, RoutedEventArgs e)
        {
            //register callback
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().Add_CB_onAuthenticationReturn(onAuthenticationReturn);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().Add_CB_onLoginRet(onLoginRet);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().Add_CB_onLogout(onLogout);
            ZOOM_SDK_DOTNET_WRAP.AuthParam param = new ZOOM_SDK_DOTNET_WRAP.AuthParam();
            //

            param.appKey = textBox_appkey.Text;
            param.appSecret = textBox_appsecret.Text;
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().SDKAuth(param);
            Hide();
        }

        void Wnd_Closing(object sender, CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
