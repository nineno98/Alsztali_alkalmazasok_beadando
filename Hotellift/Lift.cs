using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotellift
{
    internal class Lift
    {
        private int kartyaId;
        private int induloSzint;
        private int celSzint;
        private DateTime idopont;

        public int KartyaId { get => kartyaId; set => kartyaId = value; }
        public int InduloSzint { get => induloSzint; set => induloSzint = value; }
        public int CelSzint { get => celSzint; set => celSzint = value; }
        public DateTime Idopont { get => idopont; set => idopont = value; }

        public Lift(DateTime idopont, int kartyaId, int induloSzint, int celSzint )
        {
            this.kartyaId = kartyaId;
            this.induloSzint = induloSzint;
            this.celSzint = celSzint;
            this.idopont = idopont;
        }
        public override string ToString()
        {
            return $"{idopont.ToString("yyyy-MM-dd")} {kartyaId} {induloSzint} {celSzint}";
        }
    }
}
