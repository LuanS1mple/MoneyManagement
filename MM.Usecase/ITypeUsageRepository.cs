using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.Usecase
{
    public interface ITypeUsageRepository
    {
        public List<TypeUsage> GetAllTypes();
    }
}
