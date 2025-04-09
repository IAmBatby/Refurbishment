using DunGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

namespace Refurbishment.Unity
{
    public class EditorManager : MonoBehaviour
    {
        private static EditorManager _instance;

        public EditorManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = Object.FindFirstObjectByType<EditorManager>();
                return _instance;
            }
        }

        [field: SerializeField] public EditorSettings Settings { get; private set; }


        private Dictionary<SpawnSyncedObject, List<MeshFilter>> spawnSyncedObjectsPreviewDict = new Dictionary<SpawnSyncedObject, List<MeshFilter>>();
        private Dictionary<Doorway, List<MeshFilter>> doorwaysPreviewDict = new Dictionary<Doorway, List<MeshFilter>>();

        private PreviewInfoGroup spawnSyncedObjectsPreviewGroup;


        private void RefreshListeners()
        {
#if UNITY_EDITOR
            Selection.selectionChanged -= OnSelectionChanged;
            Selection.selectionChanged += OnSelectionChanged;
#endif
        }

        private void OnEnable()
        {
            RefreshListeners();
        }

        private void OnSelectionChanged()
        {
            if (Settings == null) return;

            List<SpawnSyncedObject> list = FindObjectsByType<SpawnSyncedObject>(FindObjectsSortMode.None).ToList();
            spawnSyncedObjectsPreviewGroup = new(list.Select(s => s.gameObject).ToList(), list.Select(s => s.spawnPrefab).ToList(), Color.blue, Color.blue);
        }

        private void OnDrawGizmos()
        {
            if (Settings == null) return;
            RefreshListeners();

            spawnSyncedObjectsPreviewGroup?.PreviewAll();
        }
    }
}
