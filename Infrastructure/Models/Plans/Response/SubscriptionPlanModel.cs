using Domain.ShareData.Base;
using Infrastructure.DataSource.Seeds.Models;
using Infrastructure.Models.User;

namespace Infrastructure.Models.Plans
{
  public  class SubscriptionPlanModel : BaseSubscriptionPlan
    {
        public new UserModel? User { get; set; }
        public new List<PlanFeatureModel>? Features { get; set; }
        public new List<Domain.ShareData.Base.Service>? Services { get; set; }

    }






}
