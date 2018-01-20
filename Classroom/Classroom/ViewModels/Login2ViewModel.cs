using Classroom.Events;
using Classroom.Models;
using Classroom.sdk_wrap;
using Classroom.Services;
using Prism.Commands;
using Prism.Mvvm;
using System.Threading.Tasks;
using System.Windows.Input;
using ZOOM_SDK_DOTNET_WRAP;


namespace Classroom.ViewModels
{
    public class LoginModel : BindableBase
    {
        public LoginModel()
        {
            LoginCommand = new DelegateCommand(/*async*/ () =>
            {
                Logging = true;

                //await Task.Run(() =>
                //{
                    //if (!IsInputFieldsValid())
                    //{
                    //    return;
                    //}

                    RegisterCallbacks();

                    SDKAuth();

                //}).ConfigureAwait(false);
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
                    Target = Target.LoginView,
                    Argument = new Argument() { Category = Category.UserName, }
                });
                Err = "请填写用户名！";
                return false;
            }

            if (string.IsNullOrEmpty(Pwd))
            {
                EventAggregatorManager.Instance.EventAggregator.GetEvent<UIGotFocusEvent>().Publish(new EventArgument()
                {
                    Target = Target.LoginView,
                    Argument = new Argument() { Category = Category.Password, }
                });
                Err = "请填写密码！";
                return false;
            }

            return true;
        }

        private void RegisterCallbacks()
        {
            IAuthServiceDotNetWrap authServiceDotNetWrap = CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap();

            authServiceDotNetWrap.Add_CB_onAuthenticationReturn((authResult) =>
            {
                if (authResult != AuthResult.AUTHRET_SUCCESS)
                {
                    Logging = false;
                    Err = authResult.ToString();
                    return;
                }

                App.UserModel = new UserModel()
                {
                    UserName = UserName,
                };

                EventAggregatorManager.Instance.EventAggregator.GetEvent<WindowCloseEvent>().Publish(new EventArgument()
                {
                    Target = Target.LoginView,
                });


                //Login();
            });

            //authServiceDotNetWrap.Add_CB_onLoginRet((loginStatus, accountInfo) =>
            //{

            //    switch (loginStatus)
            //    {
            //        case LOGINSTATUS.LOGIN_IDLE:
            //            break;
            //        case LOGINSTATUS.LOGIN_PROCESSING:
            //            break;
            //        case LOGINSTATUS.LOGIN_SUCCESS:
            //            Logging = false;
            //            EventAggregatorManager.Instance.EventAggregator.GetEvent<WindowCloseEvent>().Publish(new EventArgument()
            //            {
            //                Target = Target.LoginView,
            //            });

            //            break;
            //        case LOGINSTATUS.LOGIN_FAILED:
            //            Logging = false;
            //            Err = "登录失败！";
            //            break;
            //        default:
            //            break;
            //    }
            //});
            //authServiceDotNetWrap.Add_CB_onLogout(() =>
            //{

            //});
        }

        private void Login()
        {
            LoginParam loginParam = new LoginParam()
            {
                loginType = LoginType.LoginType_Email,
                emailLogin = new LoginParam4Email()
                {
                    bRememberMe = RememberPwd,
                    password = Pwd,
                    userName = UserName,
                }
            };

            SDKError error = SdkWrap.Instacne.Login(loginParam);

            if (error != SDKError.SDKERR_SUCCESS)
            {
                Logging = false;
                Err = error.ToString();
            }
        }

        private void SDKAuth()
        {
            SDKError err = SdkWrap.Instacne.SDKAuth(new AuthParam()
            {
                //appKey = "miUWGGznzyA9NvGE0mWaHxqH5K62jbQGf9Vi",
                //appSecret = "ktwJENTTfWGOlBOyvCOc81x5Ax4DFCU2lhCO",

                appKey = "p3TojubkBYyntp8m4rVevr0yYmH1HVW9yPiR",
                appSecret = "JLuhz1VkcWGVSUESJj19biBi7NZcbVWENRXe",
            });

            if (err != SDKError.SDKERR_SUCCESS)
            {
                Logging = false;
                Err = err.ToString();
            }
        }
    }


    public class Login2ViewModel
    {
        public Login2ViewModel()
        {
            LoginModel = new LoginModel()
            {
                UserName = "justlucky@126.com",
                //UserName = "wuxu190718@outlook.com",
                //Pwd = "abc123.cn_"
            };
        }
        public LoginModel LoginModel { get; set; }
    }
}
