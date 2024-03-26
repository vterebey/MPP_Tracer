using System;
using System.Collections.Generic;
using System.Linq;

namespace TracerImplementation
{
    public class ThreadDetails
    {
        public int Id { get; private set; }
        public long ExecutionTime { get; private set; }
        public Stack<Method> RunningMethods { get; private set; }
        public List<Method> RootMethods { get; private set; }

        public int MaxStackDeep { get; private set; } 

        public ThreadDetails(int id)
        {
            Id = id;
            ExecutionTime = 0;
            MaxStackDeep = 0;
            RunningMethods = new Stack<Method>();
            RootMethods = new List<Method>();
        }

        public void StartTraceMethod(Method method)
        {
            if (RunningMethods.Count() != 0)
            {
                Method topMethod = RunningMethods.Peek();
                topMethod.AddNestedMethod(method);
            }
            RunningMethods.Push(method);
            method.StartTrace();
            if (RunningMethods.Count > MaxStackDeep )
            {
                MaxStackDeep = RunningMethods.Count;
            }
        }

        public void StopTraceMethod()
        {
            if (RunningMethods.Count > 0)
            {
                Method executedMethod = RunningMethods.Pop();
                executedMethod.StopTrace();
                ExecutionTime += executedMethod.ExecutionTime;
                if (RunningMethods.Count == 0)
                {
                    RootMethods.Add(executedMethod);
                }
            }
        }
    }
}
