using System;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

internal class Program
{
    static void Zadanie2()
    {
        string sciezkaPliku = "tekst.txt";

        using (StreamWriter writer = new StreamWriter(sciezkaPliku))
        {
            while (true)
            {
                Console.Write("Podaj dowolny tekst: ");
                string tekst = Console.ReadLine();
                writer.WriteLine(tekst);

                Console.Write("Czy chcesz podać kolejny tekst? (tak/nie): ");
                string odp = Console.ReadLine().ToLower();

                if (odp != "tak")
                    break;
            }
        }

        Console.WriteLine("Zakończono zapisywanie danych.\n");
    }

    static void Zadanie3()
    {
        string sciezkaPliku = "tekst.txt";

        if (!File.Exists(sciezkaPliku))
        {
            Console.WriteLine("Plik nie istnieje!");
            return;
        }

        Console.WriteLine("Zawartość pliku:");
        using (StreamReader reader = new StreamReader(sciezkaPliku))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }

    static void Zadanie4()
    {
        string sciezkaPliku = "tekst.txt";

        using (StreamWriter writer = new StreamWriter(sciezkaPliku, append: true))
        {
            while (true)
            {
                Console.Write("Podaj tekst do dopisania: ");
                string tekst = Console.ReadLine();
                writer.WriteLine(tekst);

                Console.Write("Czy chcesz dopisać kolejny tekst? (tak/nie): ");
                string odp = Console.ReadLine().ToLower();

                if (odp != "tak")
                    break;
            }
        }

        Console.WriteLine("Zakończono dopisywanie danych.\n");
    }
    static void Zadanie6()
    {
        string sciezka = "StudenciJSON.txt";
        List<Student> studenci = new List<Student>();
        Student s1 = new Student
        {
            Imie = "Andrzej",
            Nazwisko = "Kiebala",
            Oceny = new List<int> {3, 4, 5 }
        };
        Student s2 = new Student 
        {
            Imie = "Anna",
            Nazwisko = "Furman",
            Oceny = new List<int> {4, 3, 5 }
        };
        Student s3 = new Student 
        {
            Imie = "Barbara",
            Nazwisko = "Mrozowicz",
            Oceny = new List<int> { 4, 5, 3}
        };
        studenci.Add(s1);
        studenci.Add(s2);
        studenci.Add(s3);
        string json = JsonSerializer.Serialize(studenci);
        using(StreamWriter pisz = new StreamWriter(sciezka))
        {
            pisz.WriteLine(json);
        }

        
    }
    static void Zadanie7()
    {
        string sciezka = "StudenciJSON.txt"; 

        if (!File.Exists(sciezka))
        {
            Console.WriteLine("Plik nie istnieje!");
            return;
        }

        string json;
        using (StreamReader reader = new StreamReader(sciezka))
        {
            json = reader.ReadToEnd();
        }

        var studenci = JsonSerializer.Deserialize<List<Student>>(json);

        if (studenci == null)
        {
            Console.WriteLine("Brak danych do wyświetlenia.");
            return;
        }

        foreach (var s in studenci)
        {
            Console.WriteLine($"Imię: {s.Imie}");
            Console.WriteLine($"Nazwisko: {s.Nazwisko}");
            Console.WriteLine("Oceny: " + string.Join(", ", s.Oceny));
            Console.WriteLine(); 
        }
    }
    static void Zadanie8()
    {
        string sciezka = "StudenciXML.xml";

        List<Student> studenci = new List<Student>();

        Student s1 = new Student
        {
            Imie = "Andrzej",
            Nazwisko = "Kiebala",
            Oceny = new List<int> { 3, 4, 5 }
        };
        Student s2 = new Student
        {
            Imie = "Anna",
            Nazwisko = "Furman",
            Oceny = new List<int> { 4, 3, 5 }
        };
        Student s3 = new Student
        {
            Imie = "Barbara",
            Nazwisko = "Mrozowicz",
            Oceny = new List<int> { 4, 5, 3 }
        };

        studenci.Add(s1);
        studenci.Add(s2);
        studenci.Add(s3);

        XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));

        using (StreamWriter writer = new StreamWriter(sciezka))
        {
            serializer.Serialize(writer, studenci);
        }

        Console.WriteLine("Zapisano studentów do pliku XML.\n");
    }
    static void Zadanie9()
    {
        string sciezka = "StudenciXML.xml";

        if (!File.Exists(sciezka))
        {
            Console.WriteLine("Plik XML nie istnieje!");
            return;
        }

        XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
        List<Student> studenci;

        using (StreamReader reader = new StreamReader(sciezka))
        {
            studenci = (List<Student>)serializer.Deserialize(reader);
        }

        Console.WriteLine("Odczytano studentów z pliku XML:\n");

        foreach (var s in studenci)
        {
            Console.WriteLine($"Imię: {s.Imie}");
            Console.WriteLine($"Nazwisko: {s.Nazwisko}");
            Console.WriteLine("Oceny: " + string.Join(", ", s.Oceny));
            Console.WriteLine();
        }
    }
    static void Zadanie10()
    {
        string sciezka = "Iris.csv";

        if (!File.Exists(sciezka))
        {
            Console.WriteLine("Plik Iris.csv nie istnieje!");
            return;
        }

        string[] linie = File.ReadAllLines(sciezka);

        Console.WriteLine("Zawartość pliku CSV:\n");

        foreach (string linia in linie)
        {
            Console.WriteLine(linia);
        }

        Console.WriteLine();
    }
    static void Zadanie11()
    {
        string sciezka = "iris.csv";

        if (!File.Exists(sciezka))
        {
            Console.WriteLine("Plik iris.csv nie istnieje!");
            return;
        }

        string[] linie = File.ReadAllLines(sciezka);

        if (linie.Length <= 1)
        {
            Console.WriteLine("Plik nie zawiera danych!");
            return;
        }

        string[] naglowki = linie[0].Split(',');
        int kolumny = naglowki.Length;

        double[] suma = new double[kolumny];
        int[] licznik = new int[kolumny];

        for (int i = 1; i < linie.Length; i++)
        {
            string[] pola = linie[i].Split(',');

            if (pola.Length == 0 || (pola.Length == 1 && string.IsNullOrWhiteSpace(pola[0])))
                continue;

            for (int j = 0; j < kolumny; j++)
            {
                if (j >= pola.Length)
                    continue;

                string pole = pola[j]?.Trim() ?? string.Empty;

                if (pole.Length >= 2 && pole.StartsWith("\"") && pole.EndsWith("\""))
                {
                    pole = pole.Substring(1, pole.Length - 2).Replace("\"\"", "\"");
                }

                if (string.IsNullOrEmpty(pole))
                    continue;

                if (double.TryParse(pole, NumberStyles.Any, CultureInfo.InvariantCulture, out double val) ||
                    double.TryParse(pole, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    suma[j] += val;
                    licznik[j]++;
                }
                
            }
        }

        Console.WriteLine("Średnie wartości kolumn numerycznych:\n");

        for (int j = 0; j < kolumny; j++)
        {
            if (licznik[j] > 0)
            {
                double srednia = suma[j] / licznik[j];
                Console.WriteLine($"{naglowki[j]}: {srednia:F3}");
            }
        }

        Console.WriteLine();
    }
    static void Zadanie12()
    {
        string sciezka = "iris.csv";
        string wyjscie = "iris_filtered.csv";

        if (!File.Exists(sciezka))
        {
            Console.WriteLine("Plik iris.csv nie istnieje!");
            return;
        }

        string[] linie = File.ReadAllLines(sciezka);

        if (linie.Length <= 1)
        {
            Console.WriteLine("Plik nie zawiera danych!");
            return;
        }

        string[] naglowki = linie[0].Split(',');

        int idx_len = Array.IndexOf(naglowki, "sepal length");
        int idx_wid = Array.IndexOf(naglowki, "sepal width");
        int idx_class = Array.IndexOf(naglowki, "class");

        if (idx_len < 0 || idx_wid < 0 || idx_class < 0)
        {
            Console.WriteLine("Nie znaleziono wymaganych kolumn!");
            return;
        }

        List<string> wynik = new List<string>();

        wynik.Add("sepal length,sepal width,class");

        for (int i = 1; i < linie.Length; i++)
        {
            string[] pola = linie[i].Split(',');

            if (pola.Length < naglowki.Length)
                continue; 

            if (double.TryParse(
                pola[idx_len],
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out double sepalLen))
            {
                if (sepalLen < 5.0)
                {
                    string nowaLinia = $"{pola[idx_len]},{pola[idx_wid]},{pola[idx_class]}";
                    wynik.Add(nowaLinia);
                }
            }
        }

        File.WriteAllLines(wyjscie, wynik);

        Console.WriteLine("Utworzono plik iris_filtered.csv z przefiltrowanymi danymi.\n");
    }




    private static void Main(string[] args)
    {
        /*
        Zadanie2();
        Zadanie3();
        while (true)
        {
            Console.Write("Czy chcesz coś dopisać do pliku? (tak/nie): ");
            string odp = Console.ReadLine().ToLower();
            if (odp != "tak")
            {
                break;
            }
            else
            {
                Zadanie4();
            }
        }
        Zadanie6();
        Zadanie7();
        Zadanie8();
        Zadanie9();
        Zadanie10();
        Zadanie11();
        Zadanie12();
        */

    }
}
public class Student
{
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public List<int> Oceny { get; set; }
}
