using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts._Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(CharacterData), menuName = "Data/" + nameof(CharacterData))]
    public sealed class CharacterData : ScriptableObject
    {
        [Range(1, 20)] public int Health;
        [Range(1f, 5f)] public float Speed;
        public AssetReference PrefabReference;
    }
}