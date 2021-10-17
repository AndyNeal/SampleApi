using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi
{
    public interface ITinyUrlStore
    {
        public TinyUrlResponse Create(TinyUrl input);
        public TinyUrlResponse Read(string key);
        public TinyUrlResponse Update(TinyUrl input);
        public TinyUrlResponse Delete(string key);
        public void Purge();
    }
}
