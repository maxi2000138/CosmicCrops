using System;
using System.Collections.Generic;
using _Project.Scripts.UI.Screens;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(ScreenConfig), menuName = "Config/" + nameof(ScreenConfig))]
    public sealed class ScreenConfig : SerializedScriptableObject
    {
        public Dictionary<ScreenType, ScreenData> Screens;
    }
    
    [Serializable]
    public class ScreenData
    {
        public AssetReference PrefabReference;
    }
}