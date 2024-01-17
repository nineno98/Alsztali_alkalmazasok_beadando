using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hotellift
{
    internal class Program
    {
        public static List<Lift> lifts;
        static void Main(string[] args)
        {
             //= new List<Lift>();
            lifts = beolvasas();
            //DateTime dt = toDateformat("1998.06.24.");
            //Console.WriteLine(dt);
            
            //1.feladat
            Console.WriteLine("3. feladat: Összes lifthasználat: "+lifts.Count);
            //4. feladat
            Console.WriteLine("4. feladat: Időszak: " + lifts.Select(x => x.Idopont).Min().ToString("yyyy-MM-dd")+
                " - "+ lifts.Select(x => x.Idopont).Max().ToString("yyyy-MM-dd"));
            //5.feladat
            Console.WriteLine("5. feladat: Célszint max: " + lifts.Select(x => x.CelSzint).Max());

            //6.feladat
            int kartyaszam, celszint;
            Console.WriteLine("6. feladat:");
            Console.WriteLine("Adja meg a kártya számát!");
            kartyaszam = szambekeres();
            Console.WriteLine("Adja meg a célszint számát!");
            celszint = szambekeres();
            Console.WriteLine($"\tKártya száma: {kartyaszam}\n\tCélszint száma: {celszint}");

            //7. feladat
            Console.Write("7.feladat ");
            Console.WriteLine(Utaztake(kartyaszam,celszint) ? "A(z) "+kartyaszam+" kártyával utaztak a(z) "+celszint+" emeletre" : "A(z) " + kartyaszam + " kártyával nem utaztak a(z) " + celszint + " emeletre");

            //8. feladat

            Console.WriteLine("8.feladat: Statisztika");
            List<(DateTime, int)> statistic = CreateStat();
           
            foreach (var item in statistic)
            {
                Console.WriteLine("\t"+item.Item1.ToString("yyyy-MM-dd")+" - "+item.Item2+"x");
            }

            Console.ReadKey();
            
        }

        private static List<(DateTime, int)> CreateStat()
        {
            var dateSet = new HashSet<DateTime>(lifts.Select(x => x.Idopont)).ToList();
            List<(DateTime, int)> stat = new List<(DateTime, int)>();
            int counter = 0;
            for (int i = 0; i < dateSet.Count; i++)
            {
                for (int j = 0; j < lifts.Count; j++)
                {
                   
                    if (dateSet[i].Equals(lifts[j].Idopont))
                    {
                        counter++;
                    }
                    
                }
                stat.Add((dateSet[i], counter));
                counter = 0;
            }
            return stat;
            
        }

        private static bool Utaztake(int kartyaszam, int celszint)
        {
            foreach (var lift in lifts)
            {
                if (lift.KartyaId == kartyaszam && lift.CelSzint == celszint)
                {
                    return true;
                }
            }
            return false;
        }

        private static int szambekeres()
        {
            int bekertSzam;
            while (true)
            {
                try
                {
                    bekertSzam = Convert.ToInt32(Console.ReadLine());
                    return bekertSzam;
                }
                catch (Exception)
                {

                    Console.WriteLine("Hiba. Az alapértelmezett érték került beállításra (5).");
                    return 5;
                }
            }
            
        }

        private static DateTime toDateformat(string str)
        {
            DateTime dt;
            var validDate = DateTime.TryParse(str, out dt);
            if (validDate)
            {
                return dt;
            }
            else
            {
                Console.WriteLine($"{str} sztring nem konvertálható.");
                return dt;
            }
        }

        private static List<Lift> beolvasas()
        {
            List<Lift> lifts2 = new List<Lift>();
            StreamReader reader = new StreamReader("lift.txt");
            string line;
            string[] tmp;
            while ((line = reader.ReadLine()) != null)
            {
                tmp = line.Split(' ');
                lifts2.Add(new Lift(toDateformat(tmp[0]),Convert.ToInt32(tmp[1]), Convert.ToInt32(tmp[2]), Convert.ToInt32(tmp[3])));
            }
            return lifts2;
        }
    }
}
