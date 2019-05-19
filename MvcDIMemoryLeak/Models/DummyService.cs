using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDIMemoryLeak.Models
{
    public class DummyService
    {
        private byte[] data = new byte[10 * 1000 * 1000];
        public int Id => 5;
    }
}