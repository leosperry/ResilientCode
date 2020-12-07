using System;
using System.IO;
using System.Linq;

namespace CsvProcessor
{
    class Program
    {
static void Main(string[] args)
{
    AccountParsingLogic logic = new AccountParsingLogic();

    Func<string[], Account> lineParser = cells => {
        var id = logic.GetAccountNumber(cells[0]);
        var name = logic.GetNameForSaving(cells[1]);
        var value = logic.GetValue(cells[2]);

        if (id.HasValue)
        {
            return new Account()
            {
                ID = id.Value,
                Name = name,
                Value = value
            };
        }
        return null;
    };

    var csvReader = new CsvReader<Account>(
        "path to file", lineParser);

    var accounts = csvReader.Read().Where(acct => acct != null);

    var accountProvider = new AccountProvider();
    accountProvider.SaveAccounts(accounts);
}

        private static void Example1()
        {
            StreamReader rdr = new StreamReader("path to file");
            string line;
            int lineCount = 0;
            while ((line = rdr.ReadLine()) != null)
            {
                //process the line
                lineCount++;
            }
            Console.WriteLine($"{lineCount} lines processed");
        }

    }
}
