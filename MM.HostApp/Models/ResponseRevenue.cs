using Entities.Models;

namespace MM.HostApp.Models
{
    public class ResponseRevenue
    {
        public List<Revenue> Revenues { get; set; }
        public List<Usage> UsageRevenues { get; set; }
        public List<Jar> Jars { get; set; }
        public string Message { get; set; }
    }
}
