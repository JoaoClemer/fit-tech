namespace FitTech.Comunication.Responses.Student
{
    public class ResponseGetAllStudentsOfGymDTO
    {
        public string Name { get; set; }

        public string PlanName { get; set; }

        public bool PlanIsActive { get; set; }

        public DateTime PlanExpirationDate { get; set; }
    }
}
