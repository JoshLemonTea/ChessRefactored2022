using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Views
{
    public class PieceView : MonoBehaviour
    {
        public Vector3 WorldPosition => transform.position;

        public string Name => gameObject.name;

        public void MoveTo(Vector3 position)
            => transform.position = position;
    }
}
