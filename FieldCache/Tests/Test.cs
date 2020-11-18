using Microsoft.VisualStudio.TestTools.UnitTesting;
using FieldCacheNamespace;
using System.Collections.Concurrent;

namespace Tests
{
    [TestClass]
    public sealed class Test
    {
        [TestMethod]
        public void TestSimple1()
        {
            var container = new FieldCache<int>();
            Assert.AreEqual(4, container.GetValue(() => 4));
            Assert.AreEqual(4, container.GetValue(() => 4));
        }

        [TestMethod]
        public void TestSimpleString()
        {
            var container = new FieldCache<string>();
            Assert.AreEqual("ss", container.GetValue(() => "ss"));
            Assert.AreEqual("ss", container.GetValue(() => "ss"));
        }

        private record SomeTestRecord
        {
            public ConcurrentDictionary<string, string> Dict => dict.GetValue(() => new());
            private FieldCache<ConcurrentDictionary<string, string>> dict;
        }

        [TestMethod]
        public void TestThreadSafety()
        {
            SomeTestRecord someInstance = new SomeTestRecord();

            void ChangeADict(int threadId)
            {
                someInstance.Dict["someSpecificKey"] = threadId.ToString();
            }

            new ThreadingChecker(ChangeADict).Run(iterCount: 10000);
        }

        private record Person(string FirstName, string LastName)
        {
            public string FullName => fullName.GetValue(() => FirstName + LastName);
            private FieldCache<string> fullName;
        }

        [TestMethod]
        public void TestEqualityPure()
        {
            var a = new Person("John", "Ivanov");
            var b = new Person("John", "Ivanov");
            Assert.AreEqual(a, b);
        }

        [TestMethod]
        public void TestEqualityOneInitted()
        {
            var a = new Person("John", "Ivanov");
            Assert.AreEqual("JohnIvanov", a.FullName);
            var b = new Person("John", "Ivanov");
            Assert.AreEqual(a, b);
        }

        [TestMethod]
        public void TestEqualityBothInitted()
        {
            var a = new Person("John", "Ivanov");
            Assert.AreEqual("JohnIvanov", a.FullName);
            var b = new Person("John", "Ivanov");
            Assert.AreEqual("JohnIvanov", b.FullName);
            Assert.AreEqual(a, b);
        }

        [TestMethod]
        public void TestUnequalityPure()
        {
            var a = new Person("Peter", "Smith");
            var b = new Person("John", "Ivanov");
            Assert.AreNotEqual(a, b);
        }
    }
}
