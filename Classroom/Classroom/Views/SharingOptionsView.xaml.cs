using Classroom.Events;
using Classroom.Services;
using Classroom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Classroom.Views
{
    /// <summary>
    /// SharingOptionsView.xaml 的交互逻辑
    /// </summary>
    public partial class SharingOptionsView : Window
    {
        public SharingOptionsView()
        {
            InitializeComponent();
            DataContext = new SharingOptionsViewModel();
        }

        private void desktop_card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<CardSelectedEvent>().Publish(new EventArgument()
            {
                Argument = new Argument()
                {
                    Category = Category.DesktopCard,
                },
                Target = Target.SharingOptionsViewModel,
            });
        }

        private void doc_card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<CardSelectedEvent>().Publish(new EventArgument()
            {
                Argument = new Argument()
                {
                    Category = Category.DocCard,
                },
                Target = Target.SharingOptionsViewModel,
            });
        }

        protected override void OnClosed(EventArgs e)
        {
            SharingOptionsViewModel viewModel = DataContext as SharingOptionsViewModel;

            viewModel?.UnsubscribeEvents();
        }

        private void start_sharing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<StartSharingEvent>().Publish(new EventArgument()
            {
                Target = Target.SharingOptionsViewModel,
            });
        }
    }
}
