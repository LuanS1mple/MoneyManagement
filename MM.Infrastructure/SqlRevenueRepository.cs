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
    public class SqlRevenueRepository : IRevenueRepository
    {
        public void Add(Revenue revenue)
        {
            MoneyManagementContext.Ins.Revenues.Add(revenue);
            MoneyManagementContext.Ins.SaveChanges();
        }

        public List<Revenue> Revenues()
        {
            return MoneyManagementContext.Ins.Revenues.Include(s=>s.Jar).Include(s=>s.Usage).ToList();
        }
    }
}
