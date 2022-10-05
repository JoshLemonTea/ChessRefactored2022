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
            positionView.Selected += PositionViewSelected;

        }

        private void PositionViewSelected(object sender, EventArgs e)
        {
            if(sender is PositionView view)
            {
                var origPos = view.transform.position;
                var gridPos = PositionHelper.GridPosition(origPos);
                var worldPos = PositionHelper.WorldPosition(gridPos);

                Debug.Log(
                    $"Position at original position: {origPos}\n" +
                    $"         at grid position: {gridPos}\n" +
                    $"         at world position: {worldPos}\n" );
            }
            
        }
    }
}
