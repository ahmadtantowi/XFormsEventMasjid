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
    class DkmViewModel : INotifyPropertyChanged
    {
        private List<Dkm> dkm;
        public List<Dkm> Dkms
        {
            get { return dkm; }
            set
            {
                dkm = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task Load()
        {
            var service = new DataService();
            Dkms = await service.GetListDkm();
        }
    }
}
