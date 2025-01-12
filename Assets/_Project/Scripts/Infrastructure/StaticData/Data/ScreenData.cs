using System;
using System.Collections.Generic;
using _Project.Scripts.UI.Screens;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(ScreenData), menuName = "Data/" + nameof(ScreenData))]
    public sealed class ScreenData : SerializedScriptableObject
    {
        public Dictionary<ScreenType, ScreenInfo> Screens;
    }
    
    [Serializable]
    public class ScreenInfo
    {
        public AssetReference PrefabReference;
    }
}