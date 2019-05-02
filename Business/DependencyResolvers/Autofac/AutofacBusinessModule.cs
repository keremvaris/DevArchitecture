using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.BackgroundJobs;
using Core.BackgroundJobs.HangFire;
using Core.Utilities.Interceptors;
using Core.Utilities.Mail;
using Core.Utilities.Mail.FakeMail;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
            builder.RegisterType<FakeMailService>().As<IMailService>().SingleInstance();
            builder.RegisterType<NotificationService>().As<INotificationService>().SingleInstance();
            builder.RegisterType<HangFireBackgroundWorker>().As<IBackgroundWorker>().SingleInstance();
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly)
              .AsImplementedInterfaces()
              .EnableInterfaceInterceptors(new ProxyGenerationOptions()
              {
                  Selector = new AspectInterceptorSelector()
              })
              .SingleInstance();
        }
    }
}
