using FitTech.Comunication.Requests.Gym;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTech.Application.UseCases.Gym.Create
{
    public interface ICreateGymUseCase
    {
        Task Execute(RequestCreateGymDTO request);
    }
}
