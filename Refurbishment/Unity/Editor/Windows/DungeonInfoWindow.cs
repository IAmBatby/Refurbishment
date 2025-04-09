#if UNITY_EDITOR
using DunGen.Graph;
using LethalLevelLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Refurbishment.Unity
{
    public class DungeonInfoWindow : EditorWindow
    {
        private EditorSingleton Singleton => EditorSingleton.instance;
        private EditorSettings Settings => EditorSettings.Instance;

        [MenuItem("Refurbishment/View Dungeon Information")]
        public static void OpenWindow()
        {
            GetWindow<DungeonInfoWindow>().Show();
        }

        public void OnGUI()
        {
            Singleton.selectedPreset = (DungeonPreset)EditorGUILayout.ObjectField(Singleton.selectedPreset, typeof(DungeonPreset), false);
            if (Singleton.selectedPreset != null)
                ViewPresetInformation(Singleton.selectedPreset);
        }

        private void ViewPresetInformation(DungeonPreset preset)
        {
            ExtendedDungeonFlow extendedDungeonFlow = preset.ExtendedDungeonFlow;
            DungeonFlow dungeonFlow = preset.ExtendedDungeonFlow?.DungeonFlow;

            EditorGUILayout.BeginVertical();
            LabelUtilities.DrawField("ExtendedDungeonFlow", extendedDungeonFlow, LayoutMode.Horizontal);
            if (dungeonFlow == null) return;
            LabelUtilities.DrawField("DungeonFlow", dungeonFlow, LayoutMode.Horizontal);

            LabelUtilities.DrawFields("Tiles", dungeonFlow.GetTiles().Distinct().ToList(), LayoutMode.Vertical);


            EditorGUILayout.EndVertical();
        }
    }
}
#endif
