using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _4esFeladat
{
    class Adatok
    {
        public string ora;
        public string perc;
        public int azon;
        public string merre;

        public Adatok(string ora, string perc, int azon, string merre)
        {
            this.ora = ora;
            this.perc = perc;
            this.azon = azon;
            this.merre = merre;
        }
    }
    class DB
    {
        public int azon;
        public int db;

        public DB(int azon, int db)
        {
            this.azon = azon;
            this.db = db;
        }
    }
    class DB2
    {
        public string azon;
        public int db;

        public DB2(string azon, int db)
        {
            this.azon = azon;
            this.db = db;
        }
    }
    class Program
    {
        static List<Adatok> adatok = new List<Adatok>();
        static List<DB> athaladasokSzama = new List<DB>();
        static List<string> mettolMeddig = new List<string>();
        static void Beolvas()
        {
            StreamReader s = new StreamReader("ajto.txt", Encoding.Default);
            while (!s.EndOfStream)
            {
                string sor = s.ReadLine();
                string[] tordel = sor.Split(' ');
                Adatok adat = new Adatok(tordel[0], tordel[1], Convert.ToInt32(tordel[2]), tordel[3]);
                adatok.Add(adat);
            }
            s.Close();
        }
        static void masodikFeladat()
        {
            string elsoBelepes = "", utolsoKilepes = "";
            bool megal = false;
            foreach (var item in adatok)
            {
                if (item.merre == "be" && megal == false)
                {
                    elsoBelepes = Convert.ToString(item.azon);
                    megal = true;
                }
                if (item.merre == "ki")
                    utolsoKilepes = Convert.ToString(item.azon);
            }
            Console.WriteLine("Az első belépő: {0}\nAz utolsó kilépő: {1}",elsoBelepes,utolsoKilepes);
        }
        static void harmadikFeladat()
        {
            bool uj = false;
            foreach (var item in adatok)
            {
                DB alap = new DB(item.azon, 0);
                foreach (var item2 in athaladasokSzama)
                {
                    if (item.azon == item2.azon)
                    {
                        uj = true;
                    }
                }
                if (uj == false)
                athaladasokSzama.Add(alap);
                uj = false;
            }
            athaladasokSzama = athaladasokSzama.OrderBy(x => x.azon).ToList();
            foreach (var item in athaladasokSzama)
            {
                foreach (var item2 in adatok)
                {
                    if (item.azon == item2.azon)
                    {
                        item.db++;
                    }
                }
            }
            StreamWriter w = new StreamWriter("athaladas.txt");
            foreach (var item in athaladasokSzama)
            {
                //Console.WriteLine("Azonosító: {0} DB: {1}",item.azon,item.db);
                w.WriteLine("{0} {1}", item.azon, item.db);
            }
            w.Close();
        }
        static void negyedikFeladat()
        {
            Console.Write("A végén a társalgóban voltak:");
            foreach (var item in athaladasokSzama)
            {
                if (item.db%2!=0)
                {
                    Console.Write("   {0}", item.azon);
                }
            }
        }
        static void otodikFeladat()
        {
            bool mehet = true;
            List<DB2> percek = new List<DB2>();
            foreach (var item in adatok)
            {
                foreach (var item2 in percek)
                {
                    string ido = item.ora + " " + item.perc;
                    if (ido == Convert.ToString(item2.azon))
                    {
                        mehet = false;
                    }
                }
                if (mehet == true)
                {
                    DB2 adat = new DB2(item.ora + " " + item.perc, 1);
                    percek.Add(adat);
                }
                mehet = true;

            }
            foreach (var item in percek)
            {
                foreach (var item2 in adatok)
                {
                    string ido = item2.ora + " " + item2.perc;
                    if (item.azon == ido && item2.merre == "be")
                    {
                        item.db++;
                    }
                }
            }
            int max = 0;
            string maxint = "";
            foreach (var item in percek)
            {
                if (max < item.db)
                {
                    max = item.db;
                    maxint = item.azon;
                }
            }
            Console.WriteLine("Például {0}-kor voltak a legtöbben a társalgóban.", maxint);
        }
        static void hetedikFeladat(int azonosito)
        {
            string mettolmeddig = "";
            foreach (var item in adatok)
            {
                if (item.azon == azonosito)
                {
                    mettolmeddig += item.ora + ":" + item.perc;
                    mettolMeddig.Add(mettolmeddig);
                    mettolmeddig = "";
                }
            }
            int i = 0;
            foreach (var item in mettolMeddig)
            {
                if (i % 2 != 0)
                {
                    Console.WriteLine("{0}",item);
                }
                else
                {
                    Console.Write("{0}-", item);
                }
                i++;
            }
        }
        static void nyolcadikFeladat(int azonosito)
        {
            DateTime zaro_nap = DateTime.Today;
            DateTime kezdo_nap = DateTime.Today;
            int i = 0;
            double percek = 0;
            string ora = "", perc = "";

            foreach (var item in mettolMeddig)
            {
                if (Char.IsDigit(item[1]))
                {
                    ora = Convert.ToString(item[0]) + Convert.ToString(item[1]);
                }
                else
                {
                    ora = Convert.ToString(item[0]);
                }


                if (item[item.Length - 2] != ':')
                {
                    perc = Convert.ToString(item[item.Length - 2]) + Convert.ToString(item[item.Length-1]);
                }
                else
                {
                    perc = Convert.ToString(item[item.Length-1]);
                }


                if (i % 2 == 0)
                {
                    kezdo_nap = new DateTime(2020, 02, 10, Convert.ToInt32(ora), Convert.ToInt32(perc), 0);
                }
                else
                {
                    zaro_nap = new DateTime(2020, 02, 10, Convert.ToInt32(ora), Convert.ToInt32(perc), 0);
                    TimeSpan kulonbseg = zaro_nap - kezdo_nap;
                    percek += kulonbseg.Minutes;
                }
                i++;
            }
            if (mettolMeddig.Count %2 == 0)
            Console.WriteLine("A(z) {0}. személy összesen {1} percet volt bent, a megfigyelés végén nem volt a társalgóban.", azonosito, percek);
            else
                Console.WriteLine("A(z) {0}. személy összesen {1} percet volt bent, a megfigyelés végén nem volt a társalgóban.", azonosito, percek);

        }
        static void Main(string[] args)
        {
            Beolvas();
            Console.WriteLine("2. feladat");
            masodikFeladat();
            Console.WriteLine();
            harmadikFeladat();
            Console.WriteLine("4. feladat");
            negyedikFeladat();
            Console.WriteLine();
            Console.WriteLine("5. feladat");
            otodikFeladat();
            Console.WriteLine();
            Console.WriteLine("6. feladat");
            Console.Write("Adja meg a személy azonosítóját: ");
            int azonosito = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("7. feladat");
            hetedikFeladat(azonosito);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("8. feladat");
            nyolcadikFeladat(azonosito);
            Console.ReadLine();
        }
    }
}