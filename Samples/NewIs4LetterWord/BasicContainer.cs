using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.NewIs4LetterWord
{
public class BasicContainer
{
    Dictionary<Type, Func<object>> _resolvers = new Dictionary<Type, Func<object>>();
    public void Register<T>(Func<T> resolver)
    {
        _resolvers[typeof(T)] = () => resolver();
    }

    public T Resolve<T>()
    {
        return (T)_resolvers[typeof(T)]();
    }
}
}
