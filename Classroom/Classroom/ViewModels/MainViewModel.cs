using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classroom.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            IsMainCardSelected = true;
        }

        private bool _isMainCardSelected;

        public bool IsMainCardSelected
        {
            get { return _isMainCardSelected; }
            set { _isMainCardSelected = value; }
        }


        private bool _isHistoryCardSelected;

        public bool IsHistoryCardSelected
        {
            get { return _isHistoryCardSelected; }
            set { _isHistoryCardSelected = value; }
        }

        private void SubscribeEvents()
        {

        }

        private void UnsubscribeEvents()
        {

        }
    }
}
