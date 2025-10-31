internal class Program
{
    private static void Main(string[] args)
    {
        Zwierze zwierze = new Zwierze("");
        Pies pies = new Pies("Oddy");
        Kot kot = new Kot("Marik");
        Waz waz = new Waz("Nagini");
        Piekarz piekarz = new Piekarz();
        //Pracownik pracownik = new Pracownik(); Błąd kompilacji — klasa abstrakcyjna!
        A a = new A();
        B b = new B();
        C c = new C();


        powiedz_cos(zwierze);
        powiedz_cos(pies);
        powiedz_cos(kot);
        powiedz_cos(waz);
        piekarz.Pracuj();
}
    static void powiedz_cos(Zwierze zwierze)
    {
        zwierze.daj_glos();
        Console.WriteLine($"Typ obiektu: {zwierze.GetType().Name}");
    }
}
class Zwierze
{
    protected string nazwa;
    public Zwierze(string nazwa)
    {
        this.nazwa = nazwa;
    }
    public virtual void daj_glos()
    {
        Console.WriteLine("...");
    }
  
}
class Pies: Zwierze
{
    public Pies(string nazwa) : base(nazwa)
    {

    }
    public override void daj_glos()
    {
        Console.WriteLine($"{nazwa} robi woof woof!");
    }
}
class Kot: Zwierze
{
    public Kot(string nazwa) : base(nazwa)
    {

    }
    public override void daj_glos()
    {
        Console.WriteLine($"{nazwa} robi miau miau!");
    }
}
class Waz: Zwierze
{
    public Waz(string nazwa) : base(nazwa)
    {

    }
    public override void daj_glos()
    {
        Console.WriteLine($"{nazwa} robi ssssssss!");
    }
}
abstract class Pracownik
{
    public abstract void Pracuj();
}
class Piekarz : Pracownik
{
    public override void Pracuj()
    {
        Console.WriteLine("Trwa pieczenie...");
    }
}
class A
{
    public A()
    {
        Console.WriteLine("To jest konstruktor A");
    }
}
class B : A
{
    public B() : base()
    {
        Console.WriteLine("To jest konstruktor B");
    }
}
class C : B
{
    public C() : base()  
    {
        Console.WriteLine("To jest konstruktor C");
    }
}
