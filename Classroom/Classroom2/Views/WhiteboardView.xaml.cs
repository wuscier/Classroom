﻿using Classroom.Events;
using Classroom.Services;
using Classroom.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Classroom.Views
{
    /// <summary>
    /// WhiteboardView.xaml 的交互逻辑
    /// </summary>
    public partial class WhiteboardView : Window
    {
        public WhiteboardView()
        {
            InitializeComponent();
            DataContext = new WhiteboardViewModel();
        }

        private void note_card_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (note_detail.Visibility == Visibility.Visible)
            {
                note_detail.Visibility = Visibility.Collapsed;
            }
            else
            {
                note_detail.Visibility = Visibility.Visible;
            }
        }

        private void thumbnail_card_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (thumbnail_detail.Visibility == Visibility.Visible)
            {
                thumbnail_detail.Visibility = Visibility.Collapsed;
            }
            else
            {
                thumbnail_detail.Visibility = Visibility.Visible;
            }
        }

        private void next_page_card_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<NextPageEvent>().Publish(new EventArgument()
            {
                Target = Target.WhiteboardViewModel,
            });
        }

        private void previous_page_card_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<PreviousPageEvent>().Publish(new EventArgument()
            {
                Target = Target.WhiteboardViewModel,
            });
        }

        protected override void OnClosed(EventArgs e)
        {
            WhiteboardViewModel whiteboardViewModel = DataContext as WhiteboardViewModel;

            whiteboardViewModel.UnsubscribeEvents();
        }

        private void pen_card_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<PenSelectedEvent>().Publish(new EventArgument()
            {
                Target = Target.WhiteboardViewModel,
            });

        }

        private void eraser_card_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            EventAggregatorManager.Instance.EventAggregator.GetEvent<EraserSelectedEvent>().Publish(new EventArgument()
            {
                Target = Target.WhiteboardViewModel,
            });

        }

        private void color_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Border border = sender as Border;
            color_selected.Background = border.Background;
        }

        private void thickness_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            StackPanel stackPanel = sender as StackPanel;
            TextBlock textBlock = stackPanel.Children[1] as TextBlock;
            thickness_number.Text = textBlock.Text;
        }
    }
}
