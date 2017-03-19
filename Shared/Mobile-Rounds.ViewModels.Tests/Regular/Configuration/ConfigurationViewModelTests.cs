using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mobile_Rounds.ViewModels.Platform;
using Mobile_Rounds.ViewModels.Regular.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Tests.Regular.Configuration
{
    class TestSettings : ISettings
    {
        private object toReturn;
        private string key;

        public TestSettings(string expectedKey, object val)
        {
            key = expectedKey;
            toReturn = val;
        }
        public TReturn GetValue<TReturn>(string key)
        {
            Assert.AreEqual(this.key, key);
            return (TReturn)toReturn;
        }

        public void SaveValue(string key, object value)
        {
            Assert.AreEqual(this.key, key);
            toReturn = value;
        }
    }

    [TestClass]
    public class ConfigurationViewModelTests
    {
        [TestInitialize]
        public void Start()
        {
            ServiceResolver.Register<IApiRequest>(() => null);
        }
        [TestMethod]
        public void Sets_Default_Values()
        {
            ServiceResolver.Register<ISettings>(() => new TestSettings(
                Constants.APIHostConfigKey, "http://api.com"));

            var vm = new ConfigurationViewModel();
            Assert.AreEqual(1, vm.Crumbs.Count);
            Assert.AreEqual(vm.Crumbs.ElementAt(0).Title, "Settings");
            Assert.IsNotNull(vm.Save);
            Assert.AreEqual("http://api.com", vm.ApiHost);
        }
    }
}
