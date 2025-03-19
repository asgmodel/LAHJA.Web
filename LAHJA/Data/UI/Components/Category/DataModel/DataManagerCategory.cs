

namespace LAHJA.Data.UI.Components.Category.DataModel
{
    public static class DataManagerCategory
    {
        private static List<CategoryComponent> planInfos = new List<CategoryComponent>();
        public static List<CategoryComponent> GetAllCategoryInfos()
        {
            CategoryComponent obj1 = new CategoryComponent()
            {
                Id="1",Name= "Data Storage",
                Description= "Provides secure cloud storage for your data.",
                ImageUrl= "/ai-hand.png"
            };
            CategoryComponent obj2 = new CategoryComponent()
            {

                Id = "2",
                Name = "Custom Branding",
                Description = "Provides secure cloud storage for your data.",
                ImageUrl = "/ai-hand.png",
               
            };
            CategoryComponent obj3 = new CategoryComponent()
            {
                Id = "3",
                Name = "Advanced Analytics",
                Description = "Provides secure cloud storage for your data.",
                ImageUrl = "/ai-hand.png",

            };
            planInfos.Add(obj1);
            planInfos.Add(obj2);
            planInfos.Add(obj3);

            return planInfos;
        }
    }
    
}

