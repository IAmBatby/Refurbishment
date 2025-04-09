using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Refurbishment.Base
{
    [Flags] public enum SubTileType { None = 0, Floor = 1, Empty = 2, Wall = 4, Ceiling = 8 }

    [System.Serializable]
    public class TileInfo
    {
        [field: Header("Tile Properties")]
        [field: SerializeField] public Vector3Int TileSize { get; private set; }
        [field: SerializeField] public float TileScale { get; private set; }
        [field: SerializeField] public float TileThickness { get; private set; }

        public Vector3 TileScaleVector => new Vector3(TileScale, TileScale, TileScale);

        public Vector3 CenterTileOffset => Vector3.Lerp(AllSubTiles[0].Position, AllSubTiles[AllSubTiles.Count - 1].Position, 0.5f);

        public SubTileInfo[,,] SubTilesMatrix { get; private set; }
        [field: SerializeField] public List<SubTileInfo> AllSubTiles { get; private set; } = new List<SubTileInfo>();

        private Vector3 StartingPosition;


        public TileInfo(Vector3 position, Vector3 scale, Vector3Int tileSize, float tileScale, float tileThickness)
        {
            StartingPosition = ((position - scale / 2) + (TileScaleVector / 2)) - (scale / 2);
            TileSize = tileSize;
            TileScale = tileScale;
            TileThickness = tileThickness;
            PopulateSubTiles();
        }

        private void PopulateSubTiles()
        {
            SubTilesMatrix = new SubTileInfo[TileSize.x, TileSize.y, TileSize.z];
            AllSubTiles.Clear();
            for (int x = 0; x < TileSize.x; x++)
                for (int y = 0; y < TileSize.y; y++)
                    for (int z = 0; z < TileSize.z; z++)
                    {
                        Vector3 adjustedPosition = StartingPosition + new Vector3(TileScaleVector.x * x, TileScaleVector.y * y, TileScaleVector.z * z);
                        SubTileInfo subTileInfo = new SubTileInfo(adjustedPosition, new Vector3Int(x, y, z), TileSize, TileScale, TileThickness);
                        SubTilesMatrix[x, y, z] = subTileInfo;
                        AllSubTiles.Add(subTileInfo);
                    }
        }

        public List<SubTileInfo> GetSubTiles(SubTileType tileType)
        {
            return (AllSubTiles.Where(t => (tileType & t.TileType) != 0).ToList());
        }

        public List<SubTileInfo> GetSubTiles()
        {
            return (AllSubTiles.ToList());
        }
    }
}
