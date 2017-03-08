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

        private int LowerCompare(string left, string right)
        {
            var leftLowercase = left.ToLower();
            var rightLowercase = right.ToLower();
            return left.CompareTo(right);
        }

        private bool ValidateLessThan(string value, string bound)
        {
            return LowerCompare(value, bound) < 0;
        }

        private bool ValidateLessThanOrEqual(string value, string bound)
        {
            return LowerCompare(value, bound) <= 0;
        }

        private bool ValidateEqualTo(string value, string bound)
        {
            return LowerCompare(value, bound) == 0;
        }
        private bool ValidateGreaterThan(string value, string bound)
        {
            return LowerCompare(value, bound) > 0;
        }

        private bool ValidateGreaterThanOrEqual(string value, string bound)
        {
            return LowerCompare(value, bound) >= 0;
        }

        private bool ValidateEither(string value, string min, string max)
        {
            return LowerCompare(value, min) == 0 || LowerCompare(value, max) == 0;
        }

        private bool ValidateBetween(string value, string min, string max)
        {
            return ValidateGreaterThanOrEqual(value, min) && ValidateLessThanOrEqual(value, max);
        }

        public bool ValidateWithinBounds(string value, string lowerBound, string upperBound)
        {
            if (this.Name == Either)
            {
                return ValidateEither(value, lowerBound, upperBound);
            }
            if (this.Name == Between)
            {
                return ValidateBetween(value, lowerBound, upperBound);
            }

            return true;
        }
        public bool ValidateWithinBounds(string value, string max)
        {
            if (this.Name == LessThan)
            {
                return ValidateLessThan(value, max);
            }
            if (this.Name == LessThanOrEqual)
            {
                return ValidateLessThanOrEqual(value, max);
            }

            if (this.Name == GreaterThan)
            {
                return ValidateGreaterThan(value, max);
            }
            if (this.Name == GreaterThanOrEqual)
            {
                return ValidateGreaterThanOrEqual(value, max);
            }

            if(this.Name == EqualTo)
            {
                return ValidateEqualTo(value, max);
            }

            return true;
        }

        public string Name { get; set; }
        public bool UsesOneInput { get; set; }
        public bool UsesTwoInputs { get; set; }
    }
}
