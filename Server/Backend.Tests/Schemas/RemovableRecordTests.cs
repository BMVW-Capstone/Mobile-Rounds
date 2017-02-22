using Backend.Schemas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Tests.Schemas
{
    public class TestRemovableTable : RemovableRecord
    {

    }

    [TestClass]
    public class RemovableRecordTests
    {
        [TestMethod, TestCategory("Schemas")]
        public void SetsNotMarkedAsDeletedAsDefault()
        {
            var testTable = new TestRemovableTable();
            Assert.IsFalse(testTable.IsMarkedAsDeleted);
        }
    }
}
