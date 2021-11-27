using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Samples.Models;

namespace Samples.WhenThouYields.Linq
{
    class LinqExamples
    {
        object CountExample(IEnumerable<object> someCollection)
        {
if (someCollection.Count() > 1)
{
    foreach (var item in someCollection)
    {
        // do work
    }
}

return new { 
    data = someCollection,
    recordCount = someCollection.Count()
};
        }

        void twocollection()
        {
            List<Book> books = null;
            List<Category> categories = null;

var categoriesUsedInBooks =
    from cId in books.SelectMany(b => b.CategoryIds).Distinct()
    join cat in categories
    on cId equals cat.ID
    select cat;

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
