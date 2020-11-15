using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.WhenThouYields.Linq
{
    static class WhereExample
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> collection, Func<T, bool> filter)
        {
            foreach (var item in collection)
            {
                if (filter(item))
                {
                    yield return item;
                }
            }
        }
    }
}
