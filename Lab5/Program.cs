using System;
using System.IO;

internal class Program
{
    static void Zapyta()
    {
        string sciezkaPliku = "dane_uzytkownika.txt";

        while (true)
        {
            Console.WriteLine("Podaj swoje imię: ");
            string imie = Console.ReadLine();

            Console.WriteLine("Podaj swoje nazwisko: ");
            string nazwisko = Console.ReadLine();

            Console.WriteLine("Podaj swój wiek: ");
            string wiek = Console.ReadLine();

            int wiekLiczba;
            while (!int.TryParse(wiek, out wiekLiczba) || wiekLiczba < 0)
            {
                Console.WriteLine("Podaj poprawny wiek (liczba większa od 0): ");
                wiek = Console.ReadLine();
            }

            using (StreamWriter writer = new StreamWriter(sciezkaPliku, true))
            {
                writer.WriteLine($"Imię: {imie}");
                writer.WriteLine($"Nazwisko: {nazwisko}");
                writer.WriteLine($"Wiek: {wiekLiczba}");
            }

            Console.WriteLine("Dane zostały zapisane do pliku.");

            Console.WriteLine("Czy chcesz dodać kolejne dane? (tak/nie): ");
            string odpowiedz = Console.ReadLine();
            if (odpowiedz.ToLower() != "tak")
            {
                break; 
            }
        }

        Console.WriteLine("Zakończono zapisywanie danych.");
    }

    private static void Main(string[] args)
    {
        Zapyta(); 
    }
}
