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
    public class SqlExpenditureRepository : IExpenditureRepository
    {
        public void Add(Expenditure expense)
        {
            MoneyManagementContext.Ins.Expenditures.Add(expense);
            MoneyManagementContext.Ins.SaveChanges();
        }

        public List<Expenditure> Expenditures()
        {
            return MoneyManagementContext.Ins.Expenditures.Include(s=>s.Jar).Include(s=>s.Usage).ToList();
        }
    }
}
