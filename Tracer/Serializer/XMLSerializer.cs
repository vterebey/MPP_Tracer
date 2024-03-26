using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace TracerImplementation.Serializer
{
    public class XMLSerializer : ISerialize
    {
        private List<ThreadDetails> _threads;
        private List<XElement> _xmlThreads;


        public string SerializeResult(List<ThreadDetails> threadsResult)
        {
            _threads = threadsResult;
            _xmlThreads = new List<XElement>();
            foreach (var thread in _threads)
            {
                XElement xmlThread = new XElement("thread",
                    new XAttribute("id", thread.Id),
                    new XAttribute("time", thread.ExecutionTime + "ms"));
                foreach (var rootMethod in thread.RootMethods)
                {
                    XElement xmlMethod = SerializeMethod(rootMethod);
                    xmlThread.Add(xmlMethod);
                }
                _xmlThreads.Add(xmlThread);
            }
            return MakeRoot().ToString();
        }
        
        private XElement SerializeMethod(Method method)
        {
            XElement xmlMethod = new XElement("method", 
                new XAttribute("name", method.Name),
                new XAttribute("class", method.ClassName),
                new XAttribute("time", method.ExecutionTime));
            foreach (var nestedMethod in method.NestedMethods)
            {
                xmlMethod.Add(SerializeMethod(nestedMethod));
            }
            return xmlMethod;
        }

        private XElement MakeRoot()
        {
            XElement root = new XElement("root");
            foreach (var thread in _xmlThreads)
            {
                root.Add(thread);
            }
            return root;
        }

    }
}
