using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace DesktopImmersiveClock {
	public sealed partial class ClassicDigit: Page, IUpdateTime {
		public ClassicDigit() {
			this.InitializeComponent();
			Update();
		}
		protected override void OnNavigatedTo(NavigationEventArgs e) {
			base.OnNavigatedTo(e);
			(e.Parameter as MainPage).current = this;
		}

		public void Update() {
			string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
			int day = DateTime.Now.Day;
			string week = DateTime.Now.DayOfWeek.ToString();
			var o_h = App.Translate(DateTime.Now.Hour);
			var o_m = App.Translate(DateTime.Now.Minute);
			var o_s = App.Translate(DateTime.Now.Second);
			string hour = $"{o_h.Item1}{o_h.Item2}";
			string min = $"{o_m.Item1}{o_m.Item2}";
			string sec = $"{o_s.Item1}{o_s.Item2}";
			DayBlock.Text = $"{month} {day} {week}";
			DateTimeBlock.Text = $"{hour}:{min}:{sec}";
		}
	}
}
