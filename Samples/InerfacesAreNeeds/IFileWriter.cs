using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Samples.SOLID.ISP
{
    public interface IFileWriterBad
    {
        void Initialize(char driveLetter, string path);
        bool WriteFile(Stream s);
    }

public interface IFileWriter
{
    bool WriteFile(Stream s);
}

public class FileWriter : IFileWriter
{
    public FileWriter(char driveLetter, string path)
    {
        /*
            * initialization logic
            * and infratructure dependent references
            * belong to the class
            * not the interface
            */
    }

    public bool WriteFile(Stream s)
    {
        throw new NotImplementedException();
    }
    }
}
