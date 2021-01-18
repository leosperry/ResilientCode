using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.TooManyIfs
{
    class Book
    {

    }

    class Category
    {
        public int ID { get; set; }
    }

    class Author { }

    interface IBookProvider
    {
        Task<IEnumerable<Book>> Search(string query);
        Task Create(string title, string description, int authorId, IEnumerable<int> categoryIds);
    }

    interface IAuthorProvider
    {
        Task<Author> GetById(int authorId);
    }

    interface ICategoryProvider
    {
        Task<Category> GetById(int cid);
    }

    interface IBookLogic
    {

    }

    class BookController
    {
        IAuthorProvider _authorProvider;
        IBookProvider _bookProvider;
        ICategoryProvider _catProvider;

public Task<IEnumerable<Book>> Search(string query)
{
    if (string.IsNullOrEmpty(query) || query.Length > 50)
    {
        // return non-200
        throw new Exception("meaningful error"); 
    }
    else
    {
        return _bookProvider.Search(query);
    }
}

public async Task Create(string title, string description, int authorId, 
    IEnumerable<int> categoryIds)
{
    if (string.IsNullOrEmpty(title) && authorId > 0)
    {
        if (await _authorProvider.GetById(authorId) != null)
        {
            if (categoryIds == null)
            {
                categoryIds = Enumerable.Empty<int>();
            }
            var catTasks = categoryIds.Select(cid => _catProvider.GetById(cid));
            var categories = await Task.WhenAll(catTasks);

            if (categories.All(c => c != null))
            {
                await _bookProvider.Create(
                    title, description, authorId, categoryIds);
                return;
            }
        }
    }
    //return non-200
}
    }

}
