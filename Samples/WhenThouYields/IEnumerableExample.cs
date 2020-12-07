using System;
using System.Text;

namespace Samples.WhenThouYields.Hidden
{
public interface IEnumerable
{
    IEnumerator GetEnumerator();
}

public interface IEnumerator
{
    object Current { get; }
    bool MoveNext();
    void Reset();
}
}
