using System;
using System.Collections;
using System.Text;

namespace Samples.WhenThouYields
{
    class ForeachExample
    {
public void ForeachIteration(IEnumerable enumerable)
{
    foreach (var item in enumerable)
    {
        // do work
    }
}

public void WhileIteration(IEnumerable enumerable)
{
    var enumerator = enumerable.GetEnumerator();
    while (enumerator.MoveNext())
    {
        var item = enumerator.Current;
        // do work
    }
    enumerator.Reset();
}
    }
}
