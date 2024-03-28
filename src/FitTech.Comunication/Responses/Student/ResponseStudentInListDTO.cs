namespace FitTech.Comunication.Responses.Student
{
    public class ResponseStudentInListDTO
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string PlanName { get; set; }

        public bool PlanIsActive { get; set; }

        public DateTime PlanExpirationDate { get; set; }
    }
}
