#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Refurbishment.Unity
{
    public enum LayoutMode { Horizontal, Vertical, None }
    public static class LabelUtilities
    {
        public static void DrawFields<T>(string fieldName, List<T> unityObjects, LayoutMode layoutMode) where T : UnityEngine.Object
        {
            BeginLayoutMode(layoutMode);
            EditorGUILayout.SelectableLabel(fieldName);
            foreach (T unityObject in unityObjects)
            {
                EditorGUILayout.ObjectField(unityObject, typeof(T), true);
            }
            EndLayoutMode(layoutMode);
        }

        public static void DrawField<T>(string fieldName, T unityObject, LayoutMode layoutMode) where T : UnityEngine.Object
        {
            BeginLayoutMode(layoutMode);
            EditorGUILayout.SelectableLabel(fieldName);
            EditorGUILayout.ObjectField(unityObject, typeof(T), true);
            EndLayoutMode(layoutMode);
        }

        public static void DrawValue<T>(T unityObject) where T : UnityEngine.Object
        {
            EditorGUILayout.ObjectField(unityObject, typeof(T), true);
        }

        private static void BeginLayoutMode(LayoutMode layoutMode)
        {
            if (layoutMode == LayoutMode.Horizontal)
                EditorGUILayout.BeginHorizontal(GUILayout.ExpandHeight(false));
            else if (layoutMode == LayoutMode.Vertical)
                EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(false));
        }

        private static void EndLayoutMode(LayoutMode layoutMode)
        {
            if (layoutMode == LayoutMode.Horizontal)
                EditorGUILayout.EndHorizontal();
            else if (layoutMode == LayoutMode.Vertical)
                EditorGUILayout.EndVertical();
        }
    }
}
#endif
