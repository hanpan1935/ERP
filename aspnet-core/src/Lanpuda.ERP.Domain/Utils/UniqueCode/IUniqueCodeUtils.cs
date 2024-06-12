using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Lanpuda.ERP.Utils.UniqueCode
{
    public interface IUniqueCodeUtils : ITransientDependency
    {
        Task<string> GetUniqueNumberAsync(string prefix);
    }
}
