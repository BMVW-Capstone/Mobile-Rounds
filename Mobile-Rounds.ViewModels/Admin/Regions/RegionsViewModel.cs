// <copyright file="RegionsViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using Mobile_Rounds.ViewModels.Shared.Controls;

namespace Mobile_Rounds.ViewModels.Admin.Stations
{
    public class RegionsViewModel : Shared.BaseViewModel
    {
        public RegionsViewModel()
        {
            this.Crumbs.Add(new BreadcrumbItemModel("Admin", this.GoToAdmin));
            this.Crumbs.Add(new BreadcrumbItemModel("Regions"));
        }
    }
}
