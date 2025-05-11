using Entities.Models;

namespace MM.HostApp.Models
{
    public class UsageResponse
    
    {
        public List<Usage> usages {  get; set; }
        public string Message { get; set; }
        public List<TypeUsage> types { get; set; }

    }

}
