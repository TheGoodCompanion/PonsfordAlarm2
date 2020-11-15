using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PonsfordAlarm2.Helpers;
using PonsfordAlarm2.Models;
using PonsfordAlarm2.Repositories;
using PonsfordAlarm2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PonsfordAlarm2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page1 : ContentPage
	{

		public Page1 ()
		{
			InitializeComponent();
        }

        public async void ReturnToList()
        {
            await Navigation.PushAsync(new AlarmSet());
        }
    }
}