using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TracerClasses.Serializer
{
    public interface IWriter
    {
        void WriteToFile(string text);
    }
}
