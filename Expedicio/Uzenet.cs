using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedicio
{

    internal class Uzenet
    {
        private int _napsorszam;
        private int _radiosID;
        private string _uzenet;

        public int Napsorszam { get => _napsorszam; set => _napsorszam = value; }
        public int RadiosID { get => _radiosID; set => _radiosID = value; }
        public string Uzenet_ { get => _uzenet; set => _uzenet = value; }

        public Uzenet(int napsorszam, int radiosID, string uzenet)
        {
            _napsorszam = napsorszam;
            _radiosID = radiosID;
            _uzenet = uzenet;
        }
        public override string ToString()
        {
            return $"Nap: {_napsorszam}, a vevő rádiós: {_radiosID}\nAz üzenet: {_uzenet}";
        }
    }
}
