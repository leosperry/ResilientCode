using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Samples.EfficientDoesntExist
{
    class Account
    {

    }

    interface IAccountProvider
    {
        Task<Account> GetById(int id);
    }

    interface ILogger
    {
        void LogException(Exception ex);
    }

    class AccountLogic
    {
        ILogger _logger;
        IAccountProvider _acctProvider;

public async Task<Account> GetById(int id)
{
    try
    {
        return await _acctProvider.GetById(id);
    }
    catch (Exception ex)
    {
        _logger.LogException(ex);
        throw;
    }
}
    }
}
