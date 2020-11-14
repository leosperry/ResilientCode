using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CsvProcessor
{
class CsvReader<T> : ICsvReader<T> where T : class
{
    string _path;
    Func<string[], T> _lineParser;

    public bool FileHasHeader { get; set; } = true;

    public CsvReader(string path, Func<string[], T> lineParser)
    {
        _path = path;
    }

    public IEnumerable<T> Read()
    {
        StreamReader rdr = new StreamReader(_path);
        if (FileHasHeader)
        {
            //ignore the first line
            rdr.ReadLine();
        }
        string line;
        T item;
        while ((line = rdr.ReadLine()) != null)
        {
            item = null;
            try
            {
                item = _lineParser(line.Split(','));
            }
            catch (Exception)
            {
                //log the error
            }
            if (item != null)
            {
                yield return item;
            }
        }
    }
}
}
