using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystem.Views
{
    internal class MenuView : MonoBehaviour
    {
        public event EventHandler StartClicked;

        [SerializeField]
        private Button _startButton;

        private void Start()
        {
            _startButton.onClick.AddListener(() => OnStartClicked(EventArgs.Empty));
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);

        protected virtual void OnStartClicked(EventArgs eventArgs)
        {
            var handler = StartClicked;
            handler?.Invoke(this, eventArgs);
        }
    }
}
