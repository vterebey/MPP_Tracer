using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TracerClasses.Serializer
{
    public class Writer : IWriter
    {
        private string _path;
        public Writer(string path)
        {
            _path = path;
        }

        public void WriteToFile(string text)
        {
            if (!File.Exists(_path))
            {
                File.WriteAllText(_path, text, Encoding.UTF8);
            }
        }
    }
}
