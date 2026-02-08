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

            WyswietlStudentow(connection);
            WyswietlStudentaPoId(connection, 1);

            var studenci = PobierzStudentowZOcenami(connection);
            WypiszStudentowZOcenami(studenci);

            DodajStudenta(connection, new Student { Imie = "Adam", Nazwisko = "Nowak" });

            DodajOcene(connection, new Ocena
            {
                StudentId = 1,
                Przedmiot = "Matematyka",
                Wartosc = 4.5
            });

            UsunOcenyZGeografii(connection);
            AktualizujOcene(connection, 1, 5.0);
        }
        catch (Exception exc)
        {
            Console.WriteLine("Wystąpił błąd: " + exc);
        }

    }
    //Zadanie 4
    public static void WyswietlStudentow(SqlConnection connection)
    {
        string sql = "SELECT student_id, imie, nazwisko FROM student";

        using SqlCommand cmd = new SqlCommand(sql, connection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader["student_id"]} {reader["imie"]} {reader["nazwisko"]}");
        }
    }
    //Zadanie 5
    public static void WyswietlStudentaPoId(SqlConnection connection, int id)
    {
        string sql = "SELECT imie, nazwisko FROM student WHERE student_id = @id";

        using SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@id", id);

        using SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Console.WriteLine($"{reader["imie"]} {reader["nazwisko"]}");
        }
        else
        {
            Console.WriteLine("Nie znaleziono studenta.");
        }
    }
    //Zadanie 6
    public static List<Student> PobierzStudentowZOcenami(SqlConnection connection)
    {
        List<Student> studenci = new();

        string sql = @"
        SELECT s.student_id, s.imie, s.nazwisko,
               o.ocena_id, o.wartosc, o.przedmiot
        FROM student s
        LEFT JOIN ocena o ON s.student_id = o.student_id
        ORDER BY s.student_id";

        using SqlCommand cmd = new SqlCommand(sql, connection);
        using SqlDataReader reader = cmd.ExecuteReader();

        Student? current = null;

        while (reader.Read())
        {
            int studentId = (int)reader["student_id"];

            if (current == null || current.StudentId != studentId)
            {
                current = new Student
                {
                    StudentId = studentId,
                    Imie = reader["imie"].ToString()!,
                    Nazwisko = reader["nazwisko"].ToString()!
                };
                studenci.Add(current);
            }

            if (reader["ocena_id"] != DBNull.Value)
            {
                current.Oceny.Add(new Ocena
                {
                    OcenaId = (int)reader["ocena_id"],
                    Wartosc = (double)reader["wartosc"],
                    Przedmiot = reader["przedmiot"].ToString()!,
                    StudentId = studentId
                });
            }
        }

        return studenci;
    }

    public static void WypiszStudentowZOcenami(List<Student> studenci)
    {
        foreach (var s in studenci)
        {
            Console.WriteLine($"{s.StudentId} {s.Imie} {s.Nazwisko}");
            foreach (var o in s.Oceny)
            {
                Console.WriteLine($"   {o.Przedmiot}: {o.Wartosc}");
            }
        }
    }
    //Zadanie 7
    public static void DodajStudenta(SqlConnection connection, Student student)
    {
        string sql = "INSERT INTO student(imie, nazwisko) VALUES (@imie, @nazwisko)";

        using SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@imie", student.Imie);
        cmd.Parameters.AddWithValue("@nazwisko", student.Nazwisko);

        cmd.ExecuteNonQuery();
    }
    //Zadanie 8
    public static bool CzyPoprawnaOcena(double ocena)
    {
        if (ocena < 2 || ocena > 5) return false;
        if (ocena == 2.5) return false;
        return ocena * 2 == Math.Floor(ocena * 2);
    }

    public static void DodajOcene(SqlConnection connection, Ocena ocena)
    {
        if (!CzyPoprawnaOcena(ocena.Wartosc))
        {
            Console.WriteLine("Niepoprawna ocena.");
            return;
        }

        string sql = @"INSERT INTO ocena(wartosc, przedmiot, student_id)
                   VALUES (@w, @p, @sid)";

        using SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@w", ocena.Wartosc);
        cmd.Parameters.AddWithValue("@p", ocena.Przedmiot);
        cmd.Parameters.AddWithValue("@sid", ocena.StudentId);

        cmd.ExecuteNonQuery();
    }
    //Zadanie 9
    public static void UsunOcenyZGeografii(SqlConnection connection)
    {
        string sql = "DELETE FROM ocena WHERE przedmiot = 'geografia'";
        using SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.ExecuteNonQuery();
    }
    //Zadanie 10
    public static void AktualizujOcene(SqlConnection connection, int ocenaId, double nowaWartosc)
    {
        if (!CzyPoprawnaOcena(nowaWartosc))
        {
            Console.WriteLine("Niepoprawna ocena.");
            return;
        }

        string sql = "UPDATE ocena SET wartosc = @w WHERE ocena_id = @id";

        using SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@w", nowaWartosc);
        cmd.Parameters.AddWithValue("@id", ocenaId);

        cmd.ExecuteNonQuery();
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
