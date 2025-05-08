using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.Usecase
{
    public interface ICustomerRepository
    {
        public bool isCorrectAccount(string username,string password);
        public Customer GetByAccount(string username);
        public Customer GetById(int id);
        public void AddDeposit(int amount,int customerId);
    }
}
