using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mobile_Rounds.ViewModels.Shared.Commands;
using Mobile_Rounds.ViewModels.Shared.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Tests.Shared.Home
{
    [TestClass]
    public class HomePageViewModelTests
    {
        [TestMethod]
        public void ConstructorSetsCorrectDefaults()
        {
            var vm = new HomePageViewModel();

            Assert.IsNotNull(vm.StartRound);
            Assert.IsNotNull(vm.Sync);
        }

        [TestMethod]
        public void ConstructorSetsCorrectCommandTypes()
        {
            var vm = new HomePageViewModel();

            Assert.IsInstanceOfType(vm.StartRound, typeof(StartRoundCommand));
            Assert.IsInstanceOfType(vm.Sync, typeof(AsyncCommand));
        }
    }
}
