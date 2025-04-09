using DunGen;
using Refurbishment.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Refurbishment.Unity
{
    public enum DrawMode { None, Tile, SubTiles, Faces }
    public class ExtendedTile : MonoBehaviour
    {
        [field: SerializeField] public DungeonPreset Preset { get; private set; }
        [field: SerializeField] public Tile Tile { get; private set; }
        [field: Space(10)]
        [field: SerializeField] public Vector3Int TileSize { get; private set; }

        private TileInfo TileInfo;
        private List<SubTileInfo> tilesToDraw = new List<SubTileInfo>();

        ////////// References //////////

        [field: SerializeField] public List<Doorway> CollectedDoorways { get; private set; } = new List<Doorway>();
        [field: SerializeField] public List<GlobalProp> CollectedGlobalProps{ get; private set; } = new List<GlobalProp>();
        [field: SerializeField] public List<RandomProp> CollectedRandomProps { get; private set; } = new List<RandomProp>();
        [field: SerializeField] public List<SpawnSyncedObject> CollectedSyncedObjectSpawners { get; private set; } = new List<SpawnSyncedObject>();
        [field: SerializeField] public List<RandomScrapSpawn> CollectedScrapSpawners { get; private set; } = new List<RandomScrapSpawn>();
        [field: SerializeField] public List<RandomMapObject> CollectedHazardSpawners { get; private set; } = new List<RandomMapObject>();
        [field: SerializeField] public List<EnemyVent> CollectedEnemySpawners { get; private set; } = new List<EnemyVent>();
        [field: SerializeField] public List<Transform> CollectedAINodes { get; private set; } = new List<Transform>();
        [field: SerializeField] public List<Light> CollectedPoweredLights { get; private set; } = new List<Light>();

        private void OnValidate() => RefreshTileInfo();
        private void Start() => RefreshTileInfo();
        private void RefreshTileInfo()
        {
            DisablePicking();
            if (Tile == null) Tile = GetComponent<Tile>();
            TileInfo = new TileInfo(transform.position, transform.lossyScale, TileSize, Preset.PreferredSubTileScale, Preset.PreferredSubTileThickness);
            tilesToDraw = EditorSettings.Instance.GizmosDrawMode == DrawMode.Faces ? TileInfo.GetSubTiles(EditorSettings.Instance.DrawSubTileTypeMode) : TileInfo.GetSubTiles();

            CollectedDoorways = gameObject.GetComponentsInChildren<Doorway>().ToList();
            CollectedGlobalProps = gameObject.GetComponentsInChildren<GlobalProp>().ToList();
            CollectedRandomProps = gameObject.GetComponentsInChildren<RandomProp>().ToList();
            CollectedSyncedObjectSpawners = gameObject.GetComponentsInChildren<SpawnSyncedObject>().ToList();
            CollectedScrapSpawners = gameObject.GetComponentsInChildren<RandomScrapSpawn>().ToList();
            CollectedHazardSpawners = gameObject.GetComponentsInChildren<RandomMapObject>().ToList();
            CollectedEnemySpawners = gameObject.GetComponentsInChildren<EnemyVent>().ToList();
            CollectedAINodes = gameObject.GetComponentsInChildren<Transform>().Where(t => t.gameObject.CompareTag("AINode")).ToList();
            CollectedPoweredLights = gameObject.GetComponentsInChildren<Light>().Where(t => t.gameObject.CompareTag("PoweredLight")).ToList();
        }

        private void OnDrawGizmos()
        {
            if (TileInfo == null || TileSize == Vector3Int.zero || Preset.PreferredSubTileScale == 0f || EditorSettings.Instance.GizmosDrawMode == DrawMode.None) return;
            Gizmos.matrix = transform.localToWorldMatrix;
            DrawBoundries();
        }

        public void DrawBoundries()
        {
            if (tilesToDraw == null || TileInfo.AllSubTiles == null || TileInfo.AllSubTiles.Count == 0) return;
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(TileSize.x * Preset.PreferredSubTileScale, TileSize.y * Preset.PreferredSubTileScale, TileSize.z * Preset.PreferredSubTileScale));
            Vector3 offset = Vector3.Lerp(TileInfo.AllSubTiles[0].Position, TileInfo.AllSubTiles[TileInfo.AllSubTiles.Count - 1].Position, 0.5f);
            foreach (SubTileInfo subTile in tilesToDraw)
            {
                if (EditorSettings.Instance.GizmosDrawMode == DrawMode.SubTiles)
                    Utilities.DrawCube(subTile.Position - offset, TileInfo.TileScaleVector, new Color(Color.white.r, Color.white.g, Color.white.b, 0f), EditorSettings.Instance.TileOpacity);
                else
                    for (int i = 0; i < subTile.Faces.Length; i++)
                        if (subTile.Faces[i].Active && (subTile.Faces[i].FaceType & EditorSettings.Instance.DrawSubTileTypeMode) != 0)
                            Utilities.DrawCube(subTile.Faces[i].Position - offset, subTile.Faces[i].Scale, subTile.Faces[i].Color, EditorSettings.Instance.TileOpacity);
            }
        }

        private void DisablePicking()
        {
#if UNITY_EDITOR
            SceneVisibilityManager.instance.DisablePicking(gameObject, false);
#endif
        }
    }
}
