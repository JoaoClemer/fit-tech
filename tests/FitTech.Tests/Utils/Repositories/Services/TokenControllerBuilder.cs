using FitTech.Application.Services.Token;

namespace FitTech.Tests.Utils.Repositories.Services
{
    public class TokenControllerBuilder
    {
        public static TokenController Instance()
        {
            return new TokenController(1000, "MkFSVzYpRDFfIUVwIjd8akVTVGdAdX5+a0R3dHhWa2Z6VHhcKHQmeQ==");
        }
    }
}
