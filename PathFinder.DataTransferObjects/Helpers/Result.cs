using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.DataTransferObjects.Helpers
{
    public class Result
    {
        public bool Status { get; set; }
    }

    public class Result<T> where T : class
    {
        public T Value { get; set; }
    }
}
