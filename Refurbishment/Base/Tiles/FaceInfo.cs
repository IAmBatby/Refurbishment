using Refurbishment.Unity;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Refurbishment.Base
{
    public struct FaceInfo
    {
        public bool Active;
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;
        public Color Color;
        public SubTileType FaceType;

        public FaceInfo(bool isActive, Vector3 position, Vector3 rotation, Vector3 scale, SubTileType faceType)
        {
            Active = isActive;
            Position = position;
            Rotation = rotation;
            Scale = scale;
            FaceType = faceType;
            Color = Color.white;
            if (faceType == SubTileType.Floor)
                Color = EditorSettings.Instance.TileFloorPreview;
            else if (faceType == SubTileType.Wall)
                Color = EditorSettings.Instance.TileWallPreview;
            else if (faceType == SubTileType.Ceiling)
                Color = EditorSettings.Instance.TileCeilingPreview;

        }
    }
}
