using GameSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem
{
    public class GameLoop : MonoBehaviour
    {
        private void OnEnable()
        {
            var boardView = FindObjectOfType<BoardView>();
            boardView.PositionSelected += PositionViewSelected;

        }

        private void PositionViewSelected(object sender, PositionEventArgs e)
            => Debug.Log($"Position Selected: {e.Position}");
    }
}
