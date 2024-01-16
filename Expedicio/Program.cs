using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedicio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1.feladat
            List<Uzenet> uzenets = FajlBeolvas();
            
            foreach (var uzenet in uzenets)
            {
                Console.WriteLine(uzenet);
            }

            //2.feladat: legelso legutolso

           
            Console.WriteLine("-------------------\n2. feladat");
            Console.WriteLine($"A legelső adás ({uzenets[0].Napsorszam}) vevője: {uzenets[0].RadiosID}, az utolsó adás ({uzenets[uzenets.Count - 1].Napsorszam}) rögzítője: {uzenets[uzenets.Count-1].RadiosID} ");

            // 3. feladat
            Console.WriteLine("-------------------\n3. feladat");
            List <(int,int)> farkas = FarkasKeres(uzenets);
            foreach (var item in farkas)
            {
                Console.WriteLine($"Feljegyzés napja: {item.Item1}, rádiós sorszáma: {item.Item2}");
            }
            // 4. feladat:
            Console.WriteLine("-------------------\n4. feladat");
            List<(int,int)> stat = NapiStatisztika(uzenets);
            foreach (var item in stat)
            {
                Console.WriteLine($"{item.Item1} nap: {item.Item2}");
            }
            // 5. feladat
            Console.WriteLine("-------------------\n5. feladat");
            Osszefesul(uzenets.Select(x => x.Uzenet_).ToList());
            List<string> adas = new List<string>();
            for (int i = 1; i <= 11; i++)
            {
                List<Uzenet> uzitmp = AzonosNapok(uzenets,i);
                string szoveg = Osszefesul(uzitmp.Select(x => x.Uzenet_).ToList());
                adas.Add(szoveg);
                Console.WriteLine(szoveg);
                uzitmp.Clear();


            }
            FajlbaIras(adas);
            // 6. feadat
            Console.WriteLine("-------------------\n6. feladat");

             // szame();

            // 7. feladat
            Console.WriteLine("-------------------\n7. feladat");

            (int,int) bekertadatok = BekeresRadiosAdas();
            (int, string) valasz = (0,null);
            if (VaneFeljegyzes(bekertadatok, uzenets))
            {
                foreach (var uzi in uzenets)
                {
                    if (uzi.Napsorszam == bekertadatok.Item1 && uzi.RadiosID == bekertadatok.Item2)
                    {
                        valasz = Kolyokadatok(uzi.Uzenet_.ToCharArray());
                    }
                }
                if (valasz.Item1 == 0)
                {
                    Console.WriteLine("Nincs  információ");
                    
                }
                else
                {
                    Console.WriteLine("A megfigyelt egyedek száma:"+valasz.Item1);
                }
            }
            else
            {
                Console.WriteLine("Nincs  ilyen  feljegyzés.");
            }
            
            Console.ReadKey();
        }

        private static bool VaneFeljegyzes((int,int) bekert, List<Uzenet> uzik)
        {

            for (int i = 0; i < uzik.Count; i++)
            {
                if (uzik[i].Napsorszam == bekert.Item1 && uzik[i].RadiosID == bekert.Item2)
                {
                    return true;
                }
            }
            return false;
        }

        private static int szambekeres()
        {
            int szam;
            while (true)
            {
                try
                {
                    szam = Convert.ToInt32(Console.ReadLine());
                    return szam;
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }
            }
            
        }

        private static (int,int) BekeresRadiosAdas()
        {
            int napsorszam, radios;
            Console.WriteLine("Adja meg a nap sorszámát!");
            napsorszam = szambekeres();
            Console.WriteLine("Adja meg a rádióamatőr sorszámát!");
            radios = szambekeres();
            return (napsorszam, radios);
        }

        private static bool isSzam(char betu)
        {
            bool valasz = true;
           
                if (betu < '0' || betu > '9')
                {
                    valasz = false;
                }
           
            return valasz;
        }

        private static (int,string) Kolyokadatok(char[] szo)
        {
            int perPos = 0;
            int spacePos = 0;
            for (int i = 0; i < szo.Length; i++)
            {
                if (szo[i] == '#')
                {
                    return (0, "Nem megállapítható.");
                }
                if (szo[i] == '/')
                {
                    perPos = i; break;
                }
            }
            for (int i = perPos; i < szo.Length; i++)
            {
                if (szo[i] == '#')
                {
                    return (0, "Nem megállapítható.");
                }
                if (szo[i] == ' ')
                {
                    spacePos = i;
                    break;
                }
            }
            char[] chars1 = new char[perPos];
            char[] chars2 = new char[spacePos - perPos];
            for (int i = 0; i < perPos; i++)
            {
                
                chars1[i] = szo[i];
            }
            for (int i = 0; i < chars2.Length; i++)
            {
                chars2[i] = szo[perPos + i+1];
            }
            
            int kifejlett = Convert.ToInt32(new string(chars1));
            int kolyok = Convert.ToInt32(new string(chars2));
            return (kifejlett+kolyok, $"{new string(chars1)},{new string(chars2)}");
            
        }

        private static bool szame(char[] szo)
        {
            bool valasz = true;
            for (int i = 1; i <= szo.Length; i++)
            {
                if (szo[i] < '0' || szo[i] > '9')
                {
                    valasz = false;
                }
            }
            return valasz;
        }

        private static void FajlbaIras(List<string> irandok)
        {
            string fileName = "adas.txt";
            File.WriteAllLines(fileName, irandok);
            Console.WriteLine($"Fájlba írva. ({fileName})");
        }

        private static List<Uzenet> AzonosNapok(List<Uzenet> uzi, int nap)
        {
            List<Uzenet> results = new List<Uzenet>();
            foreach (var item in uzi)
            {
                if (item.Napsorszam == nap)
                {
                    results.Add(item);
                }
            }
            return results;
        }

        private static string Osszefesul(List<string> list)
        {
            string teljesSzov = null;
            char[,] texts = new char[list[0].ToCharArray().Length,list.Count];
           

            for (int row = 0; row < texts.GetLength(1); row++)
            {
                
                char[] tmp = list[row].ToCharArray();
                for (int col = 0; col < texts.GetLength(0); col++)
                {
                    texts[col,row] = tmp[col];

                }
            }
            char[] tmp_ = new char[list[0].Length];
            for (int col = 0; col < texts.GetLength(0); col++)
            {
                for (int row = 0; row < texts.GetLength(1); row++)
                {
                    if (texts[col,row] != '#')
                    {
                        tmp_[col] = texts[col,row];
                    }   
                
                }
                

            }
            teljesSzov = new string(tmp_);
            return teljesSzov;

        }

        private static List<(int,int)> NapiStatisztika(List<Uzenet> uzik)
        {
            List<(int,int)> tupleList = new List<(int,int)> ();

            for (int i = 1; i <= 11; i++)
            {
                int counter = 0;
                foreach (var uzenet in uzik)
                {
                    if (uzenet.Napsorszam == i)
                    {
                        counter++;
                    }
                }
                tupleList.Add((i,counter));
                
            }
            return tupleList;
        }

        private static List<(int,int)> FarkasKeres(List<Uzenet> uzik)
        {
            List<(int, int)> result = new List<(int, int)> ();
            string keresett = "farkas";
            foreach (var uzenet in uzik)
            {
                if (BenneVanE(uzenet.Uzenet_,keresett))
                {
                    result.Add((uzenet.Napsorszam, uzenet.RadiosID));
                }
            }
            return result;
        }

        private static bool BenneVanE(string forras, string keresett)
        {
            if (forras.Contains(keresett))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static List<Uzenet> FajlBeolvas()
        {
            List<Uzenet> uzenets = new List<Uzenet>();
            try
            {
                StreamReader reader = new StreamReader("veetel.txt");
                string line;
                int steps = 1;
                bool elsosor = true;
                string[] elsosortmp =null;
                string uziTmp = null;
                while ((line = reader.ReadLine())!=null)
                {
                    switch (steps)
                    {
                        case 1:
                            elsosortmp = line.Split(' ');
                            steps = 2;
                            break;
                        case 2:
                            uziTmp = line;
                            uzenets.Add(new Uzenet(Convert.ToInt32(elsosortmp[0]), Convert.ToInt32(elsosortmp[1]), uziTmp));

                            steps = 1;
                            break;
                        
                        default:
                            break;
                    }

                }
                reader.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return uzenets;
        }
        
    }
}
