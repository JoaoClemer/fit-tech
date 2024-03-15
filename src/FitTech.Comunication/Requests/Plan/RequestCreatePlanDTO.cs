using FitTech.Comunication.Enum;

namespace FitTech.Comunication.Requests.Plan
{
    public class RequestCreatePlanDTO
    {
        public int GymId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public PlanTypeDTO PlanType { get; set; }
    }
}
