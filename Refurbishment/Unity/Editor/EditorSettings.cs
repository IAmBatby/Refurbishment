using Refurbishment.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Refurbishment.Unity
{
    [CreateAssetMenu(fileName = "EditorSettings", menuName = "ScriptableObjects/Refurbishment/EditorSettings", order = 1)]
    public class EditorSettings : ScriptableObject
    {
        private static EditorSettings _instance;
        public static EditorSettings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = Resources.FindObjectsOfTypeAll<EditorSettings>().FirstOrDefault();
                return (_instance);
            }
        }
        [field: SerializeField] public List<DungeonPreset> DungeonPresets {  get; set; }

        [field: Space(20), Header("Tile Gizmos Settings")]
        [field: SerializeField, Range(0f, 1f)] public float TileOpacity { get; private set; }

        [field: SerializeField] public Color TileFloorPreview { get; private set; } = Color.white;
        [field: SerializeField] public Color TileWallPreview { get; private set; } = Color.white;
        [field: SerializeField] public Color TileCeilingPreview { get; private set; } = Color.white;

        [field: SerializeField] public DrawMode GizmosDrawMode { get; private set; } = DrawMode.SubTiles;
        [field: SerializeField] public SubTileType DrawSubTileTypeMode { get; private set; }

        [field: Space(20), Header("Object Gizmos Settings")]
        [field: SerializeField, Range(0f, 1f)] public float ObjectOpacity { get; private set; }

        [field: SerializeField] public Color SpawnSyncedObjectPreviewPrimary { get; private set; } = Color.white;
        [field: SerializeField] public Color SpawnSyncedObjectPreviewSecondary { get; private set; } = Color.white;

        [field: Space(20), Header("Node Gizmos Settings")]
        [field: SerializeField, Range(0f, 1f)] public float NodeOpacity { get; private set; }
    }
}