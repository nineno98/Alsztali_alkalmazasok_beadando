using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vasmegye
{
    internal class SzemelyiSzam
    {
        // 1 - ff, 0 nő;
        private int _nem;
        private DateTime _szuletesi_datum;
        private int _azon;

        public int Nem { get => _nem; set => _nem = value; }
        public DateTime Szuletesi_datum { get => _szuletesi_datum; set => _szuletesi_datum = value; }
        public int Azon { get => _azon; set => _azon = value; }

        public SzemelyiSzam(int nem, DateTime szuletesi_datum, int azon)
        {
            _nem = nem;
            _szuletesi_datum = szuletesi_datum;
            _azon = azon;
        }
        public override string ToString()
        {
            return $"Nem: {_nem} szuldate: {_szuletesi_datum.ToString()} azon: {_azon}";
        }
    }
}
