using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Platform
{
    /// <summary>
    /// A way to resolve services per platform. To register a service, 
    /// call the <see cref="Register{TService}(Func{TService})"/> method. Typically,
    /// this is called from the App.xaml.cs file to register services when the application loads.
    /// </summary>
    public static class ServiceResolver
    {
        /// <summary>
        /// Internal cache of service objects.
        /// </summary>
        private static Dictionary<Type, object> services;

        static ServiceResolver()
        {
            // create our initial cache.
            services = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Registers a function to call when the service is requested.
        /// </summary>
        /// <typeparam name="TService">The service type to register.</typeparam>
        /// <param name="register">The method to call when registering.</param>
        public static void Register<TService>(Func<TService> register)
        {
            //get the platform service, then add it to the cache.
            var service = register();

            //save on reflection cost by casting once.
            Type serviceType = typeof(TService);

            if (services.ContainsKey(serviceType))
            {
                //service already registered, so update rather than add.
                services[serviceType] = service;
            }
            else
            {
                //just add the service
                services.Add(serviceType, service);
            }
        }

        /// <summary>
        /// Resolves a given platform service to a hard object.
        /// </summary>
        /// <typeparam name="TService">The service type to fetch.</typeparam>
        /// <returns>The service object that was registered, if any. This can 
        /// throw exceptions if the service was not registered prior to calling.</returns>
        public static TService Resolve<TService>()
        {
            //get the service from the dictionary. If there is no 
            //service cached, let the user handle the error.
            return (TService)services[typeof(TService)];
        }
    }
}
