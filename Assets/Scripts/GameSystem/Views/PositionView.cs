using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    internal class PositionView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private UnityEvent OnActivation;

        [SerializeField]
        private UnityEvent OnDeactivation;

        public event EventHandler Selected;


        public Vector3 WorldPosition => transform.position;

        public void OnPointerClick(PointerEventData eventData)
            => OnSelected(EventArgs.Empty);

        protected virtual void OnSelected(EventArgs eventArgs)
        {
            var handler = Selected;
            handler?.Invoke(this, eventArgs);
        }

        internal void Activate()
            => OnActivation?.Invoke();

        internal void Deactivate()
            => OnDeactivation?.Invoke();
    }
}
