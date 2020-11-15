using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Samples.Models;

namespace Samples.WhenThouYields.Linq
{
    class LinqExamples
    {
        void twocollection()
        {
            List<Book> books = null;
            List<Category> categories = null;

        var categoriesUsedInBooks =
            from cId in books.SelectMany(b => b.CategoryIds).Distinct()
            join c in categories
            on cId equals c.ID
            select c;

        }
    }

    class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }

    class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public List<int> CategoryIds { get; set; }

    }
}
