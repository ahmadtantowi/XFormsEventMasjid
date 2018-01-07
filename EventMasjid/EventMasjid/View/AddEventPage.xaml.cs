using EventMasjid.Model;
using EventMasjid.Service;
using EventMasjid.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EventMasjid.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEventPage : ContentPage
	{
        public AddEventPage(Event events)
		{
			InitializeComponent();
            BindingContext = new EventViewModel();

            lblPelaksana.Text = events.Dkm_Pelaksana;
		}

        public AddEventPage()
        {
            InitializeComponent(); var i = lblWaktu.Time.Hours; var j = lblTanggal.Date.Date;
        }

        async void BtnTambahkan(object sender, EventArgs e)
        {
            var addEvent = new Event()
            {
                Dkm_Pelaksana = lblPelaksana.Text,
                Nama_Event = lblAcara.Text,
                Pemateri = lblPemateri.Text,
                Lokasi_Event = lblLokasi.Text,
                Tlp_Event = lblKontak.Text,
                Waktu_Event = new DateTime(lblTanggal.Date.Year, lblTanggal.Date.Month, lblTanggal.Date.Day, lblWaktu.Time.Hours, lblWaktu.Time.Minutes, 0),

            };
            //Debug.WriteLine("dkm_pelaksana:{0} nama_event:{1} pemateri:{2} lokasi_event:{3} tlp_event:{4} waktu_event:{5}",
            //    addEvent.Dkm_Pelaksana, addEvent.Nama_Event, addEvent.Pemateri, addEvent.Lokasi_Event, addEvent.Tlp_Event, addEvent.Waktu_Event);

            //var service = new DataService();
            //if (await service.SaveEvent(addEvent, true))
            //{
            //    await DisplayAlert("Info", "Data berhasil disimpan", "OK");
            //}
            //else
            //{
            //    await DisplayAlert("Info", "Data gagal disimpan.", "OK");
            //}
        }

        void BtnBatalkan(object sender, EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }
	}
}