using Classroom.Events;
using Classroom.Services;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;

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

                //MessageBox.Show($"username：{UserName}， password：{Pwd}\r\n autologin：{AutoLogin}，remember pwd：{RememberPwd}\r\n err：{Err}");
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
            set { _logging = value; }
        }

        public ICommand LoginCommand { get; set; }


        private bool IsInputFieldsValid()
        {
            Err = string.Empty;
            if (string.IsNullOrEmpty(UserName))
            {
                EventAggregatorManager.Instance.EventAggregator.GetEvent<UIGotFocusEvent>().Publish("UserName");
                Err = "请填写用户名！";
                return false;
            }

            if (string.IsNullOrEmpty(Pwd))
            {
                EventAggregatorManager.Instance.EventAggregator.GetEvent<UIGotFocusEvent>().Publish("Pwd");
                Err = "请填写密码！";
                return false;
            }

            return true;
        }

        
    }


    public class LoginViewModel
    {
        public LoginViewModel()
        {
            LoginModel = new LoginModel();
        }
        public LoginModel LoginModel { get; set; }
    }
}
