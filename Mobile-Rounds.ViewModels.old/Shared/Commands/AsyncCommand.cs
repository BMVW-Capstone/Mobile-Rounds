// <copyright file="AsyncCommand.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mobile_Rounds.ViewModels.Shared.Commands
{
    /// <summary>
    /// Represents a command that can run async.
    /// </summary>
    public class AsyncCommand : ICommand
    {
        /// <summary>
        /// Event for when the command changed.
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, ev) => { };

        /// <summary>
        /// Triggers the notification that the command change happened.
        /// </summary>
        public void RaiseExecuteChanged()
        {
            this.CanExecuteChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Determines if the command can execute or not.
        /// </summary>
        /// <param name="parameter">The command parameter</param>
        /// <returns>True if it can execute, otherwise false.</returns>
        public bool CanExecute(object parameter)
        {
            return this.Condition(parameter);
        }

        /// <summary>
        /// Executes the action from the constructor with the given parameter.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        public void Execute(object parameter)
        {
            this.Action(parameter);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncCommand"/> class.
        /// Defines a command that can be created using just a lambda function.
        /// </summary>
        public AsyncCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncCommand"/> class.
        /// Defines a command that can be created using just a lambda function.
        /// </summary>
        /// <param name="action">The action to call on trigger.</param>
        public AsyncCommand(Action<object> action)
        {
            this.Action = action;
            this.Condition = (param) => true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncCommand"/> class.
        /// Defines a command that can be created using just a lambda function.
        /// </summary>
        /// <param name="action">The action to call on trigger.</param>
        /// <param name="condition">The conditional check function.</param>
        public AsyncCommand(Action<object> action, Func<object, bool> condition)
        {
            this.Action = action;
            this.Condition = condition;
        }

        /// <summary>
        /// Gets or sets the action to call when the command executes.
        /// </summary>
        protected Action<object> Action { get; set; }

        /// <summary>
        /// Gets or sets the function to determine if the action can execute.
        /// </summary>
        protected Func<object, bool> Condition { get; set; }
    }
}
