using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CommandSystem
{
    public class CommandQueue
    {
        public event EventHandler Changed;

        private List<ICommand> _commands = new List<ICommand>();


        private int _currentCommand = -1;


        public bool IsAtEnd => _currentCommand >= _commands.Count - 1;

        public bool IsAtStart => _currentCommand < 0;

        public bool Execute(ICommand command)
        {
            if (command.Commit())
            {
                _commands.Add(command);
                _currentCommand += 1;

                OnChanged(EventArgs.Empty);

                return true;
            }


            return false;
        }

        public void ToEnd()
        {
            while (!IsAtEnd)
                Forward();
        }

        public void Backward()
        {
            if (IsAtStart)
                return;

            _commands[_currentCommand].Rollback();
            _currentCommand -= 1;

            OnChanged(EventArgs.Empty);
        }

        public void Forward()
        {

            if (IsAtEnd)
                return;

            _currentCommand += 1;
            _commands[_currentCommand].Commit();

            OnChanged(EventArgs.Empty);
        }

        protected virtual void OnChanged(EventArgs eventArgs)
        {
            var handler = Changed;
            handler?.Invoke(this, eventArgs);
        }
    }
}