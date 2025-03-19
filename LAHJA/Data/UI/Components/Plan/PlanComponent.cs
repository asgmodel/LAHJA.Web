using Microsoft.AspNetCore.Components;

namespace LAHJA.Data.UI.Components.Plan
{

    public class FeaturePlanInfo
    {
        public string? Id { get; set; }
        public string? ProductId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; } = true;
        public decimal Price { get; set; } = 2;

        public string? PriceDescription { get; set; }
        public bool IsFixed { get; set; } = true;

        public bool? IsPaid { get; set; } = true; 
        public int Quantity { get; set; } = 1;
        public decimal TotalPrice => Quantity * Price;


    }

    public class SubscriptionPlanInfo : FeaturePlanInfo
    {


        public string ClassImport { get; set; } = "plan-all-card";



         public string HeaderImport { get; set; } = "";
        public string? BillingPeriod { get; set; }
        public decimal? TotalBilling { get; set; } = 0;
        public decimal TotalAmount { get; set; } = 0;
        //public string? TotalAmountDescription { get; set; }
        public string? Token { get; set; }
        public string? Processor { get; set; }
        public string? ConnectionType { get; set; }
        public string? AbsolutePath { get; set; }
        public string? Image { get; set; }
        public List<string>? Images { get; set; }
        public string? ContainerId { get; set; }

        /// <summary>
        /// //////////////////////////////////////////////////////
        /// </summary>
        public decimal? MonthlyPrice { get; set; }
        public decimal? AnnualPrice { get; set; }
        public decimal? WeeklyPrice { get; set; }


        public new List<FeaturePlanInfo> Features { get; set; }

    }


}