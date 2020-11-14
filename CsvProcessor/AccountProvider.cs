using System;
using System.Collections.Generic;
using System.Text;

namespace CsvProcessor
{
class AccountProvider : IAccountProvider
{
    public void SaveAccounts(IEnumerable<Account> accounts)
    {
        foreach (var account in accounts)
        {
            // save to the database
        }
    }
}
}
