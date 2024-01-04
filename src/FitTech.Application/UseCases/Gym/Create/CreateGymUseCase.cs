using FitTech.Comunication.Requests.Gym;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTech.Application.UseCases.Gym.Create
{
    public class CreateGymUseCase
    {
        public void Execute(RequestCreateGymDTO request)
        {

        }

        private void Validate(RequestCreateGymDTO request)
        {
            var validator = new CreateGymValidator();

            var result = validator.Validate(request);

            if(!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage);
                throw new Exception();
            }
        }
    }
}
