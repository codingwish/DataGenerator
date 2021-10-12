using Microsoft.Extensions.DependencyInjection;
using System;

namespace DataGenerator.Core.Container.Infrastructure
{
    public interface IServiceContainer
    {
        IServiceCollection ServiceCollection { get; }

        IServiceProvider ServiceProvider { get; set; }

        void Register<TContract, TImplementation>(ServiceImplementation serviceImplementation)
            where TImplementation : TContract;
    }
}