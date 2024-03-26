using System.Collections.Generic;
using System.Diagnostics;

namespace TracerImplementation
{
    public class Method
    {
        public string Name { get; private set; }
        public string ClassName { get; private set; }
        public long ExecutionTime { get; private set; }
        public List<Method> NestedMethods { get; private set; }

        private Stopwatch stopwatch;

        public Method(string name, string className)
        {
            Name = name;
            ClassName = className;
            stopwatch = new Stopwatch();
            NestedMethods = new List<Method>();
        }

        public void StartTrace()
        {
            stopwatch.Start();
        }

        public void StopTrace()
        {
            stopwatch.Stop();
            ExecutionTime = stopwatch.ElapsedMilliseconds;
        }

        public void AddNestedMethod(Method method)
        {
            NestedMethods.Add(method);
        }

    }
}
