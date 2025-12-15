using Microsoft.Data.SqlClient;
public class Program
{
    public static void Main()
    {
        string connectionString =
        "Data Source=10.200.2.28;" + 
        "Initial Catalog=studenci_71441;" + 
       "Integrated Security=True;" +
        "Encrypt=True;" +
        "TrustServerCertificate=True";
        try
        {
            using SqlConnection connection = new
           SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Połączono z bazą.");
        }
        catch (Exception exc)
        {
            Console.WriteLine("Wystąpił błąd: " + exc);
        }

    }
}

public class Student
{
    public int StudentId { get; set; }
    public string Imie { get; set; } = "";
    public string Nazwisko { get; set; } = "";
    public List<Ocena> Oceny { get; set; } = new();
}
public class Ocena
{
    public int OcenaId { get; set; }
    public double Wartosc { get; set; }
    public string Przedmiot { get; set; } = "";
    public int StudentId { get; set; }
}
