using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Samples.AwaitMeansNoWait
{
    class MultiDataCallsSync
    {
        IBookProvider _bookProvider;

object GetPageInfo(int bookId)
{
    var book = _bookProvider.GetById(bookId);
    var booksByAuthor = _bookProvider.GetByAuthor(book.AuthorId);
    var booksInCategory = _bookProvider.GetTop10BooksInCategory(book.PrimaryCategoryId);

    return new { 
        booksByAuthor,
        booksInCategory
    };
}
object GetPageInfo2(int bookId)
{
    var book = _bookProvider.GetById(bookId);
    IEnumerable<Book> booksByAuthor = null;
    IEnumerable<Book> booksInCategory = null;
    Thread authorThread = new Thread(() =>
        booksByAuthor = _bookProvider.GetByAuthor(book.AuthorId)
    );
    authorThread.Start();
    Thread catThread = new Thread(() =>
        booksInCategory = 
        _bookProvider.GetTop10BooksInCategory(book.PrimaryCategoryId)
    );
    catThread.Start();
    authorThread.Join();
    catThread.Join();

    return new
    {
        booksByAuthor,
        booksInCategory
    };
}

async Task<object> GetPageInfoAsync(int bookId)
{
    var book = await _bookProvider.GetByIdAsync(bookId);
    var booksByAuthor = _bookProvider.GetByAuthorAsync(book.AuthorId);
    var booksInCategory = _bookProvider
        .GetTop10BooksInCategoryAsync(book.PrimaryCategoryId);

    return new { 
        booksByAuthor = await booksByAuthor,
        booksInCategory = await booksInCategory
    };
}
    }

    class Book
    {
        public int AuthorId { get; set; }
        public int PrimaryCategoryId { get; set; }
    }

    class Catagory { }

    interface IBookProvider
    {
        Book GetById(int bookId);
        Task<Book> GetByIdAsync(int bookId);

        IEnumerable<Book> GetTop10BooksInCategory(int categoryId);
        Task<IEnumerable<Book>> GetTop10BooksInCategoryAsync(int categoryId);

        IEnumerable<Book> GetByAuthor(int authorId);
        Task<IEnumerable<Book>> GetByAuthorAsync(int authorId);
    }
}
