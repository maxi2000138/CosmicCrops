using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(CharacterConfig), menuName = "Config/" + nameof(CharacterConfig))]
    public sealed class CharacterConfig : ScriptableObject
    {
        [Range(1, 20)] public int Health;
        [Range(1f, 5f)] public float Speed;
        public AssetReference PrefabReference;
    }


}