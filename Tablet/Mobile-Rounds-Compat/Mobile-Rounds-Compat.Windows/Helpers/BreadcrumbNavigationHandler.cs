using Mobile_Rounds.ViewModels.Platform;
using Mobile_Rounds.ViewModels.Shared.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.Helpers
{
    public class BreadcrumbNavigationHandler : IBreadcrumbNavigationEvent
    {
        public void Handle(object eventObj)
        {
            var ev = eventObj as GoedWare.Controls.Breadcrumb.Compat.BreadcrumbEventArgs;
            if (ev != null)
            {
                var model = ev.Item as BreadcrumbItemModel;
                if (model.Command != null)
                {
                    model.Command.Execute(model.Title);
                }
            }
        }
    }
}
