using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sashka
{
    enum Genre { Детектив, Наука, История, Фантастика, Другое }

    class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public Genre Genres { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}\n" +
                   $"Название: {Title}\n" +
                   $"Автор: {Author}\n" +
                   $"Жанр: {Genres}\n" +
                   $"Год издания: {Year}\n" +
                   $"Цена: {Price}\n";
        }
    }

    class Program
    {
        static void Main()
        {
            List<Book> book = new List<Book>();
            int id = 1;

            book.Add(new Book { Id = id++, Title = "Война и мир", Author = "Толстой", Genres = Genre.Детектив, Year = 1869, Price = 1500 });
            book.Add(new Book { Id = id++, Title = "Преступление и наказание", Author = "Достоевский", Genres = Genre.Детектив, Year = 1866, Price = 1200 });
            book.Add(new Book { Id = id++, Title = "Основы основ", Author = "Дима Кизим", Genres = Genre.Наука, Year = 1988, Price = 322 });
            book.Add(new Book { Id = id++, Title = "Биография", Author = "Игорь Строков", Genres = Genre.История, Year = 1954, Price = 99999999999999 });
            book.Add(new Book { Id = id++, Title = "Основы программирования", Author = "Сережа Козлов", Genres = Genre.Фантастика, Year = 2008, Price = 67 });

            bool work = true;
            while (work)
            {
                Console.WriteLine("Выберите команду:");
                Console.WriteLine("1 - Добавить книгу");
                Console.WriteLine("2 - Удалить книгу по ID");
                Console.WriteLine("3 - Найти книги");
                Console.WriteLine("4 - Сортировать по названию");
                Console.WriteLine("5 - Сортировать по году");
                Console.WriteLine("6 - Самая дорогая и самая дешёвая книга");
                Console.WriteLine("7 - Группировать по авторам");
                Console.WriteLine("8 - Показать все книги");
                Console.WriteLine("0 - Выход");

                string command = Console.ReadLine();

                //новая книжка
                if (command == "1")
                {
                    Console.WriteLine("Введите название");
                    string name = Console.ReadLine();
                    if (name == "")
                    {
                        Console.WriteLine("название не может быть пустым");
                        continue;
                    }

                    Console.WriteLine("Введите цену");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
                    {
                        Console.WriteLine("некоректная цена");
                    }

                    Console.WriteLine("Введите автора");
                    string author = Console.ReadLine();
                    if (author == "")
                    {
                        Console.WriteLine("название не может быть пустым");
                        continue;
                    }

                    Console.WriteLine("Введите год");
                    if (!int.TryParse(Console.ReadLine(), out int year))
                    {
                        Console.WriteLine("Введите целое число");
                    }

                    Console.WriteLine("Выберите жанр:");
                    Console.WriteLine("0 - Детектив");
                    Console.WriteLine("1 - Наука");
                    Console.WriteLine("2 - История");
                    Console.WriteLine("3 - Фантастика");
                    Console.WriteLine("4 - Другое");

                    if (!int.TryParse(Console.ReadLine(), out int genreId) || genreId < 0 || genreId > 4)
                    {
                        Console.WriteLine("Некорректный жанр");
                        continue;
                    }

                    book.Add(new Book { Id = id++, Title = name, Author = author, Genres = (Genre)genreId, Year = year, Price = price });
                    Console.WriteLine("Книга создана");
                }
                //удаление
                else if (command == "2")
                {
                    Console.WriteLine("Введите код книги для удаления:");
                    string code = Console.ReadLine();
                    Book found = null;

                    if (int.TryParse(code, out int code2))
                    {
                        foreach (Book p in book)
                        {
                            if (p.Id == code2)
                            {
                                found = p;
                                break;
                            }
                        }

                        if (found == null)
                        {
                            Console.WriteLine("Товар не найден\n");
                            continue;
                        }

                        book.Remove(found);
                        Console.WriteLine("Товар удалён\n");
                    }
                }
                //поиск автор/название/жанр
                else if (command == "3")
                {
                    Console.WriteLine("Выберите тип поиска:");
                    Console.WriteLine("1 - По названию");
                    Console.WriteLine("2 - По автору");
                    Console.WriteLine("3 - По жанру");
                    string type = Console.ReadLine();

                    List<Book> results = new List<Book>();

                    if (type == "1")
                    {
                        Console.WriteLine("Введите название:");
                        string title = Console.ReadLine();
                        results = book.Where(b => b.Title.ToLower().Contains(title.ToLower())).ToList();
                    }
                    else if (type == "2")
                    {
                        Console.WriteLine("Введите автора:");
                        string author = Console.ReadLine();
                        results = book.Where(b => b.Author.ToLower().Contains(author.ToLower())).ToList();
                    }
                    else if (type == "3")
                    {
                        Console.WriteLine("Выберите жанр:");
                        Console.WriteLine("0 - Детектив");
                        Console.WriteLine("1 - Наука");
                        Console.WriteLine("2 - История");
                        Console.WriteLine("3 - Фантастика");
                        Console.WriteLine("4 - Другое");

                        if (!int.TryParse(Console.ReadLine(), out int genreId) || genreId < 0 || genreId > 4)
                        {
                            Console.WriteLine("Некорректный жанр\n");
                            continue;
                        }
                        results = book.Where(b => b.Genres == (Genre)genreId).ToList();
                    }
                    else
                    {
                        Console.WriteLine("Некорректный тип поиска\n");
                        continue;
                    }

                    if (results.Count == 0)
                    {
                        Console.WriteLine("Книги не найдены\n");
                        continue;
                    }

                    foreach (Book b in results)
                    {
                        Console.WriteLine(b);
                    }
                }
                //сортировка
                else if (command == "4")
                {
                    List<Book> sorted = book.OrderBy(b => b.Title).ToList();
                    foreach (Book b in sorted)
                    {
                        Console.WriteLine(b);
                    }
                }
                //тоже
                else if (command == "5")
                {
                    List<Book> sorted1 = book.OrderBy(b => b.Year).ToList();
                    foreach (Book c in sorted1)
                    {
                        Console.WriteLine(c);
                    }
                }
                //лакшери или дешевка челлендж
                else if (command == "6")
                {
                    List<Book> sorted2 = book.OrderBy(b => b.Price).ToList();
                    var sortedmin = sorted2.First();
                    var sortedmax = sorted2.Last();
                    Console.WriteLine($"Минимальная цена\n{sortedmin}");
                    Console.WriteLine($"Максимальная цена\n{sortedmax}");
                }
                //вывод сортировки по году
                else if (command == "7")
                {
                    List<Book> sorted3 = book.OrderBy(b => b.Year).ToList();
                    foreach (var group in sorted3)
                    {
                        Console.WriteLine($"Автор: {group.Author} - книга: {group.Title}");
                    }
                    Console.WriteLine();
                }
                //весь список
                else if (command == "8")
                {
                    if (book.Count == 0)
                    {
                        Console.WriteLine("Список книг пуст\n");
                        continue;
                    }

                    foreach (Book e in book)
                    {
                        Console.WriteLine(e);
                    }
                }
                //прога делает пока пока
                else if (command == "0")
                {
                    work = false;
                    break;
                }
            }
        }
    }
}
