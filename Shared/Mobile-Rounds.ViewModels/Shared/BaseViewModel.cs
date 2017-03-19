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
using Mobile_Rounds.ViewModels.Regular.ReadingInput;
using Mobile_Rounds.ViewModels.Shared.Commands;
using Mobile_Rounds.ViewModels.Shared.Controls;
using Mobile_Rounds.ViewModels.Shared.Navigation;
using Mobile_Rounds.ViewModels.Platform;

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

        /// <summary>
        /// Gets or sets the service to use for API requests.
        /// </summary>
        public IApiRequest Api { get; set; }


        /// <summary>
        /// Initializes static members of the <see cref="BaseViewModel"/> class.
        /// </summary>
        static BaseViewModel()
        {
        }

        /// <summary>
        /// Gets the property used to handle navigating to the admin page.
        /// </summary>
        public ICommand GoToAdmin { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is an admin user or not.
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Gets the property used to handle navigating to the configure page.
        /// </summary>
        public ICommand GoToConfiguration { get; private set; }

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
        /// Triggers the loading of data and updates the IsLoading boolean for the UI.
        /// </summary>
        public async Task LoadDataAsync()
        {
            this.IsLoading = true;
            base.RaisePropertyChanged(nameof(this.IsLoading));

            await this.FetchDataAsync();

            this.IsLoading = false;
            base.RaisePropertyChanged(nameof(this.IsLoading));
        }

        /// <summary>
        /// Internal method for actually doing the request (not setting IsLoading).
        /// </summary>
        protected virtual Task FetchDataAsync()
        {
            throw new NotImplementedException("You must implement this method in a base class to use it.");
        }

        /// <summary>
        /// Determines if the list is loading values or not.
        /// </summary>
        public bool IsLoading { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        protected BaseViewModel()
        {
            this.Api = ServiceResolver.Resolve<IApiRequest>();
            this.IsAdmin = false;
            this.Crumbs = new List<BreadcrumbItemModel>();
            this.GoHome = new GoHomeCommand();

            this.GoToConfiguration = new AsyncCommand((obj) =>
            {
                Navigator.Navigate(NavigationType.Configuration);
            });

            this.CrumbCommand = new AsyncCommand((obj) =>
            {
                IBreadcrumbNavigationEvent e = ServiceResolver.Resolve<IBreadcrumbNavigationEvent>();
                e.Handle(obj);
            });

            this.GoToAdmin = new AsyncCommand((obj) =>
            {
                Navigator.Navigate(NavigationType.AdminHome);
            });
        }
    }
}
