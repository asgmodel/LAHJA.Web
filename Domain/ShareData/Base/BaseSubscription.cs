﻿namespace Domain.ShareData.Base
{
    public class BaseSubscription
    {

        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? PlanId { get; set; }
        public long? Nr { get; set; }
        public string? CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public string? Status { get; set; }
        public string? BillingPeriod { get; set; }
        public DateTime? CancelAt { get; set; }
        public bool? CancelAtPeriodEnd { get; set; }
     


    }
}
