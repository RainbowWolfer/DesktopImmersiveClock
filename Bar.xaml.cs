using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace DesktopImmersiveClock {
	public sealed partial class Bar: UserControl {
		public int AnimationSpeed => 15;
		public Duration AnimationDuration => new Duration(TimeSpan.FromSeconds(0.95d));
		public bool ShowDebug { get; set; }
		private bool first = true;
		private BarInfo info;
		private int current;
		public Bar() {
			this.InitializeComponent();
		}

		public void ChangeToNumber(int target) {
			//translate
			int result;
			if(target < info.Count) {
				result = target - 1;
			} else {
				result = 0;
			}
			Debug.WriteLine($"target:{target}\tresult{result}\tcurrent{current}\tfirst{first}");
			if(!first) {
				if(target == current) {
					return;
				}
			}
			current = result;
			DoTheThing();
			first = false;
		}

		private void DoTheThing() {
			int from = current;
			int to = ++current;
			if(to > info.Count - 1) {
				current = 0;
			}

			//info.Swift();
			MoveRectangle(current);
			HideNumber(current);

			Swift(from < 0 ? info.Count - 1 : from, current);
		}

		public void Initialize(int from, int to) {
			if(from > to) {
				throw new Exception("??");
			}
			info = new BarInfo(to - from + 1);
			TextPanel.Children.Clear();

			for(int i = from; i <= to; i++) {
				TextPanel.Children.Add(new TextBlock() {
					Style = MyTextStyleForName,
					Text = i.ToString(),
				});
			}

			BackGroundShadowPanel.Height = info.Height;
			PanelTransform.TranslateY = info.Initial;
			TextPanelTransform.TranslateY = info.Initial;
		}

		private void MoveRectangle(int current) {
			Storyboard storyboard = new Storyboard() {
				Duration = AnimationDuration,
			};

			int rate = 68;
			DoubleAnimation translateYAnimation = new DoubleAnimation {
				From = PanelTransform.TranslateY,
				To = info.Initial - rate * current,
				EasingFunction = new ExponentialEase() {
					Exponent = AnimationSpeed,
					EasingMode = EasingMode.EaseOut,
				},
			};

			Storyboard.SetTarget(translateYAnimation, PanelTransform);
			Storyboard.SetTargetProperty(translateYAnimation, "TranslateY");
			//To = reset ? info.Initial : TextPanelTransform.TranslateY - rate,
			DoubleAnimation translateYAnimation2 = new DoubleAnimation {
				From = TextPanelTransform.TranslateY,
				To = info.Initial - rate * current,
				EasingFunction = new ExponentialEase() {
					Exponent = AnimationSpeed,
					EasingMode = EasingMode.EaseOut,
				},
			};

			Storyboard.SetTarget(translateYAnimation2, TextPanelTransform);
			Storyboard.SetTargetProperty(translateYAnimation2, "TranslateY");

			storyboard.Children.Add(translateYAnimation);
			storyboard.Children.Add(translateYAnimation2);

			storyboard.Begin();
			storyboard.Completed += Storyboard_Completed;
		}

		private void Storyboard_Completed(object sender, object e) {
			//hasSomePartMoving = false;
		}

		private void HideNumber(int current) {
			List<TextBlock> tbs = new List<TextBlock>();
			foreach(UIElement item in TextPanel.Children) {
				if(item is TextBlock tb) {
					tbs.Add(tb);
				}
			}

			Storyboard storyboard = new Storyboard() {
				Duration = AnimationDuration,
			};

			foreach(TextBlock item in tbs) {
				DoubleAnimation OpacityAnimation = new DoubleAnimation {
					From = item.Opacity,
					To = item.Text == current.ToString() ? 0 : 1,
					EasingFunction = new ExponentialEase() {
						Exponent = AnimationSpeed,
						EasingMode = EasingMode.EaseOut,
					},
				};

				Storyboard.SetTarget(OpacityAnimation, item);
				Storyboard.SetTargetProperty(OpacityAnimation, "Opacity");

				storyboard.Children.Add(OpacityAnimation);
			}

			storyboard.Begin();
			storyboard.Completed += Storyboard_Completed;
		}

		private void Swift(int from, int to) {
			CenterNumberText.Text = from.ToString();
			ComingNumberText.Text = to.ToString();
			MyStoryboard.Begin();
		}
	}
	public class BarInfo {
		public int Count { get; private set; }
		//public int CurrentCount { get; private set; }
		public int Height { get; private set; }
		public int Initial { get; private set; }
		//public bool ReachMax {
		//	get {
		//		Debug.WriteLine(" Check " + CurrentCount + "_" + Count);
		//		return CurrentCount > Count - 1;
		//	}
		//}

		//public void Swift() {
		//	if(CurrentCount >= Count) {
		//		CurrentCount = 0;
		//	}
		//	CurrentCount++;
		//}

		public BarInfo(int count) {
			//CurrentCount = 0;
			this.Count = count;
			switch(this.Count) {
				case 1:
					Height = 110;
					Initial = 0;
					break;
				case 2:
					Height = 140;
					Initial = 35;
					break;
				case 3:
					Height = 220;
					Initial = 65;
					break;
				case 4:
					Height = 290;
					Initial = 100;
					break;
				case 5:
					Height = 350;
					Initial = 135;
					break;
				case 6:
					Height = 430;
					Initial = 170;
					break;
				case 7:
					Height = 490;
					Initial = 205;
					break;
				case 8:
					Height = 560;
					Initial = 240;
					break;
				case 9:
					Height = 640;
					Initial = 275;
					break;
				case 10:
					Height = 720;
					Initial = 305;
					break;
				default:
					throw new Exception("?");
			}
		}
	}
}
