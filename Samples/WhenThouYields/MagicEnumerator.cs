using System;
using System.Collections;
using System.Text;

namespace Samples.WhenThouYields
{
class MagicEnumerator : IEnumerator
{
    private Func<IEnumerable> _collectionGetter;

    public object Current { get; private set; }

    public MagicEnumerator(Func<IEnumerable> collectionGetter)
    {
        _collectionGetter = collectionGetter;
    }

    public bool MoveNext()
    {
        var one = GetOne();
        if (one == null)
        {
            return false;
        }
        else
        {
            Current = one;
            return true;
        }
    }

    public void Reset()
    {
        // compiler magic that resets the state of the _collectionGetter
    }

    private object GetOne()
    {
        /*
            * Compiler magic that knows how to call the underlying reference
            * to the _collectionGetter and returns a single item
            * 
            * if there is no next item it returns null
            */
        return default;
    }
}
}
