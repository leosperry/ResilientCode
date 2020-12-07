using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samples.OofOne
{
    class Examples
    {
        readonly ICategoryProvider _categoryProvider;
        readonly IBookProvider _bookProvider;

        IEnumerable<Category> GetCategoriesFromBooks(
            IEnumerable<Book> books,
            IEnumerable<Category> categories)
        {
            List<Category> usedCategories = new List<Category>();

            foreach (var book in books)
            {
                foreach (var catId in book.CategoryIds)
                {
                    var category = categories.FirstOrDefault(c => c.ID == catId);
                    if (category != null &&
                        usedCategories.Any(uc => uc.ID == category.ID))
                    {
                        usedCategories.Add(category);
                    }
                }
            }
            return usedCategories;
        }

        IEnumerable<Category> GetCategoriesFromBooks2(
            IEnumerable<Book> books,
            IEnumerable<Category> categories)
        {
            var usedCategoryIds = books.SelectMany(b => b.CategoryIds);
            var categoryHash = new HashSet<int>(usedCategoryIds);

            foreach (var category in categories)
            {
                if (categoryHash.Contains(category.ID))
                {
                    yield return category;
                }
            }
        }

        public IEnumerable<Book> GetRelatedBooks(Book book)
        {
            Dictionary<Book, int> bookCounts = new Dictionary<Book, int>();
            foreach (var cat in _categoryProvider.GetCategories(book))
            {
                var categories = _bookProvider.GetBooksInCategory(cat.ID);
                UpdateBookCounts(bookCounts, categories);
            }

            return bookCounts.OrderBy(kvp => kvp.Value).Select(kvp => kvp.Key);
        }

        private static void UpdateBookCounts(
            Dictionary<Book, int> bookCounts, IEnumerable<Book> books)
        {
            foreach (var relatedBook in books)
            {
                if (bookCounts.ContainsKey(relatedBook))
                {
                    bookCounts[relatedBook] = bookCounts[relatedBook] + 1;
                }
                else
                {
                    bookCounts[relatedBook] = 1;
                }
            }
        }
    }


    public interface ICategoryProvider
    {
        IEnumerable<Category> GetCategories(Book book);
    }

    public interface IBookProvider
    {
        Book GetById(int id);
        IEnumerable<Book> GetBooksInCategory(int catId);
    }

    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public IEnumerable<int> CategoryIds { get; set; }
    }

    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }

    }
}
