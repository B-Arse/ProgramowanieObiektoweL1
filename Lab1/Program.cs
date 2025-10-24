using System;

namespace Lab1
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("To jest cwiczenie 1\n");

            Zwierze[] zwierzeta = new Zwierze[3];

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Podaj nazwę {i + 1}-ego zwierzaka:");
                string name = Console.ReadLine();

                Console.WriteLine($"Podaj gatunek {i + 1}-ego zwierzaka:");
                string gatunek = Console.ReadLine();

                Console.WriteLine($"Podaj liczbę nóg {i + 1}-ego zwierzaka:");
                int liczbaNog = Convert.ToInt32(Console.ReadLine());

                zwierzeta[i] = new Zwierze(name, gatunek, liczbaNog);
            }

            Zwierze klon = new Zwierze(zwierzeta[1]);
            Console.WriteLine("\nUtworzono klona drugiego zwierzaka.");

            Console.WriteLine("Podaj nowe imię dla klona: ");
            string noweImie = Console.ReadLine();
            klon.Nazwa=noweImie;

            Console.WriteLine("\n--- Informacje o wszystkich zwierzętach ---");

            foreach (var z in zwierzeta)
            {
                Console.WriteLine($"Nazwa: {z.Nazwa}, gatunek: {z.Gatunek}, liczba nóg: {z.LiczbaNog}");
                z.daj_glos();
            }

            Console.WriteLine("\n --- Dane klona ---");
            Console.WriteLine($"Nazwa: {klon.Nazwa}, gatunek: {klon.Gatunek}, liczba nóg: {klon.LiczbaNog}");
            klon.daj_glos();

            Console.WriteLine("\nŁączna liczba zwierząt: " + Zwierze.LiczbaZwierzatek());
        }
    }
    class Zwierze
    {
        private string nazwa;
        private string gatunek;
        private int liczbaNog;
        private static int liczbaZwierz = 0;

        public string Nazwa 
        {
            get {return nazwa;}
            set {nazwa=value;}
        }
        public string Gatunek 
        {
            get {return gatunek;}
            set {gatunek=value;}
        }
        public int LiczbaNog
        {
            get {return liczbaNog;}
            set {liczbaNog=value;}
        }
        public Zwierze()
        {
            nazwa = "Rex";
            gatunek = "Pies";
            liczbaNog = 4;
            liczbaZwierz++;
        }
        public Zwierze(string nazwa, string gatunek, int liczbaNog)
        {
            this.nazwa = nazwa;
            this.gatunek = gatunek;
            this.liczbaNog = liczbaNog;
            liczbaZwierz++;
        }

        public Zwierze(Zwierze inne)
        {
            this.nazwa = inne.nazwa;
            this.gatunek = inne.gatunek;
            this.liczbaNog = inne.liczbaNog;
            liczbaZwierz++;
        }
        public void daj_glos()
        {
            switch (gatunek)
            {
                case "Kot":
                    Console.WriteLine("Miau");
                    break;
                case "Krowa":
                    Console.WriteLine("Muuu!");
                    break;
                case "Pies":
                    Console.WriteLine("Hau");
                    break;
                case "Swinia":
                    Console.WriteLine("Chrum");
                    break;
                case "Koza":
                    Console.WriteLine("Meee");
                    break;
                default:
                    Console.WriteLine("(brak dźwięku)");
                    break;

            }
        }
        public static int LiczbaZwierzatek()
        {
            return liczbaZwierz;
        }
    }
}