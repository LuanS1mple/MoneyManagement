using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.Usecase
{
    public class SqlCustomerRepository : ICustomerRepository
    {
        public bool isCorrectAccount(string username, string password)
        {
            return MoneyManagementContext.Ins.Customers.Where(s=>s.Username.Equals(username) && s.Password.Equals(password)).Any();

        }
        public Customer GetByAccount(string username)
        {
            return MoneyManagementContext.Ins.Customers.Where(s => s.Username.Equals(username)).FirstOrDefault()!;
        }

        public void AddDeposit(int amount)
        {

        }

        public Customer GetById(int id)
        {
            return MoneyManagementContext.Ins.Customers.Where(s => s.Id == id).FirstOrDefault();
        }

        public void AddDeposit(int amount, int customerId)
        {
            Customer customer = GetById(customerId);
            customer.Deposit += amount;
            MoneyManagementContext.Ins.Customers.Update(customer);
            MoneyManagementContext.Ins.SaveChanges();
        }
    }
}
