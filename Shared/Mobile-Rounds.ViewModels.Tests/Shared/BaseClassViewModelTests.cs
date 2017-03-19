using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mobile_Rounds.ViewModels.Platform;
using Mobile_Rounds.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Tests.Shared
{

    class TestAPI : IApiRequest
    {
        public Task<TResult> GetAsync<TResult>(string uri) where TResult : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TResult> PostAsync<TResult>(string uri, object dataToSend) where TResult : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TResult> PutAsync<TResult>(string uri, object updatedData) where TResult : class, new()
        {
            throw new NotImplementedException();
        }
    }

    class TestVM : BaseViewModel
    {

    }

    class TestImplVM : BaseViewModel
    {
        protected override Task FetchDataAsync()
        {
            return Task.Run(() => { });
        }
    }

    [TestClass]
    public class BaseClassViewModelTests
    {
        [TestInitialize]
        public void Start()
        {
            //register dummy api request type.
            ServiceResolver.Register<IApiRequest>(() => new TestAPI());
        }

        [TestMethod]
        public void Sets_Correct_Defaults()
        {
            var vm = new TestVM();
            Assert.IsNotNull(vm.CrumbCommand);
            Assert.IsNotNull(vm.GoToAdmin);
            Assert.AreEqual(0, vm.Crumbs.Count);
            Assert.IsFalse(vm.IsLoading);
            Assert.IsFalse(vm.IsAdmin);
            Assert.IsNotNull(vm.Api);
            Assert.IsInstanceOfType(vm.Api, typeof(TestAPI));
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public async Task LoadAsync_Throws()
        {
            var vm = new TestVM();
            await vm.LoadDataAsync();
        }

        [TestMethod]
        public async Task LoadAsync_Toggles_Loading()
        {
            var vm = new TestVM();
            try
            {
                await vm.LoadDataAsync();
            }
            catch { }
            Assert.IsTrue(vm.IsLoading);
        }

        [TestMethod]
        public async Task LoadAsync_Toggles_Loading_On_Complete()
        {
            var vm = new TestImplVM();
            await vm.LoadDataAsync();
            Assert.IsFalse(vm.IsLoading);
        }
    }
}
