using FitTech.Domain.Enum;

namespace FitTech.Domain.Entities
{
    public class Student : User
    {
        public int RegistrationNumber { get; set; }
        public StudentPlan? StudentPlan { get; set; }
        public Traning? Traning { get; set; }

        public override UserType GetUserType()
        {
            return UserType.Student;
        }
    }
}
