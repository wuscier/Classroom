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


        private SubscriptionToken _subscriptionToken;
        private void SubscribeEvents()
        {
            _subscriptionToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<UIGotFocusEvent>().Subscribe((element_name) =>
             {
                 switch (element_name)
                 {
                     case "UserName":
                         username.Focus();
                         break;
                     case "Pwd":
                         password.Focus();
                         break;
                 }
             });
        }

        private void UnsubscribeEvents()
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<UIGotFocusEvent>().Unsubscribe(_subscriptionToken);
        }

        protected override void OnClosed(EventArgs e)
        {
            UnsubscribeEvents();
        }
    }
}
