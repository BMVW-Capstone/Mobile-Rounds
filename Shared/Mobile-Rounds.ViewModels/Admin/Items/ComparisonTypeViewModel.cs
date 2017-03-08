using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Admin.Items
{
    public class ComparisonTypeViewModel
    {
        public static List<string> AllTypes { get; private set; }
        public static List<ComparisonTypeViewModel> AllTypesAsViewModels { get; private set; }
        private static List<string> SingleInputs { get; set; }
        private static List<string> DoubleInputs { get; set; }


        private const string LessThan = "Less Than";
        private const string LessThanOrEqual = "Less Than Or Equal To";
        private const string GreaterThan = "Greater Than";
        private const string GreaterThanOrEqual = "Greater Than Or Equal To";
        private const string EqualTo = "Equal To";
        private const string Between = "Between";
        private const string Either = "Either";

        public static ComparisonTypeViewModel Locate(string name)
        {
            return AllTypesAsViewModels
                .FirstOrDefault(i => i.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

        static ComparisonTypeViewModel()
        {
            AllTypes = new List<string>();
            AllTypesAsViewModels = new List<ComparisonTypeViewModel>();

            SingleInputs = new List<string>()
            {
                LessThan, LessThanOrEqual, GreaterThan, GreaterThanOrEqual, EqualTo
            };

            DoubleInputs = new List<string>()
            {
                Between, Either
            };

            AllTypes.AddRange(SingleInputs);
            AllTypes.AddRange(DoubleInputs);

            foreach (var type in AllTypes)
            {
                AllTypesAsViewModels.Add(new ComparisonTypeViewModel(type));
            }
        }

        private ComparisonTypeViewModel(string comparisonType)
        {
            Name = comparisonType;
            UsesOneInput = SingleInputs.Contains(Name);
            UsesTwoInputs = DoubleInputs.Contains(Name);
        }

        public bool ValidateBoundOrder(string lowerBound, string upperBound)
        {
            var lowerBoundLowercase = lowerBound.ToLower();
            var upperBoundLowercase = upperBound.ToLower();

            return lowerBoundLowercase.CompareTo(upperBoundLowercase) < 0;
        }

        public bool ValidateWithinBounds(string lowerValue, string upperValue, string lowerBound, string upperBound)
        {
            var lowerValLowercase = lowerValue.ToLower();
            var upperValLowercase = upperValue.ToLower();
            var lowerBoundLowercase = lowerBound.ToLower();
            var upperBoundLowercase = upperBound.ToLower();

            if (this.Name == Either)
            {
                var check = lowerValLowercase.CompareTo(upperValLowercase) != 0;
                if (!check) return false;
            }
            return true;
        }

        public string Name { get; set; }
        public bool UsesOneInput { get; set; }
        public bool UsesTwoInputs { get; set; }
    }
}
