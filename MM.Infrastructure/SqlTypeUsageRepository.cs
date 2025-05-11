using Entities.Models;
using MM.Usecase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.Infrastructure
{
    public class SqlTypeUsageRepository : ITypeUsageRepository
    {
        public List<TypeUsage> GetAllTypes()
        {
            return MoneyManagementContext.Ins.TypeUsages.ToList();
        }
    }
}
