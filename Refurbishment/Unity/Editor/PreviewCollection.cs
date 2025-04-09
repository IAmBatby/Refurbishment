using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Refurbishment.Unity
{
    public class PreviewInfoGroup
    {
        private List<PreviewInfo> previewInfos = new List<PreviewInfo>();

        public PreviewInfoGroup(List<GameObject> contexts, List<GameObject> prefabs, Color newPrimary, Color newSecondary)
        {
            for (int i = 0; i < contexts.Count; i++)
                previewInfos.Add(new(contexts[i], prefabs[i], newPrimary, newSecondary));
        }

        public void PreviewAll()
        {
            foreach (PreviewInfo info in previewInfos)
                info.Preview();
        }
    }

    public class PreviewInfo
    {
        private GameObject context;
        private GameObject prefab;
        private List<MeshFilter> cachedFilters = new List<MeshFilter>();
        private Color primaryColor;
        private Color secondaryColor;

        public PreviewInfo(GameObject newContext, GameObject newPrefab, Color newPrimary, Color newSecondary)
        {
            context = newContext;
            prefab = newPrefab;
            primaryColor = newPrimary;
            secondaryColor = newSecondary;
            cachedFilters = newPrefab.GetComponentsInChildren<MeshFilter>().ToList();
        }

        public void Preview()
        {
            Utilities.DrawPrefabPreview(context.transform, prefab, primaryColor, secondaryColor, ref cachedFilters);
        }
    }
}
