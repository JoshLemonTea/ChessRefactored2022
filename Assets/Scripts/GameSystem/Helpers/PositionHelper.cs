using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem
{
    internal static class PositionHelper
    {
        public const int Rows = 8;
        public const int Columns = 8;
        public const float TileDimension = 1;

        public static Position GridPosition(Vector3 worldPosition)
        {
            var scaledWorldPos = worldPosition / TileDimension;

            var relativePosX = scaledWorldPos.x + Columns / 2.0f - 0.5f;
            var relativePosY = scaledWorldPos.z + Rows / 2.0f - 0.5f;

            var gridPosX = Mathf.FloorToInt(relativePosX);
            var gridPosY = Mathf.FloorToInt(relativePosY);

            return new Position(gridPosX, gridPosY);
        }

        public static Vector3 WorldPosition(Position position)
        {
            var scaledWorldPosX = position.X - Columns / 2.0f + 0.5f;
            var scaledWorldPosZ = position.Y - Rows / 2.0f + 0.5f;

            var worldPosX = scaledWorldPosX * TileDimension;
            var worldPosZ = scaledWorldPosZ * TileDimension;

            return new Vector3(worldPosX, 0, worldPosZ);
        }
    }
}
