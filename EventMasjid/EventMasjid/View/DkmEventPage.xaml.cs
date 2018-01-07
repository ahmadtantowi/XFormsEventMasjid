using EventMasjid.Model;
using EventMasjid.ViewModel;
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
	public partial class DkmEventPage : ContentPage
	{
		public DkmEventPage ()
		{
			InitializeComponent ();

            EventViewModel eventViewModel = new EventViewModel();
            eventViewModel.LoadByDkm();
            BindingContext = eventViewModel;

            tiTambahkan.Clicked += (sender, e) => Navigation.PushAsync(new SaveEventPage(new Event(), true));
            tiProfil.Clicked += (sender, e) => Navigation.PushAsync(new SaveDkmPage());
            tiKeluarkan.Clicked += (sender, e) =>
            {
                CrossSettings.Current.AddOrUpdateValue("isLogin", false);
                Navigation.PushAsync(new HomePage());
            };
        }

        protected void PadaItemDipilih(object sender, SelectedItemChangedEventArgs args)
        {
            var selectedItem = args.SelectedItem as Event;
            //Debug.WriteLine("id:{0} nama:{1} waktu:{2} pemateri:{3} telp:{4} lokasi{5}",
            //    selectedItem.Id_Event, selectedItem.Nama_Event, selectedItem.Waktu_Event, selectedItem.Pemateri, selectedItem.Tlp_Event, selectedItem.Lokasi_Event);

            if (selectedItem != null)
            {
                // action
                Navigation.PushAsync(new SaveEventPage(selectedItem, false));

                // menghapus item yang telah dipilih
                var listview = sender as ListView;
                if (listview != null)
                    listview.SelectedItem = null;
            }
        }
    }
}