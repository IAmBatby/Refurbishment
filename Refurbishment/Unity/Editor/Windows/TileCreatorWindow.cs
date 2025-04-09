#if UNITY_EDITOR
using Refurbishment.Base;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Refurbishment.Unity
{
    /*
    public class TileCreatorWindow : EditorWindow
    {
        private static EditorSettings Settings => EditorSettings.Instance;
        private static GameObject parentObject;

        [MenuItem("Bunnings/Open Tile Creator")]
        public static void OpenWindow()
        {
            GetWindow<TileCreatorWindow>().Show();
        }

        public void OnGUI()
        {
            EditorGUILayout.SelectableLabel("Tile Creator");

            Settings.Tiles_DefaultFloorPrefab = (GameObject)EditorGUILayout.ObjectField(Settings.Tiles_DefaultFloorPrefab, typeof(GameObject), false);
            Settings.Tiles_DefaultWallPrefab = (GameObject)EditorGUILayout.ObjectField(Settings.Tiles_DefaultWallPrefab, typeof(GameObject), false);
            Settings.Tiles_DefaultCeilingPrefab = (GameObject)EditorGUILayout.ObjectField(Settings.Tiles_DefaultCeilingPrefab, typeof(GameObject), false);

            EditorGUILayout.Space(20);

            Settings.newTileName = EditorGUILayout.TextField(Settings.newTileName);
            Settings.newTileSize = EditorGUILayout.Vector3IntField("Tile Size", Settings.newTileSize);
            Settings.newTileScale = EditorGUILayout.FloatField(Settings.newTileScale);
            Settings.newTileThickness = EditorGUILayout.FloatField(Settings.newTileThickness);
            parentObject = (GameObject)EditorGUILayout.ObjectField(parentObject, typeof(GameObject), true);

            if (parentObject != null && GUILayout.Button("Create New Tile"))
                CreateNewTile();
        }

        public void CreateNewTile()
        {
            GameObject newTileObject = new GameObject(Settings.newTileName);
            Tile tile = newTileObject.AddComponent<Tile>();
            ExtendedTileGizmos extendedTile = newTileObject.AddComponent<ExtendedTileGizmos>();

            extendedTile.TileScale = Settings.newTileScale;
            extendedTile.TileThickness = Settings.newTileThickness;
            extendedTile.TileSize = Settings.newTileSize;
            newTileObject.transform.SetParent(parentObject.transform, false);

            TileInfo newTileInfo = new TileInfo(newTileObject.transform.position, newTileObject.transform.lossyScale, Settings.newTileSize, Settings.newTileScale, Settings.newTileThickness);

            List<GameObject> spawnedFaces = new List<GameObject>();
            foreach (SubTileInfo subTile in newTileInfo.GetSubTiles())
                foreach (FaceInfo faceInfo in subTile.Faces)
                    spawnedFaces.Add(InstansiateFace(newTileObject, newTileInfo, subTile, faceInfo));

            Selection.activeGameObject = newTileObject;

            //newTileObject.transform.position = Vector3.zero;
        }

        public GameObject InstansiateFace(GameObject tileObject, TileInfo tileInfo, SubTileInfo subTile, FaceInfo faceInfo)
        {
            if (faceInfo.Active == false) return (null);

            GameObject prefabToSpawn = faceInfo.FaceType switch
            {
                SubTileType.Floor => Settings.Tiles_DefaultFloorPrefab,
                SubTileType.Wall => Settings.Tiles_DefaultWallPrefab,
                SubTileType.Ceiling => Settings.Tiles_DefaultCeilingPrefab,
                _ => null
            };
            if (prefabToSpawn == null) return (null);

            GameObject spawnedPrefab = GameObject.Instantiate(prefabToSpawn);
            spawnedPrefab.transform.rotation = Quaternion.Euler(faceInfo.Rotation);
            Vector3 offset = tileObject.transform.InverseTransformPoint(tileInfo.CenterTileOffset);
            //spawnedPrefab.transform.position = Vector3.zero;
            spawnedPrefab.transform.position = faceInfo.Position - offset;
            spawnedPrefab.transform.SetParent(tileObject.transform, true);
            //spawnedPrefab.transform.localPosition = tileObject.transform.TransformPoint(tileInfo.CenterTileOffset + faceInfo.Position);

            return (spawnedPrefab);
        }
    }
    */
}
#endif
