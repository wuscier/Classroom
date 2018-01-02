using Classroom.Events;
using Classroom.sdk_wrap;
using Classroom.Services;
using Classroom.ViewModels;
using Prism.Events;
using System;
using System.Windows;
using ZOOM_SDK_DOTNET_WRAP;

namespace Classroom.Views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();

            SubscribeEvents();

            DataContext = new LoginViewModel();
        }


        private SubscriptionToken _uiGotFocusToken;
        private SubscriptionToken _windowCloseToken;

        private void SubscribeEvents()
        {
            _uiGotFocusToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<UIGotFocusEvent>().Subscribe((argument) =>
             {
                 switch (argument.Argument.Category)
                 {
                     case Category.UserName:
                         username.Focus();
                         break;
                     case Category.Password:
                         password.Focus();
                         break;
                 }
             }, ThreadOption.UIThread, true, filter => { return filter.Target == Target.LoginView; });

            _windowCloseToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<WindowCloseEvent>().Subscribe((argument) =>
            {
                if (App.MainView == null)
                {
                    App.MainView = new MainView();
                }

                App.MainView.Show();

                Close();
            }, ThreadOption.UIThread, true, filter => { return filter.Target == Target.LoginView; });
        }

        private void UnsubscribeEvents()
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<UIGotFocusEvent>().Unsubscribe(_uiGotFocusToken);
            EventAggregatorManager.Instance.EventAggregator.GetEvent<WindowCloseEvent>().Unsubscribe(_windowCloseToken);
        }

        protected override void OnClosed(EventArgs e)
        {
            UnsubscribeEvents();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SDKError err = SdkWrap.Instacne.SDKAuth(new AuthParam()
            {
                appKey = "miUWGGznzyA9NvGE0mWaHxqH5K62jbQGf9Vi",
                appSecret = "ktwJENTTfWGOlBOyvCOc81x5Ax4DFCU2lhCO",
            });

            if (err != SDKError.SDKERR_SUCCESS)
            {
                MessageBox.Show(err.ToString());
            }
        }
    }
}
