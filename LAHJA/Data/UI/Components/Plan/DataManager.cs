


 

namespace LAHJA.Data.UI.Components.Plan
{



    public static class DataManager
    {
        
        private static List<PlanInfo> planInfos = new List<PlanInfo>();

      
        private static DataGenerator dataGenerator = new DataGenerator();

       
        public static List<PlanInfo> GetAllPlanInfos()
        {
            
            if (planInfos.Count == 0)
            {
            
                planInfos = dataGenerator.GeneratePlanInfoList(3); 
            }

            return planInfos;
        }

     
        public static void AddPlanInfo(PlanInfo planInfo)
        {
            planInfos.Add(planInfo);
        }

        
        public static void UpdatePlanInfo(string id, PlanInfo updatedPlanInfo)
        {
            var existingPlan = planInfos.Find(p => p.Id == id);
            if (existingPlan != null)
            {

                existingPlan.Name = updatedPlanInfo.Name;
                existingPlan.PlanDescription = updatedPlanInfo.PlanDescription;
                existingPlan.Status = updatedPlanInfo.Status;
                existingPlan.TotalPrice = updatedPlanInfo.TotalPrice;
                existingPlan.TechnologyServices = updatedPlanInfo.TechnologyServices;
                existingPlan.ServiceDetailsList = updatedPlanInfo.ServiceDetailsList;
            }
        }


        public static PlanInfo Sherch(string id)
        {
            var existingPlan = planInfos.Find(p => p.Id == id);
            if (existingPlan != null)
            {
                return existingPlan;
            }
            return null;
        }
        public static void DeletePlanInfo(string id)
        {
            var planToDelete = planInfos.Find(p => p.Id == id);
            if (planToDelete != null)
            {
                planInfos.Remove(planToDelete);
            }
        }
    }
}
