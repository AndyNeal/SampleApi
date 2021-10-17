using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi
{
    public class TinyUrlResponse
    {
        public bool Success { get; set; } = true;
        public string Error { get; set; }
        public TinyUrl Record { get; set; }
    }
}
