﻿using Classroom.Events;
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
            LoginCommand = new DelegateCommand(async () =>
            {
                await Task.Run(() =>
                 {
                     if (!IsInputFieldsValid())
                     {
                         return;
                     }

                     Logging = true;

                     RegisterCallbacks();

                     if (!Login())
                     {
                         Logging = false;
                         return;
                     }


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
            IAuthServiceDotNetWrap authServiceDotNetWrap = CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap();

            authServiceDotNetWrap.Add_CB_onAuthenticationReturn((authResult)=>
            {
                if (authResult == AuthResult.AUTHRET_SUCCESS)
                {
                    Logging = false;
                    EventAggregatorManager.Instance.EventAggregator.GetEvent<WindowCloseEvent>().Publish(new EventArgument()
                    {
                        Target = Target.LoginView,
                    });
                }
                else
                {
                    Err = authResult.ToString();
                }
            });

            authServiceDotNetWrap.Add_CB_onLoginRet((loginStatus,accountInfo)=>
            {
                if (loginStatus == LOGINSTATUS.LOGIN_SUCCESS)
                {
                    Logging = false;
                    EventAggregatorManager.Instance.EventAggregator.GetEvent<WindowCloseEvent>().Publish(new EventArgument()
                    {
                        Target = Target.LoginView,
                    });
                }
                else
                {
                    Err = loginStatus.ToString();
                }

            });
            authServiceDotNetWrap.Add_CB_onLogout(()=>
            {

            });
        }

        private bool Login()
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
                UserName = "wuxu190718@outlook.com",
                Pwd = "abc123.cn_"
            };
        }
        public LoginModel LoginModel { get; set; }
    }
}
