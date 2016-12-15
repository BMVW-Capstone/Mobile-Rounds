// <copyright file="ColoredTile.xaml.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Mobile_Rounds.Controls
{
    /// <summary>
    /// Represents a tile that follows our scheme.
    /// </summary>
    public sealed partial class ColoredTile : UserControl
    {
        /// <summary>
        /// Gets or sets the command to call when the tile is tapped.
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)this.GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Gers or sets the command property for XAML.
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(ColoredTile), null);

        /// <summary>
        /// Gets or sets the title to set on the tile.
        /// </summary>
        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Gets the title property for XAML.
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ColoredTile), null);

        /// <summary>
        /// Initializes a new instance of the <see cref="ColoredTile"/> class.
        /// </summary>
        public ColoredTile()
        {
            this.InitializeComponent();
        }
    }
}
