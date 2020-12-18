using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Samples.AwaitMeansNoWait
{
    class NoAwait
    {
async Task<bool> DoWork()
{
    return await Task.Run(() => true);
}

Task<bool> DoWorkFixed()
{
    return Task.Run(() => true);
}
    }
}
