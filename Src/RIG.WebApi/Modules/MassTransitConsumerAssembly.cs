using System.Reflection;
using RIG.Shared.Infrastructure.MassTransitComponents;

namespace RIG.WebApi.Modules
{
    public class MassTransitConsumerAssembly : IIntegrationMessageConsumerAssembly
    {
        public Assembly Assembly => this.GetType().Assembly;
    }
}