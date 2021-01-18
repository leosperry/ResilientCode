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
        var moreItems = GetOne(out var item);
        if (moreItems)
        {
            Current = item;
        }
        return moreItems;
    }

    public void Reset()
    {
        // compiler magic that resets the state of the _collectionGetter
    }

    private bool GetOne(out object item)
    {
        bool ranToCompletion = false;
        /*
            * Compiler magic that knows how to call the underlying reference
            * to the _collectionGetter and returns a single item
            * 
            * if there is _collectionGetter has run to completion
            * sets ranToCompletion to true
            */
        item = default;
        return !ranToCompletion;
    }
}
}
