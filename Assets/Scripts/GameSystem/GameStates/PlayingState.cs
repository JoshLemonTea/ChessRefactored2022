using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.GameStates
{
    internal class PlayingState : GameState
    {
        public const string Name = "Playing";

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Entering Playing");
        }


        public override void OnExit()
        {
            base.OnEnter();
            Debug.Log("Exiting Playing");
        }

        public override void Select(Position position)
        {
            StateMachine.MoveTo(MenuState.Name);
        }
    }
}
