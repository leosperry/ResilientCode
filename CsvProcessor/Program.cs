﻿using CsvProcessor.MultiThread;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace CsvProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadPool.SetMaxThreads(100, 100);
            FileProcessor processor = new FileProcessor(new FakeWidgetLogic(), new FakeWidgetProvider());
            processor.ProcessFileWithSemaphoreFixed(@".\FakeData.txt").Wait();
        }

static void initial(string[] args)
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

    var csvReader = new CsvReader<Account>("path to file", lineParser);

    var accounts = csvReader.Read().Where(acct => acct != null);

    var accountProvider = new AccountProvider();
    accountProvider.SaveAccounts(accounts);
}

        private static void Example1(string[] args)
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
