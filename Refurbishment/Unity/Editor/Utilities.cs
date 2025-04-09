#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Refurbishment.Unity
{
    public static class Utilities
    {
        public static void DrawPrefabPreview(Transform context, GameObject prefab, Color primary, Color secondary, ref List<MeshFilter> filters)
        {
            Matrix4x4 prevMatrix = Gizmos.matrix;
            Color prevColor = Gizmos.color;
            Gizmos.matrix = context.localToWorldMatrix;

            if (prefab == null) return;
            if (filters == null)
                filters = new List<MeshFilter>();
            if (filters.Count == 0)
                foreach (MeshFilter renderer in prefab.GetComponentsInChildren<MeshFilter>())
                    filters.Add(renderer);

            for (int i = 0; i < filters.Count; i++)
                if (filters[i] == null || filters[i].transform.root != prefab.transform)
                {
                    filters.Clear();
                    return;
                }

            Gizmos.color = new Color(primary.r, primary.g, primary.b, 0.3f);
            foreach (MeshFilter renderer in filters)
                Gizmos.DrawMesh(renderer.sharedMesh, renderer.transform.position, renderer.transform.rotation, renderer.transform.lossyScale);
            Gizmos.color = new Color(secondary.r, secondary.g, secondary.b, 0.05f);
            foreach (MeshFilter renderer in filters)
                Gizmos.DrawWireMesh(renderer.sharedMesh, renderer.transform.position, renderer.transform.rotation, renderer.transform.lossyScale);

            Gizmos.color = prevColor;
            Gizmos.matrix = prevMatrix;
        }

        public static void DrawCube(Vector3 position, Vector3 size, Color color, float overrideOpacity = 1f)
        {
            Gizmos.color = color;
            Gizmos.DrawWireCube(position, size);
            Gizmos.color = new Color(color.r, color.g, color.b, overrideOpacity);
            Gizmos.DrawCube(position, size);
        }
    }
}
#endif
