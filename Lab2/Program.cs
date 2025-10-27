internal class Program
{
    private static void Main(string[] args)
    {
        Zwierze zwierze = new Zwierze("");
        Pies pies = new Pies("Oddy");
        Kot kot = new Kot("Marik");
        Waz waz = new Waz("Nagini");

   
        powiedz_cos(zwierze);
        powiedz_cos(pies);
        powiedz_cos(kot);
        powiedz_cos(waz);
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
