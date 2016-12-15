using Mobile_Rounds.ViewModels.Shared.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Platform
{
    /// <summary>
    /// Represents and event from the breadcrumbs Platform specific, so use 
    /// Dependency Injection.
    /// </summary>
    public interface IBreadcrumbNavigationEvent
    {
        /// <summary>
        /// The method to call to handle the breadcrumb event.
        /// </summary>
        /// <param name="o">The event object.</param>
        void Handle(object o);
    }
}
