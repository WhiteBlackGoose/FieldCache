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
            Assert.AreEqual(4, container.GetValue(_ => 4, this));
            Assert.AreEqual(4, container.GetValue(_ => 4, this));
        }

        [TestMethod]
        public void TestSimpleString()
        {
            var container = new FieldCache<string>();
            Assert.AreEqual("ss", container.GetValue(_ => "ss", this));
            Assert.AreEqual("ss", container.GetValue(_ => "ss", this));
        }

        private record SomeTestRecord
        {
            public ConcurrentDictionary<string, string> Dict => dict.GetValue(_ => new(), this);
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
            public string FullName => fullName.GetValue(@this => @this.FirstName + " " + @this.LastName, this);
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
            Assert.AreEqual("John Ivanov", a.FullName);
            var b = new Person("John", "Ivanov");
            Assert.AreEqual(a, b);
        }

        [TestMethod]
        public void TestEqualityBothInitted()
        {
            var a = new Person("John", "Ivanov");
            Assert.AreEqual("John Ivanov", a.FullName);
            var b = new Person("John", "Ivanov");
            Assert.AreEqual("John Ivanov", b.FullName);
            Assert.AreEqual(a, b);
        }

        [TestMethod]
        public void TestUnequalityPure()
        {
            var a = new Person("Peter", "Smith");
            var b = new Person("John", "Ivanov");
            Assert.AreNotEqual(a, b);
        }

        private record SomeTestRecord_static
        {
            public ConcurrentDictionary<string, string> Dict => dict.GetValue(_ => new(), this);
            private FieldCache<ConcurrentDictionary<string, string>> dict;
        }

        [TestMethod]
        public void TestThreadSafety_static()
        {
            SomeTestRecord_static someInstance = new SomeTestRecord_static();

            void ChangeADict(int threadId)
            {
                someInstance.Dict["someSpecificKey"] = threadId.ToString();
            }

            new ThreadingChecker(ChangeADict).Run(iterCount: 10000);
        }

        [TestMethod]
        public void WithAlsoShouldWork1()
        {
            var personJohnSmith = new Person("John", "Smith");
            var personTonySmith = personJohnSmith with { FirstName = "Tony" };
            Assert.AreEqual("John Smith", personJohnSmith.FullName);
            Assert.AreEqual("Tony Smith", personTonySmith.FullName);
        }

        [TestMethod]
        public void WithAlsoShouldWork2()
        {
            var personJohnSmith = new Person("John", "Smith");
            Assert.AreEqual("John Smith", personJohnSmith.FullName);
            var personTonySmith = personJohnSmith with { FirstName = "Tony" };
            Assert.AreEqual("Tony Smith", personTonySmith.FullName);
        }
    }
}
