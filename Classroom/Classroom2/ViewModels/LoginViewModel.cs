using Classroom.Events;
using Classroom.sdk_wrap;
using Classroom.Services;
using Prism.Commands;
using Prism.Mvvm;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Classroom.ViewModels
{
    public class LoginModel : BindableBase
    {
        public LoginModel()
        {
            LoginCommand = new DelegateCommand(async () =>
            {
                Logging = true;

                await Task.Run(() =>
                 {
                     if (!IsInputFieldsValid())
                     {
                         return;
                     }

                     RegisterCallbacks();

                     SDKAuth();

                 }).ConfigureAwait(false);
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

            SdkWrap.Instance.AuthenticationReturnEvent += ((authResult) =>
             {
                 if (authResult.Result != AuthResult.AUTHRET_SUCCESS)
                 {
                     Logging = false;
                     Err = authResult.ToString();
                     return;
                 }

                 Login();
             });

            SdkWrap.Instance.LoginRetEvent += ((loginResult) =>
              {

                  switch (loginResult.Status)
                  {
                      case LoginStatus.LOGIN_IDLE:
                          break;
                      case LoginStatus.LOGIN_PROCESSING:
                          break;
                      case LoginStatus.LOGIN_SUCCESS:
                          Logging = false;
                          EventAggregatorManager.Instance.EventAggregator.GetEvent<WindowCloseEvent>().Publish(new EventArgument()
                          {
                              Target = Target.LoginView,
                          });

                          break;
                      case LoginStatus.LOGIN_FAILED:
                          Logging = false;
                          Err = "登录失败！";
                          break;
                      default:
                          break;
                  }
              });

            SdkWrap.Instance.LogoutEvent += (() =>
              {

              });
        }

        private void Login()
        {
            LoginParam loginParam = new LoginParam()
            {
                LoginType = LoginType.LoginType_Email,
                EmailLogin = new LoginParam4Email()
                {
                    RememberMe = RememberPwd,
                    Password = Pwd,
                    UserName = UserName,
                }
            };

            SDKError error = SdkInterop.Login(loginParam);

            if (error != SDKError.SDKERR_SUCCESS)
            {
                Logging = false;
                Err = error.ToString();
            }
        }

        private void SDKAuth()
        {
            if (!SdkInterop.InitAuthService())
            {
                MessageBox.Show("初始化auth服务失败！");
                return;
            }

            SDKError err = SdkInterop.SDKAuth(new AuthParam()
            {
                AppKey = "miUWGGznzyA9NvGE0mWaHxqH5K62jbQGf9Vi",
                AppSecret = "ktwJENTTfWGOlBOyvCOc81x5Ax4DFCU2lhCO",
            });

            if (err != SDKError.SDKERR_SUCCESS)
            {
                Logging = false;
                Err = err.ToString();
            }
        }
    }

    
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            LoginModel = new LoginModel()
            {
                UserName = "wuxu190718@outlook.com",
                Pwd = "abc123.cn_"
            };
        }
        public LoginModel LoginModel { get; set; }
    }
}
