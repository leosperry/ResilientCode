using Samples.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.SOLID.DIP
{
public class BusinessServiceWithDependencyInversion 
    : IBusinessServiceWithDependencyInversion
{
    readonly IObjectProvider _objectProvider;
    readonly ILogger _logger;
    
    public BusinessServiceWithDependencyInversion(
        IObjectProvider objProvider,
        ILogger logger)
    {
        _objectProvider = objProvider;
        _logger = logger;
    }

    public IEnumerable<SomeBusinessObject>
        GetObjectsForSpecialUseCase(int categoryId)
    {
        _logger.Log("beginning complex domain logic");
        var objects = _objectProvider.GetObjectsByCategory(categoryId);
        foreach (var item in objects)
        {
            //perform some domain logic
        }
        _logger.Log("completed complex domain logic");
        return objects;
    }
}


    public interface IBusinessServiceWithDependencyInversion
    {

    }
    public interface ILogger
    {
        void Log(string message);
    }

    public interface IObjectProvider
    {
        IEnumerable<SomeBusinessObject> GetObjectsByCategory(int categoryId);
    }
}
