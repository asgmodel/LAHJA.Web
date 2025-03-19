namespace LAHJA.Data.UI.Components.Plan
{
    public class PlanInfo : BaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string IdService { get; set; }
        public string PlanDescription { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public List<TechnologyService> TechnologyServices { get; set; } = new List<TechnologyService>();
        public List<DigitalService> ServiceDetailsList { get; set; } = new List<DigitalService>();
    }
}
