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
	public partial class SaveEventPage : ContentPage
	{
        bool isNewEvent;
        string idEvent;

        public SaveEventPage(Event events, bool isNewEvent = false)
		{
			InitializeComponent();
            this.isNewEvent = isNewEvent;
            this.idEvent = events.Id_Event;

            imgDetail.Source = events.Gambar;
            lblPelaksana.Text = events.Dkm_Pelaksana;
            lblAcara.Text = events.Nama_Event;
            lblPemateri.Text = events.Pemateri;
            lblLokasi.Text = events.Lokasi_Event;
            lblKontak.Text = events.Tlp_Event;
		}
        
        async void BtnSimpankan(object sender, EventArgs e)
        {
            var addEvent = new Event()
            {
                Id_Event = this.idEvent,
                Uname_Dkm = MyDkm.Uname_Dkm,
                Dkm_Pelaksana = MyDkm.Masjid_Dkm,
                Nama_Event = lblAcara.Text,
                Pemateri = lblPemateri.Text,
                Lokasi_Event = lblLokasi.Text,
                Tlp_Event = lblKontak.Text,
                //Waktu_Event = new DateTime(lblTanggal.Date.Year, lblTanggal.Date.Month, lblTanggal.Date.Day, lblWaktu.Time.Hours, lblWaktu.Time.Minutes, 0),
                Waktu_Event = new DateTime(lblTanggal.Date.Year, lblTanggal.Date.Month, lblTanggal.Date.Day, lblWaktu.Time.Hours, lblWaktu.Time.Minutes, 0).ToString(),
            };
            Debug.WriteLine("id_event:{0} dkm_pelaksana:{1} nama_event:{2} pemateri:{3} lokasi_event:{4} tlp_event:{5} waktu_event:{6}",
               addEvent.Id_Event, addEvent.Dkm_Pelaksana, addEvent.Nama_Event, addEvent.Pemateri, addEvent.Lokasi_Event, addEvent.Tlp_Event, addEvent.Waktu_Event);

            var service = new DataService();
            if (await service.SaveEvent(addEvent, this.isNewEvent))
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