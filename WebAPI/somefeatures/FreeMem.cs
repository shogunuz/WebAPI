using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.somefeatures
{
    public class FreeMem
    {
        public static void CollectMethod()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
