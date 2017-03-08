using Mobile_Rounds.ViewModels.Shared.ReadingType;
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
        private const string NotApplicable = "Not Applicable";

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
            return left.CompareTo(right);
        }

        private int NumberCompare(string left, string right)
        {
            float leftNum = float.MinValue;
            float rightNum = float.MinValue;

            if (!float.TryParse(left, out leftNum))
                throw new InvalidOperationException("Left value was expected to be a number for comparison.");

            if (!float.TryParse(right, out rightNum))
                throw new InvalidOperationException("Right value was expected to be a number for comparison.");

            var diff = leftNum - rightNum;

            if (diff == 0) return 0;
            if (diff < 0) return -1;
            return 1;
        }

        private bool ValidateLessThan(string value, string bound)
        {
            return NumberCompare(value, bound) < 0;
        }

        private bool ValidateLessThanOrEqual(string value, string bound)
        {
            return NumberCompare(value, bound) <= 0;
        }

        private bool ValidateEqualTo(string value, string bound)
        {
            return LowerCompare(value, bound) == 0;
        }

        private bool ValidateGreaterThan(string value, string bound)
        {
            return NumberCompare(value, bound) > 0;
        }

        private bool ValidateGreaterThanOrEqual(string value, string bound)
        {
            return NumberCompare(value, bound) >= 0;
        }

        private bool ValidateEither(string value, string min, string max)
        {
            if(LowerCompare(value, min) == 0 || LowerCompare(value, max) == 0)
            {
                return true;
            }

            if (!char.IsNumber(value[0]))
            {
                //not a number, so the comparison failed.
                return false;
            }

            //must be a number, so compare that.
            return NumberCompare(value, min) == 0 || LowerCompare(value, max) == 0;
        }

        private bool ValidateBetween(string value, string min, string max)
        {
            return ValidateGreaterThanOrEqual(value, min) && ValidateLessThanOrEqual(value, max);
        }

        private bool ValidateWithinBounds(string value, string lowerBound, string upperBound)
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
        private bool ValidateWithinBounds(string value, string max)
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

        private string Normalize(string value)
        {
            return value.ToLower();
        }

        public bool ValidateBoundOrder(string lower, string upper)
        {
            if (string.IsNullOrEmpty(lower) || string.IsNullOrEmpty(upper) || !UsesTwoInputs) return false;

            var normalizedLower = Normalize(lower);
            var normalizedUpper = Normalize(upper);

            return ValidateLessThan(lower, upper);
        }

        public bool Validate(string value, string max, string min = null)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(max)) return false;

            var valueNormalized = Normalize(value);
            var maxNormalized = Normalize(max);

            if (UsesOneInput)
            {
                return ValidateWithinBounds(valueNormalized, maxNormalized);
            }
            else
            {
                //two inputs required.
                if (string.IsNullOrEmpty(min)) return false;
                var minNormalized = Normalize(min);
                return ValidateWithinBounds(valueNormalized, minNormalized, maxNormalized);
            }
        }

        public BoundType AsEnum()
        {
            if (this.Name == Between) return BoundType.Between;
            if (this.Name == Either) return BoundType.EitherOr;
            if (this.Name == LessThan) return BoundType.LessThan;
            if (this.Name == GreaterThan) return BoundType.GreaterThan;
            if (this.Name == LessThanOrEqual) return BoundType.LessThanOrEqual;
            if (this.Name == GreaterThan) return BoundType.GreaterThan;
            if (this.Name == EqualTo) return BoundType.EqualTo;
            return BoundType.NotApplicable;
        }

        public string Name { get; set; }
        public bool UsesOneInput { get; set; }
        public bool UsesTwoInputs { get; set; }
    }
}
