using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Refurbishment.Base
{
    [Serializable]
    public struct SubTileInfo
    {
        public Vector3 Position;
        public Vector3Int Index;
        public SubTileType TileType;
        public FaceInfo[] Faces;

        public FaceInfo Face_Bottom => Faces[0];
        public FaceInfo Face_Front => Faces[1];
        public FaceInfo Face_Back => Faces[2];
        public FaceInfo Face_Left => Faces[3];
        public FaceInfo Face_Right => Faces[4];
        public FaceInfo Face_Top => Faces[5];


        public bool IsFloor => TileType.HasFlag(SubTileType.Floor);
        public bool IsWall => TileType.HasFlag(SubTileType.Wall);
        public bool IsCeiling => TileType.HasFlag(SubTileType.Ceiling);

        private float tileScale;
        private float tileThickness;

        public SubTileInfo(Vector3 newPosition, Vector3Int newIndex, Vector3Int tileMatrixSize, float newTileScale, float newTileThickness)
        {
            Position = newPosition;
            Index = newIndex;

            bool isFloor = (Index.y == 0);
            bool isCeiling = (Index.y == tileMatrixSize.y - 1);
            bool isWall = (Index.x == 0 || Index.x == tileMatrixSize.x - 1 || Index.z == 0 || Index.z == tileMatrixSize.z - 1);

            TileType = SubTileType.None;
            if (isFloor) TileType = TileType | SubTileType.Floor;
            if (isWall) TileType = TileType | SubTileType.Wall;
            if (isCeiling) TileType = TileType | SubTileType.Ceiling;
            if (!isFloor && !isWall && !isCeiling) TileType = SubTileType.Empty;
            TileType ^= SubTileType.None;

            tileScale = newTileScale;
            tileThickness = newTileThickness;
            Faces = new FaceInfo[6];
            ConstructFaceInfo(tileMatrixSize, tileScale, tileThickness);
        }

        private void ConstructFaceInfo(Vector3Int tileMatrixSize, float tileScale, float tileThickness)
        {
            bool floorActive = IsFloor;
            bool forwardActive = IsWall && Index.z == tileMatrixSize.z - 1;
            bool backwardActive = IsWall && Index.z == 0;
            bool leftActive = IsWall && Index.x == 0;
            bool rightActive = IsWall && Index.x == tileMatrixSize.x - 1;
            bool ceilingActive = IsCeiling;


            Faces[0] = new FaceInfo(floorActive, AnchorPos(y: true, negative: true), Vector3.zero, AnchorScale(y: true), SubTileType.Floor);
            Faces[1] = new FaceInfo(forwardActive, AnchorPos(z: true), new Vector3(0, 180, 0), AnchorScale(z: true), SubTileType.Wall);
            Faces[2] = new FaceInfo(backwardActive, AnchorPos(z: true, negative: true), Vector3.zero, AnchorScale(z: true), SubTileType.Wall);
            Faces[3] = new FaceInfo(leftActive, AnchorPos(x: true, negative: true), new Vector3(0, 90, 0), AnchorScale(x: true), SubTileType.Wall);
            Faces[4] = new FaceInfo(rightActive, AnchorPos(x: true), new Vector3(0, 270, 0), AnchorScale(x: true), SubTileType.Wall);
            Faces[5] = new FaceInfo(ceilingActive, AnchorPos(y: true), Vector3.zero, AnchorScale(y: true), SubTileType.Ceiling);
        }

        private Vector3 AnchorPos(bool x = false, bool y = false, bool z = false, bool negative = false)
        {
            float offset = (tileScale / 2) - (tileThickness / 2);
            if (negative)
                offset = -offset;
            if (x)
                return (Position + new Vector3(offset, 0, 0));
            else if (y)
                return (Position + new Vector3(0, offset, 0));
            else
                return (Position + new Vector3(0, 0, offset));
        }

        private Vector3 AnchorScale(bool x = false, bool y = false, bool z = false)
        {
            if (x)
                return (new Vector3(tileThickness, tileScale, tileScale));
            else if (y)
                return (new Vector3(tileScale, tileThickness, tileScale));
            else
                return (new Vector3(tileScale, tileScale, tileThickness));
        }
    }
}
