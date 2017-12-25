using Classroom.Events;
using Classroom.sdk_wrap;
using Classroom.Services;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using ZOOM_SDK_DOTNET_WRAP;

namespace Classroom.ViewModels
{
    public class LoginModel : BindableBase
    {
        public LoginModel()
        {
            LoginCommand = new DelegateCommand(() =>
            {
                if (!IsInputFieldsValid())
                {
                    return;
                }

                Logging = true;

                RegisterCallbacks();

                if (!SDKAuth())
                {
                    Logging = false;
                    return;
                }

                Logging = false;
            });
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _pwd;

        public string Pwd
        {
            get { return _pwd; }
            set { SetProperty(ref _pwd, value); }
        }

        private bool _autoLogin;

        public bool AutoLogin
        {
            get { return _autoLogin; }
            set { SetProperty(ref _autoLogin, value); }
        }

        private bool _rememberPwd;

        public bool RememberPwd
        {
            get { return _rememberPwd; }
            set { SetProperty(ref _rememberPwd, value); }
        }

        private string _err;
        public string Err
        {
            get { return _err; }
            set { SetProperty(ref _err, value); }
        }

        private bool _logging;
        public bool Logging
        {
            get { return _logging; }
            set { SetProperty(ref _logging, value); }
        }

        public ICommand LoginCommand { get; set; }

        private bool IsInputFieldsValid()
        {
            Err = string.Empty;
            if (string.IsNullOrEmpty(UserName))
            {
                EventAggregatorManager.Instance.EventAggregator.GetEvent<UIGotFocusEvent>().Publish(new EventArgument()
                {
                    Source = Source.LoginViewModel,
                    Value = Value.UserName
                });
                Err = "请填写用户名！";
                return false;
            }

            if (string.IsNullOrEmpty(Pwd))
            {
                EventAggregatorManager.Instance.EventAggregator.GetEvent<UIGotFocusEvent>().Publish(new EventArgument()
                {
                    Source = Source.LoginViewModel,
                    Value = Value.Password,
                });
                Err = "请填写密码！";
                return false;
            }

            return true;
        }

        private void RegisterCallbacks()
        {
            IAuthServiceDotNetWrap authServiceDotNetWrap = CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap();

            authServiceDotNetWrap.Add_CB_onAuthenticationReturn((authResult)=>
            {
                if (authResult == AuthResult.AUTHRET_SUCCESS)
                {
                    EventAggregatorManager.Instance.EventAggregator.GetEvent<WindowCloseEvent>().Publish(new EventArgument()
                    {
                        Source = Source.LoginViewModel,
                    });
                }
                else
                {
                    Err = authResult.ToString();
                }
            });

            authServiceDotNetWrap.Add_CB_onLoginRet((loginStatus,accountInfo)=>
            {
                
            });
            authServiceDotNetWrap.Add_CB_onLogout(()=>
            {

            });
        }

        private bool SDKAuth()
        {
            AuthParam authParam = new AuthParam()
            {
                appKey = UserName,
                appSecret = Pwd
            };

            SDKError error = SdkWrap.Instacne.SDKAuth(authParam);

            if (error != SDKError.SDKERR_SUCCESS)
            {
                Err = error.ToString();
                return false;
            }

            return true;
        }
    }

    
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            LoginModel = new LoginModel()
            {
                UserName = "miUWGGznzyA9NvGE0mWaHxqH5K62jbQGf9Vi",
                Pwd = "2HgRys821FEOeIij7GPRoL5H5xrgp5Ui6c1d"
            };
        }
        public LoginModel LoginModel { get; set; }
    }
}
