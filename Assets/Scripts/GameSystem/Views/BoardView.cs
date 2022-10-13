using BoardSystem;
using System;
using System.Collections.Generic;
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

        private readonly Dictionary<Position, PositionView> _positions = new Dictionary<Position, PositionView>();
        private List<Position> _activatedPosition = new List<Position>();

        private void OnEnable()
        {
            var positionViews = GetComponentsInChildren<PositionView>();
            foreach (var positionView in positionViews)
            {
                _positions[PositionHelper.GridPosition(positionView.WorldPosition)] = positionView;
                positionView.Selected += OnPositionViewSelected;
            }
        }

        public void SetActivePositions(List<Position> positions)
        {
            foreach (var position in _activatedPosition)
                _positions[position].Deactivate();

            _activatedPosition = positions;

            foreach (var position in positions)
                _positions[position].Activate();
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
