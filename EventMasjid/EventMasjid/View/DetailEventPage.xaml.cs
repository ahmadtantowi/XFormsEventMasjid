using EventMasjid.Model;
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
	public partial class DetailEventPage : ContentPage
	{
		public DetailEventPage(Event events)
		{
			InitializeComponent ();

            imgDetail.Source = events.Gambar;
            lblPelaksana.Text = events.Dkm_Pelaksana;
            lblAcara.Text = events.Nama_Event;
            lblPemateri.Text = events.Pemateri;
            lblWaktu.Text = events.Waktu_Event;
            //lblWaktu.Text = events.Waktu_Event.ToString("hh:mm dd MMMM yyyy");
            lblKontak.Text = events.Tlp_Event;
            lblLokasi.Text = events.Lokasi_Event;
		}
	}
}