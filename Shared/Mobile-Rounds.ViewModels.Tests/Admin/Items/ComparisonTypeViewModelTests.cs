using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mobile_Rounds.ViewModels.Admin.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Tests.Admin.Items
{
    [TestClass]
    public class ComparisonTypeViewModelTests
    {
        private const string LessThan = "Less Than";
        private const string LessThanOrEqual = "Less Than Or Equal To";
        private const string GreaterThan = "Greater Than";
        private const string GreaterThanOrEqual = "Greater Than Or Equal To";
        private const string EqualTo = "Equal To";
        private const string Between = "Between";
        private const string Either = "Either";

        [TestMethod]
        public void Static_Lists_Have_Correct_Strings()
        {
            var expected = new List<string>
            {
                LessThan, LessThanOrEqual, GreaterThan, GreaterThanOrEqual, EqualTo, Between, Either
            };

            Assert.AreEqual(expected.Count, ComparisonTypeViewModel.AllTypes.Count);
            Assert.AreEqual(expected.Count, ComparisonTypeViewModel.AllTypesAsViewModels.Count);

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], ComparisonTypeViewModel.AllTypes[i]);
            }
        }

        [TestMethod]
        public void Static_List_Of_VMs_Correct()
        {
            var expected = new List<string>
            {
                LessThan, LessThanOrEqual, GreaterThan, GreaterThanOrEqual, EqualTo, Between, Either
            };
            var vms = ComparisonTypeViewModel.AllTypesAsViewModels;

            Assert.AreEqual(expected.Count, vms.Count);

            for(int i = 0; i < expected.Count; i++)
            {
                var vm = vms[i];
                Assert.AreEqual(expected[i], vm.Name);
                if(i > 4)
                {
                    //two value check.
                    Assert.IsFalse(vm.UsesOneInput);
                    Assert.IsTrue(vm.UsesTwoInputs);
                }
                else
                {
                    //single checks.
                    Assert.IsTrue(vm.UsesOneInput);
                    Assert.IsFalse(vm.UsesTwoInputs);
                }
            }
        }

        [TestMethod]
        public void Validates_Less_Than_Ints()
        {
            var vm = ComparisonTypeViewModel.Locate(LessThan);
            Assert.IsTrue(vm.Validate("0", "1"));
            Assert.IsFalse(vm.Validate("1", "0"));
            Assert.IsFalse(vm.Validate("0", "0"));
        }

        [TestMethod]
        public void Validates_Less_Than_Or_Equal_Ints()
        {
            var vm = ComparisonTypeViewModel.Locate(LessThanOrEqual);
            Assert.IsTrue(vm.Validate("0", "1"));
            Assert.IsTrue(vm.Validate("1", "1"));
            Assert.IsFalse(vm.Validate("1", "0"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Validate_Cannot_Compare_String_And_Float_LT()
        {
            var vm = ComparisonTypeViewModel.Locate(LessThan);
            vm.Validate("abc", "0.5");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Validate_Cannot_Compare_String_And_Float_GT()
        {
            var vm = ComparisonTypeViewModel.Locate(GreaterThan);
            vm.Validate("abc", "0.5");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Validate_Cannot_Compare_String_And_Float_GTE()
        {
            var vm = ComparisonTypeViewModel.Locate(GreaterThanOrEqual);
            vm.Validate("abc", "0.5");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Validate_Cannot_Compare_String_And_Float_LTE()
        {
            var vm = ComparisonTypeViewModel.Locate(GreaterThanOrEqual);
            vm.Validate("abc", "0.5");
        }


        [TestMethod]
        public void Validates_Less_Than_Floats()
        {
            var vm = ComparisonTypeViewModel.Locate(LessThan);
            Assert.IsTrue(vm.Validate("0.2", "0.7"));
            Assert.IsFalse(vm.Validate("1.2", "0.5"));
            Assert.IsFalse(vm.Validate("0.5", "0.5"));
            Assert.IsFalse(vm.Validate(".5", "0.5"));
        }

        [TestMethod]
        public void Validates_Less_Than_Or_Equal_Floats()
        {
            var vm = ComparisonTypeViewModel.Locate(LessThanOrEqual);
            Assert.IsTrue(vm.Validate("0.2", "0.7"));
            Assert.IsTrue(vm.Validate("0.5", "0.5"));
            Assert.IsTrue(vm.Validate(".5", "0.5"));
            Assert.IsFalse(vm.Validate("1.2", "0.5"));
        }

        [TestMethod]
        public void Validates_Between_Ints()
        {
            var vm = ComparisonTypeViewModel.Locate(Between);
            Assert.IsTrue(vm.Validate("1", min: "0", max: "2"));
            Assert.IsFalse(vm.Validate("0", min: "1", max: "0"));
            Assert.IsTrue(vm.Validate("0", min: "0", max: "1"));
        }

        [TestMethod]
        public void Validates_Between_Floats()
        {
            var vm = ComparisonTypeViewModel.Locate(Between);
            Assert.IsTrue(vm.Validate("1.5", min: "0.3", max: "2.3"));
            Assert.IsFalse(vm.Validate("0.1", min: "0.75", max: "0.8"));
            Assert.IsTrue(vm.Validate("0.1", min: "0", max: "0.11"));
        }

        [TestMethod]
        public void Validates_Either_Or()
        {
            var vm = ComparisonTypeViewModel.Locate(Either);
            //Assert.IsTrue(vm.Validate("true", min: "True", max: "False"));
            //Assert.IsTrue(vm.Validate("false", min: "True", max: "False"));
            Assert.IsFalse(vm.Validate("derp", min: "True", max: "False"));

            //Assert.IsTrue(vm.Validate("0.1", min: "0.1", max: "0.8"));
            //Assert.IsTrue(vm.Validate("0.8", min: "0.1", max: "0.8"));
            //Assert.IsFalse(vm.Validate("0.8", min: "0.2", max: "0.3"));
        }
    }
}
