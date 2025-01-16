using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SZGYA13C_Setahajok
{
    internal class Hajo
    {
        public int Id { get; set; }
        public string Nev { get; set; }
        public string Tipus { get; set; }
        public int MaxUtas { get; set; }
        public int NapiBerletiDij { get; set; }

        public Hajo(int id, string nev, string tipus, int maxutas, int napiberletidij)
        {
            Id = id;
            Nev = nev;
            Tipus = tipus;
            MaxUtas = maxutas;
            NapiBerletiDij = napiberletidij;
        }

        public static List<Hajo> Fromfile(string p)
        {
            var hajo = new List<Hajo>();

            string[] line = File.ReadAllLines(p);

            foreach (string l in line)
            {
                string[] r = l.Split(',');

                int Id = int.Parse(r[0]);
                string Nev = r[1];
                string Tipus = r[2];
                int MaxUtas = int.Parse(r[3]);
                int NapiBerletiDij = int.Parse(r[4]);

                Hajo hajok = new(Id, Nev, Tipus, MaxUtas, NapiBerletiDij);
                hajo.Add(hajok);
            }
            return hajo;
        }

        public override string ToString()
        {
            return $"{Id}, {Nev}, {Tipus}, {MaxUtas}, {NapiBerletiDij}";
        }
    }
}
