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
	public partial class SaveDkmPage : ContentPage
	{
        bool isNewDkm;

		public SaveDkmPage (bool isNewDkm = false)
		{
			InitializeComponent ();
            this.isNewDkm = isNewDkm;

            lblUname.Text = isNewDkm ? null : MyDkm.Uname_Dkm;
            lblPword.Text = isNewDkm ? null : MyDkm.Pass_Dkm;
            lblDkm.Text = isNewDkm ? null : MyDkm.Masjid_Dkm;
            lblAlamat.Text = isNewDkm ? null : MyDkm.Alamat_Dkm;
            lblTelp.Text = isNewDkm ? null : MyDkm.Tlp_Dkm;
            lblEmail.Text = isNewDkm ? null : MyDkm.Email_Dkm;
            lblKetua.Text = isNewDkm ? null : MyDkm.Ketua_Dkm;

            lblUname.IsEnabled = isNewDkm ? true : false;
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
            if (await service.SaveDkm(addDkm, isNewDkm))
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
            Navigation.PopAsync(true);
        }
    }
}