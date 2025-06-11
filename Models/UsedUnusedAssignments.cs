namespace KioskServiceApp.Models
{
    public class UsedUnusedAssignments
    {
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public int Used { get; set; }
        public int Unused { get; set; }
    }

}
