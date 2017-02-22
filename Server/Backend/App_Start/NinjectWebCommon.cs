// <copyright file="NinjectWebCommon.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Web;
using System.Web.Http;
using Backend.Schemas;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.WebApi;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(AgencyRM.Tracker.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(AgencyRM.Tracker.App_Start.NinjectWebCommon), "Stop")]

namespace AgencyRM.Tracker.App_Start
{
    public static class NinjectWebCommon
    {
#pragma warning disable SA1311 // Static readonly fields must begin with upper-case letter
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
#pragma warning restore SA1311 // Static readonly fields must begin with upper-case letter

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));

            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<DatabaseContext>()
                .ToConstructor((ctx) => new DatabaseContext("DevDatabase"))
                .InRequestScope();
        }
    }
}