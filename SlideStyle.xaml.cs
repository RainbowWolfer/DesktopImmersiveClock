using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
using Windows.UI.Xaml.Navigation;

namespace DesktopImmersiveClock {
	public sealed partial class SlideStyle: Page, IUpdateTime {
		public SlideStyle() {
			this.InitializeComponent();
			Bar1.Initialize(0, 2);
			Bar2.Initialize(0, 9);
			Bar3.Initialize(0, 5);
			Bar4.Initialize(0, 9);
			Bar5.Initialize(0, 5);
			Bar6.Initialize(0, 9);
			Update();
		}
		protected override void OnNavigatedTo(NavigationEventArgs e) {
			base.OnNavigatedTo(e);
			(e.Parameter as MainPage).current = this;
		}

		private void ChangeToNow() {
			var hour = App.Translate(DateTime.Now.Hour);
			var min = App.Translate(DateTime.Now.Minute);
			var sec = App.Translate(DateTime.Now.Second);
			Bar1.ChangeToNumber(hour.Item1);
			Bar2.ChangeToNumber(hour.Item2);
			Bar3.ChangeToNumber(min.Item1);
			Bar4.ChangeToNumber(min.Item2);
			Bar5.ChangeToNumber(sec.Item1);
			Bar6.ChangeToNumber(sec.Item2);

			MonthBlock.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
			DayBlock.Text = DateTime.Now.Day.ToString();

			string era = DateTime.Now.DayOfWeek.ToString();
			WeekBlock.Text = era.Insert(era.LastIndexOf("day"), "\n").ToUpper();
		}

		public void Update() {
			ChangeToNow();
		}
	}
}
