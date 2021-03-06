﻿namespace Mlib
{
    using Caliburn.Micro;
    using Mlib.Data.Models;
    using Mlib.Extensions;
    using Mlib.Infrastructure;
    using Mlib.UI.Additional;
    using Mlib.UI.Interfaces;
    using Mlib.UI.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;

    public static class AppWindowManager
    {
        static AppWindowManager()
        {
            //TODO external settings + last position and size on close
            var windowHeightScale = 0.8;
            var windowWidthScale = 0.82;
            var minSizeProportionPercent = 0.7;

            var height = SystemParameters.PrimaryScreenHeight * windowHeightScale;
            var width = SystemParameters.PrimaryScreenWidth * windowWidthScale;
            var left = (SystemParameters.PrimaryScreenWidth / 2) - (width / 2);
            var top = (SystemParameters.PrimaryScreenHeight / 2) - (height / 2);

            defaultSettings.Add(Window.WidthProperty, width);
            defaultSettings.Add(Window.HeightProperty, height);
            defaultSettings.Add(Window.MinHeightProperty, height * minSizeProportionPercent);
            defaultSettings.Add(Window.MinWidthProperty, width * minSizeProportionPercent);
            defaultSettings.Add(Window.LeftProperty, left);
            defaultSettings.Add(Window.TopProperty, top);


        }
        static Dictionary<DependencyProperty, object> defaultSettings = new Dictionary<DependencyProperty, object>()
            {
                { Window.SizeToContentProperty, SizeToContent.Manual},
                { Window.BorderThicknessProperty, new Thickness(1)},
                { Window.ResizeModeProperty, ResizeMode.CanResize}
            };
        public static void ShowWindow<T>(T viewModel, IDictionary<DependencyProperty, object> windowSettings = null) where T : IViewModel
        {
            windowSettings?.ForEach(n =>
            {
                if (defaultSettings.ContainsKey(n.Key))
                {
                    defaultSettings[n.Key] = n.Value;
                }
            });
            var window = new ShellViewModel(viewModel, defaultSettings);

            IoC.Get<IWindowManager>().ShowWindow(window);
        }
        public static void ShowWindow<T>(IDictionary<DependencyProperty, object> settings = null) where T : IViewModel
        {
            ShowWindow((T)IoC.GetInstance(typeof(T), null), settings);
        }
        static IViewModel playlistVM;
        public static void SetPlaylistPanel(AudioPlayer audioPlayer)
        {
            playlistVM = new PlaylistViewModel(audioPlayer);
            SetMainPanel(playlistVM);
        }
        public static void SetMainPanel(IViewModel viewModel)
        {
            if (!(viewModel is PlaylistViewModel))
            {
                SetRightSidePanel(playlistVM);
            }
            IoC.Get<IMainViewModel>().MainPanel = viewModel;
        }
        public static void SetLeftSidePanel(IViewModel viewModel) => IoC.Get<IMainViewModel>().LeftSidePanel = viewModel;
        public static void SetRightSidePanel(IViewModel viewModel) => IoC.Get<IMainViewModel>().RightSidePanel = viewModel;
        public static void SetBottomPanel(IViewModel viewModel) => IoC.Get<IMainViewModel>().BottomPanel = viewModel;


        public static T FindVisualChild<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        return (T)child;
                    }

                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null) return childItem;
                }
            }
            return null;
        }
        public static T FindVisualParent<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(depObj);
                if (parent != null && parent is T)
                {
                    return (T)parent;
                }

                T parentItem = FindVisualParent<T>(parent);
                if (parentItem != null) return parentItem;

            }
            return null;
        }
    }
}
