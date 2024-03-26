using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.Internal;
using System.Threading;
using TracerImplementation;

namespace TracerTest
{
    [TestClass]
    public class UnitTest
    {
        private Tracer tracer = new Tracer();

        [TestMethod]
        public void CheckMultiThreadExampleOfUse()
        {
            Thread thread1 = new Thread(new ThreadStart(ThreadMethod));
            Thread thread2 = new Thread(new ThreadStart(ThreadMethod));
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            Assert.AreEqual(2, tracer.GetTraceResult().Count);
        }

        public void ThreadMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            FirstNestedMethod();
            SecondNestedMethod();
            tracer.StopTrace();
        }

        public void FirstNestedMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        public void SecondNestedMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            ThirdNestedMethod();
            tracer.StopTrace();
        }

        public void ThirdNestedMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        [TestMethod]
        public void CheckForCorrectDetailsOfMethod()
        {
            FirstNestedMethod();
            Assert.AreEqual("UnitTest", tracer.GetTraceResult()[0].RootMethods[0].ClassName);
            Assert.AreEqual("FirstNestedMethod", tracer.GetTraceResult()[0].RootMethods[0].Name);
        }

        [TestMethod]
        public void CheckForAmountOfRootMethods()
        {
            ThreadMethod();
            SecondNestedMethod();
            FirstNestedMethod();
            Assert.AreEqual(3, tracer.GetTraceResult()[0].RootMethods.Count);
        }

        [TestMethod]
        public void checkExecutionTime()
        {
            ThirdNestedMethod();
            Assert.IsTrue(100 <= tracer.GetTraceResult()[0].ExecutionTime, "Execution time of ThirdNestedMethod should be > 100");
        }

        [TestMethod]
        public void CheckForAmountOfNestedMethods()
        {
            ThreadMethod();
            Assert.AreEqual(3, tracer.GetTraceResult()[0].MaxStackDeep);
        }
    }
}