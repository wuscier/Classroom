using Classroom.Events;
using Classroom.Services;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;

namespace Classroom.ViewModels
{
    public class Thumbnail : BindableBase
    {
        public string ThumbnailUri { get; set; }
        public int PageNum { get; set; }
    }


    public class WhiteboardViewModel : BindableBase
    {
        private SubscriptionToken _nextToken;
        private SubscriptionToken _previousToken;

        public WhiteboardViewModel()
        {
            SubscribeEvents();

            Thumbnails = new ObservableCollection<Thumbnail>();
            PageNums = new ObservableCollection<int>();

            InitThumbnails();

            int pageCount = Thumbnails.Count;
            InitPageNums(pageCount);
            if (pageCount > 0)
            {
                CurrentThumbnail = Thumbnails[0];
                CurrentPageNum = CurrentThumbnail.PageNum;
            }
        }


        private void SubscribeEvents()
        {
            _previousToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<PreviousPageEvent>().Subscribe((argument) =>
             {
                 if (CurrentPageNum - 1 > 0)
                 {
                     CurrentPageNum -= 1;
                 }
             }, ThreadOption.PublisherThread, true, filter => { return filter.Target == Target.WhiteboardViewModel; });

            _nextToken = EventAggregatorManager.Instance.EventAggregator.GetEvent<NextPageEvent>().Subscribe((argument) =>
             {
                 if (CurrentPageNum + 1 <= Thumbnails.Count)
                 {
                     CurrentPageNum += 1;
                 }
             }, ThreadOption.PublisherThread, true, filter => { return filter.Target == Target.WhiteboardViewModel; });
        }

        public void UnsubscribeEvents()
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<NextPageEvent>().Unsubscribe(_nextToken);
            EventAggregatorManager.Instance.EventAggregator.GetEvent<PreviousPageEvent>().Unsubscribe(_previousToken);
        }

        private void InitThumbnails()
        {
            Thumbnails.Add(new Thumbnail()
            {
                PageNum = 1,
                ThumbnailUri = "../Images/1.png",
            });
            Thumbnails.Add(new Thumbnail()
            {
                PageNum = 2,
                ThumbnailUri = "Images/2.png",
            });
            Thumbnails.Add(new Thumbnail()
            {
                PageNum = 3,
                ThumbnailUri = "Images/3.png",
            });
            Thumbnails.Add(new Thumbnail()
            {
                PageNum = 4,
                ThumbnailUri = "Images/4.png",
            });
            Thumbnails.Add(new Thumbnail()
            {
                PageNum = 5,
                ThumbnailUri = "Images/5.png",
            });
            Thumbnails.Add(new Thumbnail()
            {
                PageNum = 6,
                ThumbnailUri = "Images/6.png",
            });
            Thumbnails.Add(new Thumbnail()
            {
                PageNum = 7,
                ThumbnailUri = "Images/7.png",
            });
            Thumbnails.Add(new Thumbnail()
            {
                PageNum = 8,
                ThumbnailUri = "Images/8.png",
            });
            Thumbnails.Add(new Thumbnail()
            {
                PageNum = 9,
                ThumbnailUri = "Images/9.png",
            });
            Thumbnails.Add(new Thumbnail()
            {
                PageNum = 10,
                ThumbnailUri = "Images/10.png",
            });
            Thumbnails.Add(new Thumbnail()
            {
                PageNum = 11,
                ThumbnailUri = "Images/11.png",
            });
            Thumbnails.Add(new Thumbnail()
            {
                PageNum = 12,
                ThumbnailUri = "Images/12.png",
            });
            Thumbnails.Add(new Thumbnail()
            {
                PageNum = 13,
                ThumbnailUri = "Images/13.png",
            });
            Thumbnails.Add(new Thumbnail()
            {
                PageNum = 14,
                ThumbnailUri = "Images/14.png",
            });
            Thumbnails.Add(new Thumbnail()
            {
                PageNum = 15,
                ThumbnailUri = "Images/15.png",
            });
            Thumbnails.Add(new Thumbnail()
            {
                PageNum = 16,
                ThumbnailUri = "Images/16.png",
            });
            Thumbnails.Add(new Thumbnail()
            {
                PageNum = 17,
                ThumbnailUri = "Images/17.png",
            });
        }

        private void InitPageNums(int totalPages)
        {
            PageNums.Clear();
            for (int i = 1; i <= totalPages; i++)
            {
                PageNums.Add(i);
            }
        }

        public ObservableCollection<Thumbnail> Thumbnails { get; set; }

        public ObservableCollection<int> PageNums { get; set; }

        private Thumbnail _currentThumbnail;
        public Thumbnail CurrentThumbnail
        {
            get { return _currentThumbnail; }
            set
            {
                SetProperty(ref _currentThumbnail, value);
                if (CurrentPageNum != value.PageNum)
                {
                    CurrentPageNum = value.PageNum;
                }
            }
        }

        private int _currentPageNum;
        public int CurrentPageNum
        {
            get { return _currentPageNum; }
            set
            {
                SetProperty(ref _currentPageNum, value);
                if (CurrentThumbnail.PageNum != value)
                {
                    CurrentThumbnail = Thumbnails.FirstOrDefault(t => t.PageNum == value);
                }
            }
        }

    }
}
