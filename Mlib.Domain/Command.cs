﻿using System;
using System.Windows.Input;

namespace Mlib.Domain
{
    public class Command : ICommand
    {
        Action<object> action;
        public event EventHandler CanExecuteChanged;

        public Command(Action<object> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => action.Invoke(parameter);
    }
}