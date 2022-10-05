using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    internal class PositionView : MonoBehaviour, IPointerClickHandler
    {
        public event EventHandler Selected;

        public void OnPointerClick(PointerEventData eventData)
            => OnSelected(EventArgs.Empty);

        protected virtual void OnSelected(EventArgs eventArgs)
        {
            var handler = Selected;
            handler?.Invoke(this, eventArgs);
        }
    }
}