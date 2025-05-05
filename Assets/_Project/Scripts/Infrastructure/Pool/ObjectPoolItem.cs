using UnityEngine;

namespace _Project.Scripts.Infrastructure.Pool
{
    public struct ObjectPoolItem
    {
        public readonly GameObject Prefab;
        public readonly int Count;

        public ObjectPoolItem(GameObject prefab, int count)
        {
            Prefab = prefab;
            Count = count;
        }
    }
}