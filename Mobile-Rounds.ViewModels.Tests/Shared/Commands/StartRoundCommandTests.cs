using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mobile_Rounds.ViewModels.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Tests.Shared.Commands
{
    [TestClass]
    public class StartRoundCommandTests
    {
        [TestMethod]
        public void CanExecuteIsTrue()
        {
            var cmd = new StartRoundCommand();
            Assert.IsTrue(cmd.CanExecute(null));
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ExecuteThrowsNotImplemented()
        {
            var cmd = new StartRoundCommand();
            cmd.Execute(null);
        }
    }
}
