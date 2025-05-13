using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.Usecase
{
    public interface IRevenueRepository
    {
        public List<Revenue> Revenues();
        public void Add(Revenue revenue);
    }
}
