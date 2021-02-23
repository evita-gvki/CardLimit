using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLimit.Core.Model;
using CardLimit.Core.Services.Options;

namespace CardLimit.Core.Services
{
    public interface ILimitService
    {
        public Task<Result<Limit>> FindLimitAsync(FindLimitOptions options);
        public Task<Result<Limit>> InitLimitAsync(CreateLimitOptions options);
    }
}
