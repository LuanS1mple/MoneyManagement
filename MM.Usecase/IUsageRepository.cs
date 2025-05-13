using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.Usecase
{
    public interface IUsageRepository
    {
        public List<Usage> GetAll();
        public void AddUsage(Usage usage);
        public Usage GetById(int id);
        public void Delete(Usage usage);
        public List<Usage> GetRevenueUsage();

        public List<Usage> GetExpenditureUsage();
        public int GetByName(string name);
    }
}
