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
        EventViewModel eventViewModel = new EventViewModel();

		public DkmEventPage ()
		{
			InitializeComponent ();

            eventViewModel.LoadByDkm();
            BindingContext = eventViewModel;
            
            tiKeluarkan.Clicked += (sender, e) =>
            {
                CrossSettings.Current.AddOrUpdateValue("isLogin", false);
                Navigation.PopToRootAsync();
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

        protected async void Navigasikan(object sender, EventArgs args)
        {
            string type = (string)((ToolbarItem)sender).CommandParameter;
            Type pageType = Type.GetType("EventMasjid.View." + type, true);
            Page page = (Page)Activator.CreateInstance(pageType);
            await this.Navigation.PushAsync(page);
        }
    }
}