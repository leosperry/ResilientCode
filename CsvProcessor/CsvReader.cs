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
            _lineParser = lineParser;
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

public IEnumerable<IEnumerable<T>> ReadBatched(int batchSize)
{
    StreamReader rdr = new StreamReader(_path);
    if (FileHasHeader)
    {
        //ignore the first line
        rdr.ReadLine();
    }
    while (!rdr.EndOfStream)
    {
        yield return GetBatch(batchSize, rdr);
    }
}

private IEnumerable<T> GetBatch(int batchSize, StreamReader rdr)
{
    string line;
    T item;
    while (--batchSize >= 0 && (line = rdr.ReadLine()) != null)
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
