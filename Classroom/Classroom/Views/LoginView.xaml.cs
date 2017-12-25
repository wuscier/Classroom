using Classroom.Events;
using Classroom.Services;
using Classroom.ViewModels;
using Prism.Events;
using System;
using System.Windows;

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
                 switch (argument.Value)
                 {
                     case Value.UserName:
                         username.Focus();
                         break;
                     case Value.Password:
                         password.Focus();
                         break;
                 }
             }, ThreadOption.UIThread, true, filter => { return filter.Target == Target.LoginView; });

            _windowCloseToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<WindowCloseEvent>().Subscribe((argument) =>
            {
                App.MainView = new MainView();
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
    }
}
