#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Refurbishment.Unity
{
    [FilePath("/Assets/refurbishmentsingleton.data", FilePathAttribute.Location.ProjectFolder)]
    public class EditorSingleton : ScriptableSingleton<EditorSingleton>
    {
        [HideInInspector] public DungeonPreset selectedPreset;


        private void OnDisable() => Save(true);


        [MenuItem("Refurbishment/Open Settings", priority = -1)]
        public static void OpenSettings()
        {
            Selection.activeObject = EditorSettings.Instance;
        }
    }
}
#endif