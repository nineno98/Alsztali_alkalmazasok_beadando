using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vasmegye
{
    internal class Program
    {
        public static List<SzemelyiSzam> szamok;
        static void Main(string[] args)
        {
            szamok = new List<SzemelyiSzam> ();
            //1. - 4. feladat
            Console.WriteLine("4. feladat ellenőrzés:");
            FileBeolvas();
            
            //5. feladat
            Console.WriteLine($"5. feladat: Vas megyében a vizsgált időszak alatt {szamok.Count} csecsemő született. ");

            //6.feladat
            Console.WriteLine($"6. feladat: Fiúk száma: {szamok.Where(x => x.Nem == 1).Count()}");
            //7. feladat
            Console.WriteLine($"7. feladat: Vizsgált időszak: {szamok.Select(x => x.Szuletesi_datum).Min().ToString("yyyy")} - {szamok.Select(x => x.Szuletesi_datum).Max().ToString("yyyy")}");
            //8. feladat
            Console.Write("8. feladat: ");
            Console.WriteLine(szamok.Where(x => x.Szuletesi_datum.Month == 2 && x.Szuletesi_datum.Day == 24 && x.Szuletesi_datum.Year % 4 == 0).Count() > 0 ? "Szökőnapon született baba!" : "Szokőnapon nem született baba!");
            Console.WriteLine("9. feladat: Statisztika");
            //9. feladat

            foreach (var ev in szamok.Select(x => x.Szuletesi_datum.Year).Distinct())
            {
                Console.WriteLine("\t"+ev+" - "+szamok.Where(x => x.Szuletesi_datum.Year == ev).Count()+" fő");
            }
            Console.ReadKey();
        }

        private static void FileBeolvas()
        {
            StreamReader reader = new StreamReader("vas.txt");
            string line;
            int[] id;
 
            while ((line = reader.ReadLine()) != null)
            {
                id = ArrayFromLine(line);
                
                if (CdvEll(id))
                {
                    szamok.Add(new SzemelyiSzam(setNem(id), setDate(id), setSzin(id)));
                }
                else
                {
                    Console.WriteLine($"\tHibás a {line} személyi azonosító!");
                }
            }
        }

        private static int setSzin(int[] id)
        {
            return id[7] * 100 + id[8] * 10 + id[9];
        }

        private static DateTime setDate(int[] id)
        {
            int t = 0;
            switch (id[0])
            {
                case 1: case 2:
                    t = 1900;
                    break;
                case 3: case 4:
                    t = 2000;
                    break;
                default:
                    break;
            }
           t = t + id[1] * 10 + id[2];
            return new DateTime(t, id[3] * 10 + id[4], id[5] * 10 + id[6]);
        }

        private static int setNem(int[] id)
        {
            switch (id[0])
            {
                case 1:
                    return 1;
                case 2:
                    return 0;
                case 3:
                    return 1;
                case 4:
                    return 0;
                default:
                    return 0;
            }
        }

        private static int[] ArrayFromLine(string line)
        {
            int[] id;
            char[] betuk;
            string[] szamok;
            try
            {
                szamok = line.Split('-');
               // Console.WriteLine(szamok[0] + szamok[1] + szamok[2]);
                betuk = (szamok[0] + szamok[1] + szamok[2]).ToCharArray();
                id = new int[betuk.Length];
                for (int i = 0; i < betuk.Length; i++)
                {
                    //id[i] = Convert.ToInt32(betuk[i]);
                    id[i] = betuk[i] - '0';
                   // Console.WriteLine(betuk[i]);
                    //Console.WriteLine(id[i]);
                }

                return id;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Hiba a konvertáláskor. Nem megfelelő a bemeneti lánc!\n"+ex);
                return new int[1] {0};
            }

        }

        private static bool CdvEll(int[] id)
        {
            //Console.WriteLine(id[10]);
            //Console.WriteLine((10 * id[0] + 9 * id[1] + 8 * id[2] + 7 * id[3] + 6 * id[4] + 5 * id[5] + 4 * id[6] + 3 * id[7] + 2 * id[8] + 1 * id[9]) % 11);
            if (id.Length != 11)
            {
                return false;
            }
            if (id[id.Length - 1] == (10 * id[0] + 9 * id[1] + 8 * id[2] + 7 * id[3] + 6 * id[4] + 5 * id[5] + 4 * id[6] + 3 * id[7] + 2 * id[8] + 1 * id[9]) % 11)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        
    }
}
