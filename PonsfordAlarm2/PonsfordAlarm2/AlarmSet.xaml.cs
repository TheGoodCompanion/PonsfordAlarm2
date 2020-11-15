using Newtonsoft.Json;
using PonsfordAlarm2.Helpers;
using PonsfordAlarm2.Models;
using PonsfordAlarm2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PonsfordAlarm2
{
	public partial class AlarmSet : ContentPage
	{
        

		public AlarmSet ()
		{
			InitializeComponent();
		}

        private async void Add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page1());
        }
    }
}