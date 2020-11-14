using System;
using System.Collections.Generic;
using System.Text;

namespace CsvProcessor
{
    interface ICustomFileReader<T>
    {
        T ReadLine();
    }


    class CustomFileReader
    {
    }
}
