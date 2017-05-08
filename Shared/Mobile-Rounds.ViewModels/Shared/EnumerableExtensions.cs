using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Shared
{
    internal static class EnumerableExtensions
    {
        /// <summary>
        /// Adds a list of items to an existing observable collection. Usefull for list views.
        /// </summary>
        /// <typeparam name="TType">The type of data to add.</typeparam>
        /// <param name="dest">The destination list.</param>
        /// <param name="toAdd">The items to add.</param>
        public static void AddRange<TType>(this ObservableCollection<TType> dest, IEnumerable<TType> toAdd)
        {
            foreach (var item in toAdd)
            {
                dest.Add(item);
            }
        }
    }
}
