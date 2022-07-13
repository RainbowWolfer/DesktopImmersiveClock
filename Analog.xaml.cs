using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace DesktopImmersiveClock {
	public sealed partial class Analog: Page, IUpdateTime {
		public Analog() {
			this.InitializeComponent();
			Update();
		}
		protected override void OnNavigatedTo(NavigationEventArgs e) {
			base.OnNavigatedTo(e);
			(e.Parameter as MainPage).current = this;
		}

		public void Update() {
			int hour = DateTime.Now.Hour;//24
			if(hour >= 12) {
				hour -= 12;
			}
			int min = DateTime.Now.Minute;//60
			int sec = DateTime.Now.Second;//60

			Hour.Rotation = 360 * hour / 12d;
			Debug.WriteLine($"{hour}-{Hour.Rotation}");
			Minute.Rotation = 360 * min / 60d;
			Second.Rotation = 360 * sec / 60d;

		}
	}
}
