using EventMasjid.Model;
using EventMasjid.ViewModel;
using Plugin.Settings;
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
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();

            EventViewModel eventViewModel = new EventViewModel();
            eventViewModel.LoadAll();
            BindingContext = eventViewModel;

            var isLogin = CrossSettings.Current.GetValueOrDefault("isLogin", false);

            tiSegarkan.Clicked += async (sender, e) => await eventViewModel.LoadAll();
            tiTambah.Clicked += async (sender, e) => await Navigation.PushAsync(new SaveEventPage(new Event(), true));
		}

        protected void PadaItemDipilih(object sender, SelectedItemChangedEventArgs args)
        {
            var selectedItem = args.SelectedItem as Event;
            //Debug.WriteLine("id:{0} nama:{1} waktu:{2} pemateri:{3} telp:{4} lokasi:{5} gambar:{6}", 
            //    selectedItem.Id_Event, selectedItem.Nama_Event, selectedItem.Waktu_Event, selectedItem.Pemateri, selectedItem.Tlp_Event, selectedItem.Lokasi_Event, selectedItem.Gambar);
            //Debug.WriteLine(selectedItem.ToString());

            if (selectedItem != null)
            {
                // action
                Navigation.PushAsync(new DetailEventPage(selectedItem));

                // menghapus item yang telah dipilih
                var listview = sender as ListView;
                if (listview != null)
                    listview.SelectedItem = null;
            }
        }

        protected async void Navigasikan(object sender, EventArgs args)
        {
            string type = (string)((ToolbarItem)sender).CommandParameter;
            Type pageType = Type.GetType("EventMasjid.View." + type, true);
            Page page = (Page)Activator.CreateInstance(pageType);
            await this.Navigation.PushAsync(page);
        }
    }
}