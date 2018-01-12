using EventMasjid.Model;
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
	public partial class SaveDkmPage : ContentPage
	{
        bool isNewDkm;
        string idDkm;

		public SaveDkmPage(bool isNewDkm = false)
		{
            InitializeComponent();
            this.isNewDkm = isNewDkm;
            this.idDkm = MyDkm.Id_Dkm;

            this.Title = isNewDkm ? "Buat Akun Baru" : "Ubah Profil DKM";

            lblUname.Text = isNewDkm ? null : MyDkm.Uname_Dkm;
            lblPword.Text = isNewDkm ? null : MyDkm.Pass_Dkm;
            lblDkm.Text = isNewDkm ? null : MyDkm.Masjid_Dkm;
            lblAlamat.Text = isNewDkm ? null : MyDkm.Alamat_Dkm;
            lblTelp.Text = isNewDkm ? null : MyDkm.Tlp_Dkm;
            lblEmail.Text = isNewDkm ? null : MyDkm.Email_Dkm;
            lblKetua.Text = isNewDkm ? null : MyDkm.Ketua_Dkm;

            lblUname.IsEnabled = isNewDkm ? true : false;
            Segarkan(null, null);
		}

        async void Simpankan(object sender, EventArgs e)
        {
            //if (!lblPword2.Text.Contains(lblPword.Text))
            //{
            //    lblNotif.Text = "Isi kata sandi dengan benar!";
            //    return;
            //}

            var addDkm = new Dkm()
            {
                Id_Dkm = idDkm,
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
                await DisplayAlert("Info", "Dkm berhasil disimpan", "OK");

                if (isNewDkm)
                    Navigation.InsertPageBefore(new DkmEventPage(), this);

                CrossSettings.Current.AddOrUpdateValue("isLogin", false);
                await Navigation.PopToRootAsync();
            }
            else
            {
                await DisplayAlert("Info", "Dkm gagal disimpan.", "OK");
            }
        }

        async void Hapuskan(Object sender, EventArgs e)
        {
            var service = new DataService();
            if (!this.isNewDkm)
            {
                if (await DisplayAlert("Hapus", "Apa Anda yakin ingin menghapus DKM Anda?\nIni tidak dapat dikembalikan", "Ya", "Tidak"))
                {
                    if (await service.DeleteDkmEvent(this.idDkm, true))
                    {
                        await DisplayAlert("Info", "DKM berhasil dihapus", "OK");
                        await Navigation.PopToRootAsync();
                    }
                    else
                        await DisplayAlert("Info", "DKM gagal dihapus", "OK");
                }
            }
            else
                await Navigation.PopAsync();
        }

        async void Segarkan(object sender, EventArgs e)
        {
            var service = new DataService();
            lblNotif.Text = await service.GetMyDkm();
        }
    }
}