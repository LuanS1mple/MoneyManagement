using Entities.Models;

namespace MM.HostApp.Models
{
    public class ResponseExpenditure
    {
        public List<Expenditure> Expenditures { get; set; }
        public string Message { get; set; }
        public List<Jar> ExpenditureJar { get; set; }
        public List<Usage> ExpenditureUsage { get; set; }
 
    }
}
