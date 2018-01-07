using EventMasjid.Service;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EventMasjid.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

        async void BtnMasukkan(object sender, EventArgs e)
        {
            var service = new DataService();
            if(await service.Login(uname.Text, pword.Text))
            {
                CrossSettings.Current.AddOrUpdateValue("isLogin", true);

                lblNotif.Text = "Login berhasil!";
                lblNotif.TextColor = Color.Green;
                await Navigation.PushAsync(new DkmEventPage());
            }
            else
            {
                lblNotif.Text = "Login gagal!";
                lblNotif.TextColor = Color.Red;
                //await DisplayAlert("Info", "Gagal Masuk. Terjadi kesalahan.", "OK");
            }
        }

        void BtnBuatkan(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SaveDkmPage(true));
        }

        void BtnBatalkan(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
        }
    }
}