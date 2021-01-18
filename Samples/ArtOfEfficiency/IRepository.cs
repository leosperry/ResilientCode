using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.ArtOfEfficiency
{
class MyClass
{

}
public interface IRepository<T, Tid, Tkey>
{
    IEnumerable<T> GetAll();
    T GetById(Tid id);
    IEnumerable<T> GetByKey(Tkey key);
    void Create(T item);
    void Update(T item);
    void Delete(T item);
    void DeleteById(Tid id);
}

    public interface IGetByKey<T, Tkey>
    {
        IEnumerable<T> GetByKey(Tkey id);
    }

    class Book
    {

    }

    class Category
    {

    }

    class Author
    {

    }

    class BookRepository : IRepository<Book, int, int>, IGetByKey<Book, Category>, IGetByKey<Book, Author>
    {
        public void Create(Book item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Book item)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

public IEnumerable<Book> GetAll()
{
    throw new NotImplementedException();
}

        public Book GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetByKey(int key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetByKey(Author author)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetByKey(Category category)
        {
            throw new NotImplementedException();
        }

        public void Update(Book item)
        {
            throw new NotImplementedException();
        }
    }
}
