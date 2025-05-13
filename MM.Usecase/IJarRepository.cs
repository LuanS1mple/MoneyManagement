using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.Usecase
{
    public interface IJarRepository
    {
        public List<Jar> GetAll(int customerId);
        public void Add(Jar jar);
        public void Delete(Jar jar);
        public Jar GetById(int id);

        public void AddToJarNumber(int  jarNumber,int amount);
        public int GetMaxOfJar(int jarId);
    }
}
