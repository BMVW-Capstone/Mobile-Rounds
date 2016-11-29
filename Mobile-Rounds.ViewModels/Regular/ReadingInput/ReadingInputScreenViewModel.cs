// <copyright file="ReadingInputScreenViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Controls;

namespace Mobile_Rounds.ViewModels.Regular.ReadingInput
{
    /// <summary>
    /// Represents the data operations for the reading input screen.
    /// </summary>
    public class ReadingInputScreenViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingInputScreenViewModel"/> class.
        /// Represents the full view model for the page.
        /// </summary>
        public ReadingInputScreenViewModel()
        {
            this.Crumbs.Add(new BreadcrumbItemModel("My Region"));
            this.Crumbs.Add(new BreadcrumbItemModel("Compressor Room"));
            this.Crumbs.Add(new BreadcrumbItemModel("Supply Air Receiver Tank"));
            this.ListModel = new ReadingInputListViewModel();
            this.Input = new ReadingInputViewModel();
        }

        /// <summary>
        /// Gets or sets the model used for databinding the list part of the view.
        /// </summary>
        public ReadingInputListViewModel ListModel { get; private set; }

        public ReadingInputViewModel Input { get; private set; }
    }
}
