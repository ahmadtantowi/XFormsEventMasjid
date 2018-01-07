using EventMasjid.Model;
using EventMasjid.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EventMasjid.ViewModel
{
    class EventViewModel : INotifyPropertyChanged
    {
        private List<Event> events;
        public List<Event> Events
        {
            get { return events; }
            set
            {
                events = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadAll()
        {
            var service = new DataService();
            Events = await service.GetListEvent();
        }

        public async Task LoadByDkm()
        {
            var service = new DataService();
            Events = await service.GetMyEvent();
        }
    }
}
