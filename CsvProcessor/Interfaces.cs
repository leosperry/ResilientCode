using System;
using System.Collections.Generic;
using System.Text;

namespace CsvProcessor
{
interface ICsvReader<T> where T: class
{
    IEnumerable<T> Read();
}

interface IAccountParsingLogic
{
    string GetNameForSaving(string input);
    int? GetAccountNumber(string input);
    decimal? GetValue(string input);
        bool AccountIsValid(Account account);
}

interface IAccountProvider
{
    void SaveAccounts(IEnumerable<Account> accounts);
}
}
