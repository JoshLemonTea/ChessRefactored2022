using GameSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Views
{
    internal class PositionEventArgs : EventArgs
    {
        public Position Position { get; }

        public PositionEventArgs(Position position)
        {
            Position = position;
        }
    }

    internal class BoardView : MonoBehaviour
    {
        public event EventHandler<PositionEventArgs> PositionSelected;

        private void OnEnable()
        {
            var positionViews = GetComponentsInChildren<PositionView>();
            foreach (var positionView in positionViews)
            {
                positionView.Selected += OnPositionViewSelected;
            }
        }

        private void OnPositionViewSelected(object sender, EventArgs e)
        {
            if(sender is PositionView positionView)
            {
                var position = PositionHelper.GridPosition(positionView.WorldPosition);
                OnPositionSelected(new PositionEventArgs(position));
            }
        }

        protected virtual void OnPositionSelected(PositionEventArgs  eventArgs)
        {
            var handler = PositionSelected;
            handler?.Invoke(this, eventArgs);
        }
    }
}
