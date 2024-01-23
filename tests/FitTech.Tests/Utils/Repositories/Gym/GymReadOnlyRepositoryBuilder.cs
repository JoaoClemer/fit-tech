using Bogus;
using Bogus.DataSets;
using FitTech.Domain.Repositories.Gym;
using Moq;

namespace FitTech.Tests.Utils.Repositories.Gym
{
    public class GymReadOnlyRepositoryBuilder
    {
        private static GymReadOnlyRepositoryBuilder _instance;
        private readonly Mock<IGymReadOnlyRepository> _repository;

        private GymReadOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IGymReadOnlyRepository>();
            }
        }

        public static GymReadOnlyRepositoryBuilder Instance()
        {
            _instance = new GymReadOnlyRepositoryBuilder();
            return _instance;
        }

        public GymReadOnlyRepositoryBuilder GetGymByEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
                _repository.Setup(i => i.GetGymByEmail(email)).ReturnsAsync(new Domain.Entities.Gym());

            return this;
        }

        public GymReadOnlyRepositoryBuilder GetGymByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                _repository.Setup(i => i.GetGymByName(name)).ReturnsAsync(new Domain.Entities.Gym());

            return this;
        }

        public GymReadOnlyRepositoryBuilder GetGymById(int id)
        {
            if (id > 0)
                _repository.Setup(i => i.GetGymById(id)).ReturnsAsync(new Domain.Entities.Gym
                {
                    Id = id,
                    Name = "Gym"
                });

            return this;
        }

        public IGymReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
