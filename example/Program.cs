using System;
using System.Threading;
using TracerClasses.Serializer;
using TracerImplementation;
using TracerImplementation.Serializer;

namespace Example
{
    class Program
    {
        private static Tracer _tracer = new Tracer();
        private static JSONSerializer _jSerializer = new JSONSerializer();
        private static ISerialize _serializer = new XMLSerializer();
        private static string _xmlPath = @"xml2.txt";
        private static string _jsonPath = @"json2.txt";
        private static IWriter writerXml = new Writer(_xmlPath);
        private static IWriter writerJson = new Writer(_jsonPath);
        public static void Main()
        {
            Thread thread1 = new Thread(new ThreadStart(NestedMethod));
            Thread thread2 = new Thread(new ThreadStart(NestedMethod));
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            Console.WriteLine(_serializer.SerializeResult(_tracer.GetTraceResult()));
            writerXml.WriteToFile(_serializer.SerializeResult(_tracer.GetTraceResult()));
            Console.WriteLine(_jSerializer.SerializeResult(_tracer.GetTraceResult()));
            writerJson.WriteToFile(_jSerializer.SerializeResult(_tracer.GetTraceResult())); 
        }
        public static void NestedMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            FirstNestedMethod();
            _tracer.StopTrace();
        }

        public static void FirstNestedMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            SecondNestedMethod();
            ThirdNestedMethod();
            _tracer.StopTrace();
        }

        public static void SecondNestedMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }

        public static void ThirdNestedMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(200);
            _tracer.StopTrace();
        }
    }
}
