// <copyright file="BreadcrumbItemModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mobile_Rounds.ViewModels.Shared.Controls
{
    /// <summary>
    /// Reprents a breadcrumb option.
    /// </summary>
    public sealed class BreadcrumbItemModel
    {
        /// <summary>
        /// Gets or sets the text to display.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the command that is called when the option is selected.
        /// </summary>
        public ICommand Command { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BreadcrumbItemModel"/> class.
        /// This is used to represent an option in the breadcrumb list.
        /// </summary>
        public BreadcrumbItemModel()
            : this(string.Empty, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BreadcrumbItemModel"/> class.
        /// This is used to represent an option in the breadcrumb list.
        /// </summary>
        /// <param name="title">The title to display.</param>
        public BreadcrumbItemModel(string title)
        {
            this.Title = title;
            this.Command = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BreadcrumbItemModel"/> class.
        /// This is used to represent an option in the breadcrumb list.
        /// </summary>
        /// <param name="title">The title to display.</param>
        /// <param name="command">The command to fire when the option is selected.</param>
        public BreadcrumbItemModel(string title, ICommand command)
            : this(title)
        {
            this.Command = command;
        }
    }
}
