// <copyright file="BaseViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Mobile_Rounds.ViewModels.Shared.Commands;
using Mobile_Rounds.ViewModels.Shared.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Mobile_Rounds.ViewModels.Admin.UnitOfMeasure;

namespace Mobile_Rounds.ViewModels.Shared
{
    /// <summary>
    /// Represents common operations on view models.
    /// </summary>
    public abstract class BaseViewModel : NotificationBase
    {
        public List<UnitOfMeasure> MockUnits { get; set; }


        /// <summary>
        /// Gets or sets the set of breadcrumb items that are visible on the screen, excluding
        /// the home selection.
        /// </summary>
        public ICollection<BreadcrumbItemModel> Crumbs { get; protected set; }

        /// <summary>
        /// Gets or sets the home option.
        /// </summary>
        public ICommand GoHome { get; protected set; }

        /// <summary>
        /// Gets the command to call when a breadcrumb item is selected.
        /// </summary>
        public ICommand CrumbCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        protected BaseViewModel()
        {
            this.Crumbs = new List<BreadcrumbItemModel>();
            this.GoHome = new GoHomeCommand();
            this.MockUnits = new List<UnitOfMeasure>();
            this.CrumbCommand = new AsyncCommand((obj) =>
            {
                var ev = obj as GoedWare.Controls.Breadcrumb.BreadcrumbEventArgs;
                if (ev != null)
                {
                    var model = ev.Item as BreadcrumbItemModel;
                    if (model.Command != null)
                    {
                        model.Command.Execute(model.Title);
                    }
                }
            });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        /// <param name="homeCommand">The command to call when the home item is selected.</param>
        protected BaseViewModel(ICommand homeCommand)
            : this()
        {
            this.GoHome = homeCommand;
            this.MockUnits = new List<UnitOfMeasure>();
        }
    }
}
