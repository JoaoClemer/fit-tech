using FitTech.Application.Services.Token;

namespace FitTech.Tests.Utils.Repositories.Services
{
    public class TokenControllerBuilder
    {
        public static TokenController Instance()
        {
            return new TokenController(1000, "MkFSVzYpRDFfIUVwIjd8akVTVGdAdX5+a0R3dHhWa2Z6VHhcKHQmeQ==");
        }
        public static TokenController InstanceExpiredToken()
        {
            return new TokenController(0.0166667, "b2Q3NFNSaFdCclpZZjlBNkl3SU9oS2Z6UlE4UVJoQzU5RDdJZXJlbHk2WkZGbnExWnQ=");
        }
    }
}
