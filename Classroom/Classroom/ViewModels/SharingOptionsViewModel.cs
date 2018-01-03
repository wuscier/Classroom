using Classroom.Events;
using Classroom.Helpers;
using Classroom.Services;
using Microsoft.Win32;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZOOM_SDK_DOTNET_WRAP;

namespace Classroom.ViewModels
{
    public class SharingOptionsViewModel : BindableBase
    {

        public SharingOptionsViewModel()
        {
            SubscribeEvents();

            IsDesktopSelected = false;
            IsDocSelected = false;
        }


        private bool _isDesktopSelected;

        public bool IsDesktopSelected
        {
            get { return _isDesktopSelected; }
            set { SetProperty(ref _isDesktopSelected, value); }
        }


        private bool _isDocSelected;

        public bool IsDocSelected
        {
            get { return _isDocSelected; }
            set { SetProperty(ref _isDocSelected, value); }
        }

        private SubscriptionToken _selectedToken;
        private SubscriptionToken _sharingToken;

        private void SubscribeEvents()
        {
            _selectedToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<CardSelectedEvent>().Subscribe((argument) =>
             {
                 switch (argument.Argument.Category)
                 {
                     case Category.DesktopCard:
                         IsDesktopSelected = true;
                         IsDocSelected = false;
                         break;
                     case Category.DocCard:
                         IsDocSelected = true;
                         IsDesktopSelected = false;

                         OpenFileDialog openFileDialog = new OpenFileDialog();
                         openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                         openFileDialog.Multiselect = false;
                         openFileDialog.Filter = "文档|*.doc;*.docx;*.ppt;*.pptx;*.xls;*.xlsx;*.pdf";
                         openFileDialog.ShowDialog();
                         string fileName = openFileDialog.FileName;

                         if (!string.IsNullOrEmpty(fileName))
                         {

                         }

                         break;
                 }

             }, ThreadOption.PublisherThread, true, filter => { return filter.Target == Target.SharingOptionsViewModel; });

            _sharingToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<StartSharingEvent>().Subscribe((argument) =>
            {
                if (IsDesktopSelected)
                {
                    IntPtr desktopHandle = Win32APIs.GetDesktopWindow();

                    SDKError error = CMeetingShareControllerDotNetWrap.Instance.StartAppShare(new HWNDDotNet() { value = (uint)desktopHandle.ToInt32() });
                }
                else if (IsDocSelected)
                {

                }
                else
                {
                    MessageBox.Show("请选择共享源！");
                }

            }, ThreadOption.PublisherThread, true, filter => { return filter.Target == Target.SharingOptionsViewModel; });
        }

        public void UnsubscribeEvents()
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<CardSelectedEvent>().Unsubscribe(_selectedToken);
            EventAggregatorManager.Instance.EventAggregator.GetEvent<StartSharingEvent>().Unsubscribe(_sharingToken);
        }
    }
}
