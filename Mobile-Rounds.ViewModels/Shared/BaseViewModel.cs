// <copyright file="BaseViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Mobile_Rounds.ViewModels.Admin.UnitOfMeasure;
using Mobile_Rounds.ViewModels.Shared.Commands;
using Mobile_Rounds.ViewModels.Shared.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Mobile_Rounds.ViewModels.Shared.Navigation;

namespace Mobile_Rounds.ViewModels.Shared
{
    /// <summary>
    /// Represents common operations on view models.
    /// </summary>
    public abstract class BaseViewModel : NotificationBase
    {
        /// <summary>
        /// Gets or sets the service to use for changing screens.
        /// </summary>
        public static INavigator Navigator { get; set; }

        public List<UnitOfMeasure> MockUnits { get; set; }

        /// <summary>
        /// Gets the property used to handle navigating to the admin page.
        /// </summary>
        public ICommand GoToAdmin { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is an admin user or not.
        /// </summary>
        public bool IsAdmin { get; set; }

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
            this.IsAdmin = false;
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

            this.GoToAdmin = new AsyncCommand((obj) =>
            {
                Navigator.Navigate(NavigationType.AdminHome);
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
