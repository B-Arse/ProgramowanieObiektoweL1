internal class Program
{
    private static void Main(string[] args)
    {
        ComplexNumber cn1 = new ComplexNumber(6, -7);
        ComplexNumber cn2 = new ComplexNumber(3, 5);
        ComplexNumber cn3 = (ComplexNumber)cn2.Clone();
        ComplexNumber cn4 = -cn1;
        ComplexNumber suma = cn1 + cn2;
        ComplexNumber roz = cn1 - cn2;
        ComplexNumber il = cn1 * cn2;

        Console.WriteLine("Pierwsza liczba: " + cn1.ToString());
        Console.WriteLine("Druga liczba: " + cn2.ToString());
        Console.WriteLine("Trzecia liczba (Użyta metoda Clone()): "+ cn3.ToString());
        Console.WriteLine("Suma pierwszych dwóch liczb: " + suma);
        Console.WriteLine("Różnica pierwszych dwóch liczb: " + roz);
        Console.WriteLine("Iloczyn pierwszych dwóch liczb: " + il);
        
        Console.WriteLine(cn1.Equals(cn2));
        Console.WriteLine(cn2.Equals(cn3));
        Console.WriteLine(cn3 == cn1);
        Console.WriteLine(cn2 != cn3);
        Console.WriteLine("Sprzężenie pierwszej liczby: " + cn4);
        Console.WriteLine("Modul pierwszej liczby: " + cn1.Module());
    }
}
class ComplexNumber : ICloneable, IEquatable<ComplexNumber>, IModular
{
    private double re;
    private double im;
    public double Re
    {
        get { return re; }
        set { re = value; }
    }

    public double Im
    {
        get { return im; }
        set { im = value; }
    }
    public ComplexNumber(double re, double im)
    {
        Re = re;
        Im = im;
    }
    public override string ToString()
    {
        if (Im < 0)
        {
            return $"{Re} - {-1*Im}i";
        }
        else {
            return $"{Re} + {Im}i";
        }
    }
    public static ComplexNumber operator + (ComplexNumber a, ComplexNumber b) 
    {
        return new ComplexNumber(a.Re+b.Re, a.Im+b.Im);
    }
    public static ComplexNumber operator - (ComplexNumber a, ComplexNumber b) 
    {
        return new ComplexNumber(a.Re-b.Re, a.Im-b.Im);
    }
    public static ComplexNumber operator * (ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.Re*b.Re-a.Im*b.Im, a.Re*b.Im+a.Im*b.Re);
    }
    public object Clone()
    {
        return new ComplexNumber(Re,Im);
    }
    public bool Equals(ComplexNumber other)
    {
        if (other is null)
            return false;

        return Re == other.Re && Im == other.Im;
    }

    public override bool Equals(object obj)
    {
        if (obj is ComplexNumber other)
            return Equals(other);

        return false;
    }


    public override int GetHashCode()
    {
        return HashCode.Combine(Re, Im);
    }

    public static bool operator ==(ComplexNumber a, ComplexNumber b)
    {
        if (ReferenceEquals(a, b))
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(ComplexNumber a, ComplexNumber b)
    {
        return !(a == b);
    }
    public static ComplexNumber operator -(ComplexNumber a)
    {
        return new ComplexNumber(a.Re, -1*a.Im);
    }
    public double Module()
    {
        return Math.Sqrt(Math.Pow(Re,2)+ Math.Pow(Im, 2));
    }
}
interface IModular 
{
    public double Module();
}
