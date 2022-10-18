using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystem.Views
{
    internal class ReplayView : MonoBehaviour
    {
        public event EventHandler Backward;
        public event EventHandler Forward;

        [SerializeField]
        private Button _backward;
        [SerializeField]
        private Button _forward;

        private void Start()
        {
            _backward.onClick.AddListener(() => OnBackward(EventArgs.Empty));
            _forward.onClick.AddListener(() => OnForward(EventArgs.Empty));
        }

        public bool CanGoBack { set { _backward.interactable = value; } }
        public bool CanGoForward { set { _forward.interactable = value; } }

        protected virtual void OnBackward(EventArgs eventArgs)
        {
            var handler = Backward;
            handler?.Invoke(this, eventArgs);
        }

        protected virtual void OnForward(EventArgs eventArgs)
        {
            var handler = Forward;
            handler?.Invoke(this, eventArgs);
        }
    }
}
