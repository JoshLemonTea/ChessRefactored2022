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
        public void OnEnable()
        {
            var positionView = FindObjectOfType<PositionView>();
            positionView.Selected += (s, e) => Debug.Log($"Position {s} clicked");

        }
    }
}
