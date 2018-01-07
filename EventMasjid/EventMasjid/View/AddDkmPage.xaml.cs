using EventMasjid.Model;
using EventMasjid.Service;
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
	public partial class AddDkmPage : ContentPage
	{
		public AddDkmPage ()
		{
			InitializeComponent ();
		}

        async void BtnTambahkan (object sender, EventArgs e)
        {
            var addDkm = new Dkm()
            {
                Uname_Dkm = lblUname.Text,
                Pass_Dkm = lblPword.Text,
                Masjid_Dkm = lblDkm.Text,
                Alamat_Dkm = lblAlamat.Text,
                Tlp_Dkm = lblTelp.Text,
                Email_Dkm = lblEmail.Text,
                Ketua_Dkm = lblKetua.Text,
            };

            DataService service = new DataService();
            if (await service.SaveDkm(addDkm, true))
            {
                await DisplayAlert("Info", "Data berhasil disimpan", "OK");
            }
            else
            {
                await DisplayAlert("Info", "Data gagal disimpan.", "OK");
            }
        }

        void BtnBatalkan(object sender, EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }
    }
}