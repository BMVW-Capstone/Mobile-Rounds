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
                "Less Than", "Greater Than", "Equal To"
            };

            DoubleInputs = new List<string>()
            {
                "Between", "Either"
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

        public string Name { get; set; }
        public bool UsesOneInput { get; set; }
        public bool UsesTwoInputs { get; set; }
    }
}
