using System.Collections.Immutable;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        ComplexNumber[] tablica = { 
            new ComplexNumber(6, 7),
            new ComplexNumber(1, 2),
            new ComplexNumber(6, 7),
            new ComplexNumber(1, -2),
            new ComplexNumber(-5, 9)};
        Console.WriteLine("Wypisanie liczb zespolonych z wykorzystaniem pętli foreach (tablica):");
        foreach (var l in tablica)
        {
            Console.WriteLine(l);
        }

        Console.WriteLine("\nPosortowana tablica według modula liczby zespolonej:");
        Array.Sort(tablica);
        foreach (var l in tablica)
        {
            Console.WriteLine(l);
        }

        var minliczba = tablica.OrderBy(l => l.Module()).First();
        Console.WriteLine($"\nMinimum tablicy: {minliczba}");

        var maxliczba = tablica.OrderBy(l => l.Module()).Last();
        Console.WriteLine($"\nMaksimum tablicy: {maxliczba}");

        Console.WriteLine("\nLiczby zespolone o ujemnej części urojonej (tablica): ");
        var ujemne = tablica.Where(l => l.Im<0).ToArray();
        foreach(var l in ujemne)
        {
            Console.WriteLine(l);
        }

        List<ComplexNumber> lista = new List<ComplexNumber> { 
            new ComplexNumber(6, 7),
            new ComplexNumber(1, 2),
            new ComplexNumber(6, 7),
            new ComplexNumber(1, -2),
            new ComplexNumber(-5, 9) };

        Console.WriteLine("\nWypisanie liczb zespolonych z wykorzystaniem pętli foreach (lista):");
        foreach (var l in lista)
        {
            Console.WriteLine(l);
        }

        Console.WriteLine("\nPosortowana lista według modula liczby zespolonej:");
        lista.Sort();
        foreach (var l in lista)
        {
            Console.WriteLine(l);
        }

        Console.WriteLine($"\nMinimum listy: {lista.Min()}");
        Console.WriteLine($"\nMaksimum listy: {lista.Max()}");

        Console.WriteLine("\nLiczby zespolone o ujemnej części urojonej (lista): ");
        var list_ujemne = lista.Where(l => l.Im < 0).ToList();
        foreach (var l in list_ujemne)
        {
            Console.WriteLine(l);
        }

        Console.WriteLine("\nUsunięty drugi element z listy:");
        lista.RemoveAt(1);
        foreach(var l in lista)
        {
            Console.WriteLine(l);
        }

        Console.WriteLine("\nUsunięty najmniejszy element z listy:");
        lista.Remove(lista.Min());
        foreach (var l in lista)
        {
            Console.WriteLine(l);
        }

        Console.WriteLine("\nUsunięte wszystkie elementy z listy:");
        lista.Clear();
        foreach (var l in lista)
        {
            Console.WriteLine(l);
        }

        HashSet<ComplexNumber> zbior = new HashSet<ComplexNumber>();
        ComplexNumber z1 = new ComplexNumber(6, 7);
        ComplexNumber z2 = new ComplexNumber(1, 2);
        ComplexNumber z3 = new ComplexNumber(6, 7);
        ComplexNumber z4 = new ComplexNumber(1, -2);
        ComplexNumber z5 = new ComplexNumber(-5, 9);
        
        zbior.Add(z1);
        zbior.Add(z2);
        zbior.Add(z3);
        zbior.Add(z4);
        zbior.Add(z5);

        Console.WriteLine("\nZawartość zbioru: ");
        foreach (var z in zbior)
        {
            Console.WriteLine(z);
        }

        Console.WriteLine($"\nMinimum w zbiorze: {zbior.Min()}");
        Console.WriteLine($"\nMaksimum w zbiorze: {zbior.Max()}");

        var posortowany = zbior.OrderBy(z => z.Module()).ToList(); //Możliwość jest spełniona, bo OrderBy działa na HashSet.
        var negative = zbior.Where(z => z.Im < 0); //Filtrowanie działa tak samo jak w liście i tablicy, ponieważ HashSet jest kolekcją LINQ-ową.

        Dictionary<string, ComplexNumber> slownik = new Dictionary<string, ComplexNumber>();
        slownik.Add("z1", z1);
        slownik.Add("z2", z2);
        slownik.Add("z3", z3);
        slownik.Add("z4", z4);
        slownik.Add("z5", z5);
        Console.WriteLine("\nSłownik:");

        foreach (var kv in slownik)
        {
            Console.WriteLine($"Klucz: {kv.Key} Wartość: {kv.Value}");
        }

        Console.WriteLine("\nWszystkie kluczy ze słownika: ");
        foreach(var klucz in slownik.Keys)
        {
            Console.WriteLine(klucz);
        }

        Console.WriteLine("\nWszystkie wartości ze słownika: ");
        foreach (var wartosc in slownik.Values)
        {
            Console.WriteLine(wartosc);
        }

        if (slownik.ContainsKey("z6"))
            Console.WriteLine("\nSłownik zawiera element o kluczu z6.");
        else
            Console.WriteLine("\nSłownik NIE zawiera elementu o kluczu z6.");

        var minimum = slownik.Values.Min();
        var maksimum = slownik.Values.Max();
        var neg = slownik.Values.Where(v => v.Im < 0);


        slownik.Remove("z3");

        var drugi = slownik.ElementAt(1);
        slownik.Remove(drugi.Key);

        slownik.Clear();
        
    }
}
public interface IModular 
{ 
    public double Module();
}
public class ComplexNumber : ICloneable, IEquatable<ComplexNumber>, IModular, IComparable<ComplexNumber>
{
    private double re; 
    private double im;

    public double Re { get => re; set => re = value; }
    public double Im { get => im; set => im = value; }
    public ComplexNumber(double re, double im) 
    { 
        this.re = re;
        this.im = im;
    }
    public override string ToString() 
    { 
        string sign = im >= 0 ? "+" : "-";
        return $"{re} {sign} {Math.Abs(im)}i";
    }
    public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b) 
        => new ComplexNumber(a.re + b.re, a.im + b.im);
    public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        => new ComplexNumber(a.re - b.re, a.im - b.im);
    public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        => new ComplexNumber(a.re * b.re - a.im * b.im, a.re * b.im + a.im * b.re);
    public static ComplexNumber operator -(ComplexNumber a)
        => new ComplexNumber(a.re, -a.im);
    public object Clone() => new ComplexNumber(re, im);
    public bool Equals(ComplexNumber other)
    { 
        if (other == null) return false;
        return re == other.re && im == other.im;
    }
    public override bool Equals(object obj)
        => obj is ComplexNumber other && Equals(other);
    public override int GetHashCode() 
        => HashCode.Combine(re, im);
    public static bool operator ==(ComplexNumber a, ComplexNumber b)
        => a?.Equals(b) ?? b is null;
    public static bool operator !=(ComplexNumber a, ComplexNumber b)
        => !(a == b);
    public int CompareTo(ComplexNumber other)
    {
        if (other == null) return 1;
        return Module().CompareTo(other.Module());
    }
    public double Module() 
        => Math.Sqrt(re * re + im * im);
}