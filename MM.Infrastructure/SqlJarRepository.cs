using Entities.Models;
using MM.Usecase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.Infrastructure
{
    public class SqlJarRepository : IJarRepository
    {
        public void Add(Jar jar)
        {
            var repo = MoneyManagementContext.Ins;
            repo.Jars.Add(jar);
            repo.SaveChanges();
        }

        public void Delete(Jar jar)
        {
            var repo = MoneyManagementContext.Ins;
            repo.Jars.Remove(jar);
            repo.SaveChanges();
        }

        public List<Jar> GetAll(int id)
        {
            var repo = MoneyManagementContext.Ins;
            return repo.Jars.Where(s=>s.CustomerId==id).ToList(); 
        }

        public Jar GetById(int id)
        {
            var repo = MoneyManagementContext.Ins;
            return repo.Jars.Where(s => s.Id == id).FirstOrDefault();
        }
    }
}
