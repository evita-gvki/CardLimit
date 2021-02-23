using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLimit.Core.Constants
{
    public static class ResultCode
    {
        public const int Success = 200;
        public const int BadRequest = 400;
        public const int NotFound = 404;
        public const int InternalServerError = 500;
    }
}
