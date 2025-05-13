using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.Usecase
{
    public interface IExpenditureRepository
    {
        public List<Expenditure> Expenditures();
        public void Add(Expenditure expense);
    }
}
