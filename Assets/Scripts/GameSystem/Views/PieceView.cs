using ChessSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Views
{
    public class PieceView : MonoBehaviour, IPiece
    {

        [SerializeField]
        private PieceType _type;

        [SerializeField]
        private Player _player;

        public Vector3 WorldPosition => transform.position;

        public string Name => gameObject.name;

        public PieceType Type => _type;

        public Player Player => _player;

        public void MoveTo(Vector3 position)
            => transform.position = position;
    }
}
