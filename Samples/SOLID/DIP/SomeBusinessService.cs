using Samples.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.SOLID.DIP
{
public class SomeBusinessService
{
    public IEnumerable<SomeBusinessObject> 
        GetObjectsForSpecialUseCase(int categoryId)
    {
        DataAccessLayer dal = new DataAccessLayer();
        var objects = dal.GetObectsFiltered(categoryId);
        foreach (var item in objects)
        {
            //perform some domain logic
        }
        return objects;
    }
}

    public class DataAccessLayer
    {
        public IEnumerable<SomeBusinessObject> GetObectsFiltered(int categoryId)
        {
            return default;
        }
    }
}
