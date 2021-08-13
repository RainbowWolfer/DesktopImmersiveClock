using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace DesktopImmersiveClock {
	public sealed partial class MainPage: Page {
		public IUpdateTime current;
		public SplitView _MySplitView => MySplitView;
		public MainPage() {
			this.InitializeComponent();
			Navigate(typeof(SlideStyle));
		}

		private async void Page_Loaded(object sender, RoutedEventArgs e) {
			while(true) {
				await Task.Delay(1000);
				current?.Update();
			}
		}
		private void Navigate(Type target) {
			MainFrame.Navigate(target, this, new DrillInNavigationTransitionInfo());
		}

		private void Border_Tapped(object sender, TappedRoutedEventArgs e) {
			if(MySplitView.IsPaneOpen) {
				return;
			}
			MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
		}

		private void FullScreenButton_Tapped(object sender, TappedRoutedEventArgs e) {
			var view = ApplicationView.GetForCurrentView();
			if(view.IsFullScreenMode) {
				view.ExitFullScreenMode();
				ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Auto;
				FullScreenButton.Content = "\uE740";
			} else {
				if(view.TryEnterFullScreenMode()) {
					ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
					FullScreenButton.Content = "\uE73F";
				}
			}
		}

		private void SlideButton_Tapped(object sender, TappedRoutedEventArgs e) {
			Navigate(typeof(SlideStyle));
		}

		private void AnalogButton_Tapped(object sender, TappedRoutedEventArgs e) {
			Navigate(typeof(Analog));
		}

		private void DigitalButton_Tapped(object sender, TappedRoutedEventArgs e) {
			Navigate(typeof(ClassicDigit));
		}
	}
}
