namespace KioskServiceApp.Models
{
    public class ServiceAssignmentStats
    {
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public int TotalThisYear { get; set; }
        public int TotalThisMonth { get; set; }
        public int TotalThisWeek { get; set; }
    }

}
