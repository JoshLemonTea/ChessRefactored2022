using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    internal class PositionView : MonoBehaviour, IPointerClickHandler
    {
        static readonly ProfilerMarker _PreparePerfMarker = new ProfilerMarker("PositionView.Click");

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
            _PreparePerfMarker.Begin();
            var handler = Selected;
            handler?.Invoke(this, eventArgs);
            _PreparePerfMarker.End();
        }

        internal void Activate()
            => OnActivation?.Invoke();

        internal void Deactivate()
            => OnDeactivation?.Invoke();
    }
}
