using LethalLevelLoader;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Refurbishment.Unity
{
    [CreateAssetMenu(fileName = "DungeonPreset", menuName = "ScriptableObjects/Refurbishment/DungeonPreset", order = 1)]
    public class DungeonPreset : ScriptableObject
    {
        [field: Header("References")]
        [field: SerializeField] public ExtendedDungeonFlow ExtendedDungeonFlow { get; private set; }

        [field: Space(20), Header("Values")]
        [field: SerializeField] public float PreferredSubTileScale { get; private set; }
        [field: SerializeField] public float PreferredSubTileThickness { get; private set; }

    }
}
