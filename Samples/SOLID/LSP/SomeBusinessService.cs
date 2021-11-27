using Samples.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.SOLID.LSP
{
abstract class SomeBusinessServiceBase
{
    protected List<SomeBusinessObject> _objects;

    public abstract void LoadData();

    public virtual void Render()
    {
        foreach (var item in _objects)
        {
            Console.WriteLine(item);
        }
    }
}
abstract class SomeBusinessServiceBaseFixed
{
    protected abstract IEnumerable<SomeBusinessObject> GetData();

    public virtual void Render(IEnumerable<SomeBusinessObject> objects)
    {
        foreach (var item in objects)
        {
            Console.WriteLine(item);
        }
    }
}
}
