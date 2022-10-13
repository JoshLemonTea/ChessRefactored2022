using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.GameStates
{
    internal class MenuState : GameState
    {
        public const string Name = "Menu";

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Entering Menu");
        }


        public override void OnExit()
        {
            base.OnEnter();
            Debug.Log("Exiting Menu");
        }

        public override void Play()
        {
            StateMachine.MoveTo(PlayingState.Name);    
        }
    }
}
