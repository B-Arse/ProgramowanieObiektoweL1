using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Projekt_BookManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            BookRepository repository = new BookRepository();
            repository.LoadFromFile();

            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("=== SYSTEM ZARZĄDZANIA KSIĄŻKAMI ===");
                Console.WriteLine("1. Dodaj książkę");
                Console.WriteLine("2. Wyświetl książki");
                Console.WriteLine("3. Edytuj książkę");
                Console.WriteLine("4. Usuń książkę");
                Console.WriteLine("5. Zakończ program");
                Console.Write("Wybierz opcję: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        repository.AddBook();
                        break;
                    case "2":
                        repository.DisplayBooks();
                        break;
                    case "3":
                        repository.UpdateBook();
                        break;
                    case "4":
                        repository.DeleteBook();
                        break;
                    case "5":
                        repository.SaveToFile();
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór. Naciśnij dowolny klawisz...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }

    class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }

        public string ToFileString()
        {
            return $"{Id}|{Title}|{Author}|{Year}";
        }

        public static Book FromFileString(string line)
        {
            string[] parts = line.Split('|');
            return new Book
            {
                Id = int.Parse(parts[0]),
                Title = parts[1],
                Author = parts[2],
                Year = int.Parse(parts[3])
            };
        }

        public override string ToString()
        {
            return $"{Id}. {Title} – {Author} ({Year})";
        }
    }

    class BookRepository
    {
        private List<Book> books = new List<Book>();
        private const string FileName = "books.txt";

        public void AddBook()
        {
            Console.Clear();
            Console.WriteLine("=== DODAWANIE KSIĄŻKI ===");

            Book book = new Book
            {
                Id = books.Count == 0 ? 1 : books.Max(b => b.Id) + 1,
                Title = ReadNonEmptyString("Podaj tytuł: "),
                Author = ReadNonEmptyString("Podaj autora: "),
                Year = ReadYear("Podaj rok wydania: ")
            };

            books.Add(book);
            SaveToFile();

            Console.WriteLine("Książka została dodana.");
            Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do menu...");
            Console.ReadKey();
        }

        public void DisplayBooks(bool pause = true)
        {
            Console.Clear();
            Console.WriteLine("=== LISTA KSIĄŻEK ===");

            if (books.Count == 0)
            {
                Console.WriteLine("Brak zapisanych książek.");
            }
            else
            {
                foreach (Book book in books)
                {
                    Console.WriteLine(book);
                }
            }

            if (pause)
            {
                Console.WriteLine();
                Console.WriteLine("Naciśnij Enter, aby wrócić do menu...");
                Console.ReadLine();
            }
        }

        public void UpdateBook()
        {
            Console.Clear();

            if (books.Count == 0)
            {
                Console.WriteLine("Brak książek do edycji.");
                Console.WriteLine("Najpierw dodaj książkę.");
                Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do menu...");
                Console.ReadKey();
                return; // wraca do menu
            }

            DisplayBooks(false);

            int id = ReadInt("Podaj ID książki do edycji: ");
            Book book = books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                Console.WriteLine("Nie znaleziono książki.");
                Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do menu...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("=== EDYCJA KSIĄŻKI ===");
            book.Title = ReadNonEmptyString("Nowy tytuł: ");
            book.Author = ReadNonEmptyString("Nowy autor: ");
            book.Year = ReadYear("Nowy rok wydania: ");

            SaveToFile();
            Console.WriteLine("Dane książki zostały zaktualizowane.");
            Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do menu...");
            Console.ReadKey();
        }

        public void DeleteBook()
        {
            Console.Clear();

            if (books.Count == 0)
            {
                Console.WriteLine("Brak książek do usunięcia.");
                Console.WriteLine("Najpierw dodaj książkę.");
                Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do menu...");
                Console.ReadKey();
                return;
            }

            DisplayBooks(false);

            int id = ReadInt("Podaj ID książki do usunięcia: ");
            Book book = books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                Console.WriteLine("Nie znaleziono książki.");
                Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do menu...");
                Console.ReadKey();
                return;
            }

            books.Remove(book);
            SaveToFile();

            Console.WriteLine("Książka została usunięta.");
            Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do menu...");
            Console.ReadKey();
        }

        public void SaveToFile()
        {
            File.WriteAllLines(FileName, books.Select(b => b.ToFileString()));
        }

        public void LoadFromFile()
        {
            if (!File.Exists(FileName))
                return;

            foreach (var line in File.ReadAllLines(FileName))
            {
                try
                {
                    books.Add(Book.FromFileString(line));
                }
                catch
                {
                    // ignoruj błędne linie
                }
            }
        }

        // ===== WALIDACJA DANYCH =====

        private string ReadNonEmptyString(string message)
        {
            string input;
            do
            {
                Console.Write(message);
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }

        private int ReadInt(string message)
        {
            int value;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out value))
                    return value;

                Console.WriteLine("Błąd: należy podać liczbę całkowitą.");
            }
        }

        private int ReadYear(string message)
        {
            int year;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out year) &&
                    year > 0 &&
                    year <= DateTime.Now.Year)
                {
                    return year;
                }

                Console.WriteLine("Błąd: podaj poprawny rok.");
            }
        }
    }
}
