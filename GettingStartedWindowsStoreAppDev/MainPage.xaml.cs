﻿using Windows.Storage;
using GettingStartedWindowsStoreAppDev.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace GettingStartedWindowsStoreAppDev
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        
        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public MainPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }


        public const string PageStateOutputTextBlockTextKey = "PageStateOutputTextBlock.Text";

        public const string LocalSettingsInputTextBoxTextKey = "LocalSettingsStateInputTextBox.Text";
        public const string LocalSettingsOutputTextBlockTextKey = "LocalSettingsStateOutputTextBlock.Text";


        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            e.PageState[PageStateOutputTextBlockTextKey] = PageStateOutputTextBlock.Text;
        }


        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            if (e.PageState != null && e.PageState.ContainsKey(PageStateOutputTextBlockTextKey))
            {
                PageStateOutputTextBlock.Text = (string)e.PageState[PageStateOutputTextBlockTextKey];
            }

            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(LocalSettingsInputTextBoxTextKey))
            {
                LocalSettingsInputTextBox.Text =
                    (string)ApplicationData.Current.LocalSettings.Values[LocalSettingsInputTextBoxTextKey];
            }
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(LocalSettingsOutputTextBlockTextKey))
            {
                if (!string.Equals(LocalSettingsInputTextBox.Text, string.Empty))
                {
                    LocalSettingsOutputTextBlock.Text =
                        (string)ApplicationData.Current.LocalSettings.Values[LocalSettingsOutputTextBlockTextKey];
                }
            }
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void PageBetaHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageBeta));
        }

        private void PageGammaHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageGamma));
        }

        private void LocalSettingsInputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var curText = LocalSettingsInputTextBox.Text;
            var newText = string.Empty;
            if (!string.Equals(curText, string.Empty))
            {
                newText = "LocalSettingsOutputTextBlock.Text == \"" + curText + "\"";
            }
            LocalSettingsOutputTextBlock.Text = newText;

            ApplicationData.Current.LocalSettings.Values[LocalSettingsInputTextBoxTextKey] =
                LocalSettingsInputTextBox.Text;
            ApplicationData.Current.LocalSettings.Values[LocalSettingsOutputTextBlockTextKey] =
                LocalSettingsOutputTextBlock.Text;
        }

        private void PageStateInputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var curText = PageStateInputTextBox.Text;
            var newText = string.Empty;
            if (!string.Equals(curText, string.Empty))
            {
                newText = "PageStateOutputTextBlock.Text == \"" + curText + "\"";
            }
            PageStateOutputTextBlock.Text = newText;
        }

        private void AppBarRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null)
            {
                rootFrame.Navigate(rootFrame.CurrentSourcePageType);
            }
        }

        private void AppBarHelpButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AppBarForwardButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AppBarBackButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
