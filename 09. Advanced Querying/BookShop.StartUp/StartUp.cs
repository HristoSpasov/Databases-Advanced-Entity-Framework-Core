namespace BookShop
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using BookShop.Data;
    using BookShop.Initializer;
    using BookShop.Models;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            using (var context = new BookShopContext())
            {
                // DbInitializer.ResetDatabase(context);

                // string input = Console.ReadLine();

                // string result01 = GetBooksByAgeRestriction(context, command); // Task 01 Age Restriction

                // string result02 = GetGoldenBooks(context); // Task 02 Golden Books

                // string result03 = GetBooksByPrice(context); // 03 Books by Price

                // string result04 = GetBooksNotRealeasedIn(context, year); // 04 Not Released In

                // string result05 = GetBooksByCategory(context, input); // 05 Book Titles by Category

                // string result06 = GetBooksReleasedBefore(context, date); // 06 Released Before Date

                // string result07 = GetAuthorNamesEndingIn(context, input); // 07 Author Search

                // string result08 = GetBookTitlesContaining(context, input); // 08 Book Search

                // string result09 = GetBooksByAuthor(context, input); // 09 Book Search by Author

                // int result10 = CountBooks(context, lengthCheck);  // 10 Count Books

                // string result11 = CountCopiesByAuthor(context); // 11 Total Book Copies

                // string result12 = GetTotalProfitByCategory(context); // 12 Profit by Category

                // string result13 = GetMostRecentBooks(context); // 13. Most Recent Books

                // IncreasePrices(context); // 14. Increase Prices

                // int result15 = RemoveBooks(context); // 15. Remove books

                // Console.WriteLine(result13);
                // Console.WriteLine(result13.Length);
            }
        }

        public static int RemoveBooks(BookShopContext context)
        {
            Book[] booksToRemove = context.Books
                                       .Where(c => c.Copies < 4200)
                                       .ToArray();

            int bookcToRemoveCount = booksToRemove.Length;

            context.Books.RemoveRange(booksToRemove);

            context.SaveChanges();

            return bookcToRemoveCount;
        }

        public static void IncreasePrices(BookShopContext context)
        {
            Book[] bookArr = context.Books
                .Where(rd => rd.ReleaseDate.Value.Year < 2010)
                .ToArray();

            foreach (var book in bookArr)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            Category[] categories = context.Categories
                .Include(cb => cb.CategoryBooks)
                    .ThenInclude(b => b.Book)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (Category cat in categories.OrderByDescending(bId => bId.CategoryBooks.GroupBy(c => c.BookId).Count()).ThenBy(n => n.Name))
            {
                sb.AppendLine($"--{cat.Name}");

                cat.CategoryBooks
                    .Where(cId => cId.CategoryId == cat.CategoryId)
                    .OrderByDescending(bc => bc.Book.ReleaseDate)
                    .Take(3)
                    .Select(b => new
                    {
                        BookTitle = b.Book.Title,
                        BookYear = b.Book.ReleaseDate.Value.Year
                    })
                    .Select(str => $"{str.BookTitle} ({str.BookYear})")
                    .ToList()
                    .ForEach(r => sb.AppendLine(r));
            }

            return sb.ToString().Trim();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            context.Categories
                .Include(c => c.CategoryBooks)
                .ThenInclude(n => n.Book)
                .OrderByDescending(n => n.CategoryBooks.Select(s => s.Book.Price * s.Book.Copies).Sum())
                .Select(cat => $"{cat.Name} ${cat.CategoryBooks.Select(a => a.Book.Copies * a.Book.Price).Sum():F2}")
                .ToList()
                .ForEach(r => sb.AppendLine(r));

            return sb.ToString().Trim();
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            context.Books
                .GroupBy(a => a.Author)
                .Select(b => new
                {
                    AuthorFullName = $"{b.Key.FirstName} {b.Key.LastName}",
                    BooksCount = b.Select(c => c.Copies).Sum()
                })
                .OrderByDescending(bc => bc.BooksCount)
                .Select(str => $"{str.AuthorFullName} - {str.BooksCount}")
                .ToList()
                .ForEach(r => sb.AppendLine(r));

            return sb.ToString().Trim();
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            return context.Books
                        .Where(tl => tl.Title.Length > lengthCheck)
                        .Count();
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            string pattern = $@"^{input}.*";

            StringBuilder sb = new StringBuilder();

            context.Books
                .Where(b => Regex.IsMatch(b.Author.LastName, pattern, RegexOptions.IgnoreCase))
                .Select(b => new
                {
                    Id = b.BookId,
                    Title = b.Title,
                    FullName = $"{b.Author.FirstName} {b.Author.LastName}"
                })
                .OrderBy(bId => bId.Id)
                .Select(str => $"{str.Title} ({str.FullName})")
                .ToList()
                .ForEach(r => sb.AppendLine(r));


            return sb.ToString().Trim();
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            string pattern = $@".*{input}.*";

            StringBuilder sb = new StringBuilder();

            context.Books
                .Where(t => Regex.IsMatch(t.Title, pattern, RegexOptions.IgnoreCase))
                .Select(t => t.Title)
                .OrderBy(t => t)
                .ToList()
                .ForEach(r => sb.AppendLine(r));

            return sb.ToString().Trim();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            string pattern = $@".*{input}$";

            StringBuilder sb = new StringBuilder();

            context.Authors
                .Where(fn => Regex.IsMatch(fn.FirstName, pattern, RegexOptions.IgnoreCase))
                .Select(fn => $"{fn.FirstName} {fn.LastName}")
                .OrderBy(n => n)
                .ToList()
                .ForEach(r => sb.AppendLine(r));

            return sb.ToString().Trim();
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime parsedDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            StringBuilder sb = new StringBuilder();

            context.Books
                .Where(d => d.ReleaseDate < parsedDate)
                .OrderByDescending(d => d.ReleaseDate)
                .Select(b => new
                {
                    Title = b.Title,
                    EditionType = b.EditionType,
                    Price = b.Price,
                })
                .Select(str => $"{str.Title} - {str.EditionType} - ${str.Price:F2}")
                .ToList()
                .ForEach(r => sb.AppendLine(r));

            return sb.ToString().Trim();
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            string[] categories = input.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            context.Books
                 .Select(b => new
                 {
                     Title = b.Title,
                     ContainsCategory = b.BookCategories.Any(p => categories.Contains(p.Category.Name.ToLower()))
                 })
                 .Where(c => c.ContainsCategory)
                 .Select(t => t.Title)
                 .OrderBy(t => t)
                 .ToList()
                 .ForEach(r => sb.AppendLine(r));

            return sb.ToString().Trim();
        }

        public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            StringBuilder sb = new StringBuilder();

            context.Books
                .Where(rd => rd.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(t => t.Title)
                .ToList()
                .ForEach(r => sb.AppendLine(r));

            return sb.ToString().Trim();
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            context.Books
                .Select(b => new
                {
                    Title = b.Title,
                    Price = b.Price
                })
                .Where(p => p.Price > 40)
                .OrderByDescending(p => p.Price)
                .Select(b => $"{b.Title} - ${b.Price:F2}")
                .ToList()
                .ForEach(r => sb.AppendLine(r));

            return sb.ToString().Trim();
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .Select(t => new
                {
                    Id = t.BookId,
                    Title = t.Title
                })
                .OrderBy(bId => bId.Id)
                .ToList()
                .ForEach(r => sb.AppendLine(r.Title));


            return sb.ToString().Trim();
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            StringBuilder sb = new StringBuilder();

            context.Books
                .Select(t => new
                {
                    Title = t.Title,
                    Restriction = t.AgeRestriction
                })
                .Where(r => r.Restriction.ToString().ToLower() == command.ToLower())
                .Select(t => t.Title)
                .OrderBy(n => n)
                .ToList()
                .ForEach(result => sb.AppendLine(result));


            return sb.ToString().Trim();
        }
    }
}
