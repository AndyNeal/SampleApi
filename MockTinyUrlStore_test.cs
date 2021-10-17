using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi
{
    [TestClass]
    public class MockTinyUrlStore_test
    {
        MockTinyUrlStore _store;
        public MockTinyUrlStore_test()
        {
            _store = new MockTinyUrlStore();
        }

        [TestInitialize]
        public void Setup()
        {
            _store.Create(new TinyUrl()
            { 
                Key = "GOOG",
                Url = "https://www.google.com"
            }) ;
        }

        [TestCleanup]
        public void TearDown()
        {
            _store.Purge();
        }

        [TestMethod]
        public void Get_Known()
        {
            var item = _store.Read("GOOG");
            Assert.IsTrue(item.Success);
            Assert.AreEqual(item.Record.Url, "https://www.google.com");
        }

        [TestMethod]
        public void Get_Unknown()
        {
            var item = _store.Read("fake");
            Assert.IsFalse(item.Success);
        }

        [TestMethod]
        public void Add_Value_With_Key()
        {
            var sent = _store.Create(new TinyUrl() 
            {  
                Key = "MSN",
                Url = "https://www.msn.com"
            });
            Assert.IsTrue(sent.Success);
            var received = _store.Read("MSN");
            Assert.IsTrue(received.Success);
            Assert.AreEqual(received.Record.Url, "https://www.msn.com");
        }

        [TestMethod]
        public void Add_Value_Without_Key()
        {
            var sent = _store.Create(new TinyUrl()
            {
                Url = "https://www.msn.com"
            });
            Assert.IsTrue(sent.Success);
            var received = _store.Read(sent.Record.Key);
            Assert.IsTrue(received.Success);
            Assert.AreEqual(sent.Record.Url, received.Record.Url);
        }

        [TestMethod]
        public void Update_Record()
        {
            var updated = _store.Update(new TinyUrl() 
            {
                Key = "GOOG",
                Url = "https://gmail.com"
            });
            var recieved = _store.Read("GOOG");
            Assert.IsTrue(recieved.Success);
            Assert.AreEqual(updated.Record.Url, recieved.Record.Url);
        }

        [TestMethod]
        public void Delete_Record()
        {
            _store.Delete("GOOG");
            var received = _store.Read("GOOG");
            Assert.IsFalse(received.Success);
        }
    }
}
