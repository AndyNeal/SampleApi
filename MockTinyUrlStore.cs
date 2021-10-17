using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SampleApi
{
    public class MockTinyUrlStore : ITinyUrlStore
    {
        private List<TinyUrl> store;
        private static Random random = new Random();
        //private static RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();

        public MockTinyUrlStore()
        {
            store = new List<TinyUrl>();
        }

        public TinyUrlResponse Create(TinyUrl input)
        {
            if (input.Key == null)
            {
                input.Key = RandomString(6);
            }
            var result = new TinyUrlResponse();
            if (store.Where(x=> x.Key == input.Key).Any())
            {
                result.Success = false;
                result.Error = "Key already in use";
            }
            else
            {
                store.Add(input);
                result.Record = input;
            }
            
            return result;
        }
        public TinyUrlResponse Read(string key)
        {
            var result = new TinyUrlResponse();
            var records = store.Where(x => x.Key == key);
            if (records.Count() != 1)
            {
                result.Success = false;
                result.Error = "Single element not found matching key";
            }
            else
            {
                result.Record = records.First();
            }
            return result;
        }
        public TinyUrlResponse Update(TinyUrl input)
        {
            var result = new TinyUrlResponse();
            if (store.Where(x => x.Key == input.Key).Count() != 1)
            {
                result.Success = false;
                result.Error = "Single element not found matching key";
            }
            else
            {
                var record = store.Single(x => x.Key == input.Key);
                record.Url = input.Url;
                result.Record = record;
            }
            return result;
        }

        public TinyUrlResponse Delete(string key)
        {
            var result = new TinyUrlResponse();
            if (store.Where(x => x.Key == key).Count() != 1)
            {
                result.Success = false;
                result.Error = "Single element not found matching key";
            }
            else
            {
                var record = store.Single(x => x.Key == key);
                result.Record = record;
                store.Remove(record);
            }
            return result;
        }

        public void Purge()
        {
            store = new List<TinyUrl>();
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvqxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
