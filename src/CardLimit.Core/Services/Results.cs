using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLimit.Core.Services
{
    public class Result<T>
    {
        public string ErrorMessage { get; set; }
        public int Code { get; set; }
        public T Data { get; set; }
    }
}

