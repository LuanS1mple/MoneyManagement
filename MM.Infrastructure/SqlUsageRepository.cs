using Entities.Models;
using Microsoft.EntityFrameworkCore;
using MM.Usecase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.Infrastructure
{
    public class SqlUsageRepository : IUsageRepository
    {
        public void AddUsage(Usage usage)
        {
            MoneyManagementContext.Ins.Usages.Add(usage);
            MoneyManagementContext.Ins.SaveChanges();
        }

        public void Delete(Usage usage)
        {
            usage.Enable = false;
            MoneyManagementContext.Ins.Usages.Update(usage);
            MoneyManagementContext.Ins.SaveChanges();
        }

        public List<Usage> GetAll()
        {
            return MoneyManagementContext.Ins.Usages.Include(s=> s.Type).Where(s=> s.Enable.Value).ToList();
        }

        public Usage GetById(int id)
        {
            return MoneyManagementContext.Ins.Usages.Where(s => s.Id == id).FirstOrDefault()!;
        }

        public int GetByName(string name)
        {
            return MoneyManagementContext.Ins.Usages.Where(s=>s.Name.Equals(name)).FirstOrDefault()!.Id;
        }

        public List<Usage> GetExpenditureUsage()
        {
            return MoneyManagementContext.Ins.Usages.Include(s => s.Type).Where(s => s.TypeId == 2).ToList();

        }

        public List<Usage> GetRevenueUsage()
        {
            return MoneyManagementContext.Ins.Usages.Include(s=>s.Type).Where(s => s.TypeId == 1).ToList();
        }
    }
}
