using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.WhenThouYields
{
    class Series
    {
public IEnumerable<int> PrimeNumbersLessThan10()
{
    yield return 1;
    yield return 2;
    yield return 3;
    yield return 5;
    yield return 7;
}
    }
}
