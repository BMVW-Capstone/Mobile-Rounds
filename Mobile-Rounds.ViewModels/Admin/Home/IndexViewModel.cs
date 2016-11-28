// <copyright file="IndexViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Mobile_Rounds.ViewModels.Shared.Commands;

namespace Mobile_Rounds.ViewModels
{
    /// <summary>
    /// Represents the bsaic data for the home screens as exposed to XAML.
    /// </summary>
    public class IndexViewModel : Shared.BaseViewModel
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexViewModel"/> class.
        /// Creates and sets defaults for the view model.
        /// </summary>
        public IndexViewModel()
        {
            this.Crumbs.Add(new Shared.Controls.BreadcrumbItemModel("Admin", new GoHomeCommand()));
        }
    }
}
