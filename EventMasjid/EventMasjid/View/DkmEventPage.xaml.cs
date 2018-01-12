using EventMasjid.Model;
using EventMasjid.Service;
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
        EventViewModel eventViewModel = new EventViewModel();

		public DkmEventPage ()
		{
			InitializeComponent ();

            Segarkan(null, null);
            BindingContext = eventViewModel;

            var service = new DataService();
            var x = service.GetMyDkm();
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

        protected async void Segarkan(object sender, EventArgs e)
        {
            await eventViewModel.LoadByDkm();
        }

        protected async void Navigasikan(object sender, EventArgs args)
        {
            string type = (string)((ToolbarItem)sender).CommandParameter;
            if (type.Contains("SaveDkmPage"))
            {
                await Navigation.PushAsync(new SaveDkmPage(false));
                return;
            }
            if (type.Contains("SaveEventPage"))
            {
                await Navigation.PushAsync(new SaveEventPage(new Event(), true));
                return;
            }

            Type pageType = Type.GetType("EventMasjid.View." + type, true);
            Page page = (Page)Activator.CreateInstance(pageType);
            await this.Navigation.PushAsync(page);
        }

        public void Keluarkan(object sender, EventArgs e)
        {
            CrossSettings.Current.AddOrUpdateValue("isLogin", false);
            Navigation.PopToRootAsync();
//            tes
        }
    }
}