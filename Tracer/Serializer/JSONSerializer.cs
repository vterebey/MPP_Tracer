using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace TracerImplementation.Serializer
{
    public class JSONSerializer : ISerialize
    {
        private List<ThreadDetails> _threads;
        private List<JObject> _jsonThreads;
        public string SerializeResult(List<ThreadDetails> threadsResult)
        {
            _threads = threadsResult;
            _jsonThreads = new List<JObject>();
            foreach (var thread in _threads)
            {
                JObject jsonThread = new JObject();
                jsonThread["id"] = thread.Id;
                jsonThread["time"] = thread.ExecutionTime;
                JArray jsonMethods = new JArray();
                foreach (var rootMethod in thread.RootMethods)
                {
                    JObject jsonMethod = SerializeMethod(rootMethod);
                    jsonMethods.Add(jsonMethod);
                }
                jsonThread["methods"] = jsonMethods;
                _jsonThreads.Add(jsonThread);
            }
            return MakeRoot().ToString();
        }

        private JObject SerializeMethod(Method method)
        {
            JObject jsonMethod = new JObject();
            jsonMethod["Name"] = method.Name;
            jsonMethod["Class"] = method.ClassName;
            jsonMethod["Time"] = method.ExecutionTime + " ms";
            JArray methods = new JArray();
            foreach (var nestedMethod in method.NestedMethods)
            {
                methods.Add(SerializeMethod(nestedMethod));
            }
            jsonMethod["Methods"] = methods;
            return jsonMethod;
        }

        private JObject MakeRoot()
        {
            JObject root = new JObject();
            JArray threads = new JArray();
            foreach (var thread in _jsonThreads)
            {
                threads.Add(thread);
            }
            root["threads"] = threads;
            return root;
        }

    }
}
