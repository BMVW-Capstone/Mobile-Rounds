using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mobile_Rounds.ViewModels.Shared.Commands;

namespace Mobile_Rounds.ViewModels.Tests.Shared.Commands
{
    [TestClass]
    public class SyncCommandTests
    {
        [TestMethod]
        public void CanExecuteIsTrue()
        {
            var cmd = new SyncCommand();
            Assert.IsTrue(cmd.CanExecute(null));
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ExecuteThrowsNotImpelemnted()
        {
            var cmd = new SyncCommand();
            cmd.Execute(null);
        }
    }
}
