using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace ListViewCrashTest
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			On<iOS>().SetUseSafeArea(true);
			BindingContext = ViewModel;
		}

		private MainViewModel ViewModel { get; } = new MainViewModel();
	}
}
