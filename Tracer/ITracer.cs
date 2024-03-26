using System.Collections.Generic;

namespace TracerImplementation
{
    interface ITracer
    {
        // вызывается в начале замеряемого метода
        void StartTrace();

        // вызывается в конце замеряемого метода
        void StopTrace();

        // получить результаты измерений
        List<ThreadDetails> GetTraceResult();
    }
}
