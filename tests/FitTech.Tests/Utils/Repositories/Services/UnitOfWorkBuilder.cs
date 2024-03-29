﻿using FitTech.Domain.Repositories;
using Moq;

namespace FitTech.Tests.Utils.Repositories.Services
{
    public class UnitOfWorkBuilder
    {
        private static UnitOfWorkBuilder _instance;
        private readonly Mock<IUnitOfWork> _repository;

        private UnitOfWorkBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IUnitOfWork>();
            }
        }

        public static UnitOfWorkBuilder Instance()
        {
            _instance = new UnitOfWorkBuilder();
            return _instance;
        }

        public IUnitOfWork Build()
        {
            return _repository.Object;
        }
    }
}
