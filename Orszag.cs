using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF2
{
    internal class Orszag
    {
        public string Orszagnev { get; set; }
        public int Terulet { get; set; }
        public int Nepesseg { get; set; }
        public string Fovaros { get; set; }
        public int Fovnepessege { get; set; }

        public Orszag(string sor)
        {
            string[] resz = sor.Split(';');
            Orszagnev = resz[0];
            Terulet = int.Parse(resz[1]);
            if ((resz[2].Last() == 'g'))
            {
                Nepesseg = int.Parse(resz[2].Substring(0, resz[2].Length - 1)) * 10000;
            }
            else
            {
                Nepesseg = int.Parse(resz[2]);
            }
            Fovaros = resz[3];
            Fovnepessege = int.Parse(resz[4]);
        }
    }
}
