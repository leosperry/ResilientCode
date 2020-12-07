using System;
using System.Collections.Generic;
using System.Text;

namespace CsvProcessor
{
class AccountParsingLogic : IAccountParsingLogic
{
    public bool AccountIsValid(Account account)
    {
        return 
            account.ID > 0 &&
            !string.IsNullOrEmpty(account.Name) &&
            account.Value != null;
    }

    public int? GetAccountNumber(string input)
    {
        if (int.TryParse(input, out int accountNumber))
        {
            return accountNumber;
        }
        return null;
    }

    public string GetNameForSaving(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return null;
        }
        return input.Length > 50 ? input.Substring(0, 50) : input;
    }

    public decimal? GetValue(string input)
    {
        if (decimal.TryParse(input, out decimal val))
        {
            return val;
        }
        return null;
    }
}
}
