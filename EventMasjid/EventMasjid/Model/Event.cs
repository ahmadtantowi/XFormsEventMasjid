using System;
using System.Collections.Generic;
using System.Text;

namespace EventMasjid.Model
{
    public class Event
    {
        public int Id_Event { get; set; }

        public string Dkm_Pelaksana { get; set; }

        public string Nama_Event { get; set; }

        public string Pemateri { get; set; }

        public string Lokasi_Event { get; set; }

        public string Tlp_Event { get; set; }

        public DateTime Waktu_Event { get; set; }
    }
}
