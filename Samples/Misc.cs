using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Samples
{
    class Misc
    {
        ILogger _logger = null;
        void Asdf()
        {
using (_logger.BeginScope("add context here"))
{
    CallAnotherMethod();
}
        }

        private void CallAnotherMethod()
        {
            throw new NotImplementedException();
        }

public void TopLevelMethod()
{
    try
    {
        using (_logger.BeginScope("add context here"))
        {
            CallAnotherMethod();
        }
    }
    catch (Exception ex) when (HandleException(ex, true)) 
    {
    }
}

public bool HandleException(Exception ex, bool propogate = false)
{
    _logger.LogError(ex, "unhandled error");
    return propogate;
}
    }
}
