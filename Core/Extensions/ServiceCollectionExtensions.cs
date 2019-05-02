using Autofac;
using Autofac.Core;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Module = Autofac.Module;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static IContainer _container;
        /// <summary>
        /// Her katmanın çözümleme modülleri IServiceCollection'a ekleniyor.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection service, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(service);
            }
            return ServiceTool.Create(service);
        }
        /// <summary>
        /// Daha önce IServiceCollection tarafında yapılan çözümlemeleri dolduruyor.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutofacDependencyResolvers(this IServiceCollection services, Module[] modules)
        {

            var builder = new ContainerBuilder();
            builder.Populate(services);

            foreach (var module in modules)
            {
                builder.RegisterModule(module);
            }

            _container = builder.Build();

            return services;
        }

        public static IServiceProvider CreateAutofacServiceProvider(this IServiceCollection services)
        {
            return new AutofacServiceProvider(_container);
        }
    }
}
