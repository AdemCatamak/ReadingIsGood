using System.Reflection;

namespace RIG.Shared.Infrastructure.MassTransitComponents
{
    public interface IIntegrationMessageConsumerAssembly
    {
        public Assembly Assembly { get; }
    }
}