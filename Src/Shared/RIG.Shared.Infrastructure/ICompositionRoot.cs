using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RIG.Shared.Infrastructure
{
    public interface ICompositionRoot
    {
        void Register(IServiceCollection services, IConfiguration configuration);
    }
}