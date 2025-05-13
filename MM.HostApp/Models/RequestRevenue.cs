namespace MM.HostApp.Models
{
    public class RequestRevenue
    {
        public int UsageId { get; set; }
        public string NewRevenue { get; set; }
        public int Amount { get; set; }
        public DateOnly Date { get; set; }
        public int JarId { get; set; }
        public string Note { get; set; }
    }
}
