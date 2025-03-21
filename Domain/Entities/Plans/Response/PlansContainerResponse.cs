﻿namespace Domain.Entities.Plans.Response
{
    public class PlansContainerResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }

        //public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public List<PlanResponse>? plans { get; set; }

    }
}
