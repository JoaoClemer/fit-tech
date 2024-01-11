using AutoMapper;
using FitTech.Application.Services.AutoMapper;

namespace FitTech.Tests.Utils.Mapper
{
    public class MapperBuilder
    {
        public static IMapper Instance()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperConfiguration>();
            });

            return configuration.CreateMapper();
        }
    }
}
