using _Project.Scripts.Infrastructure.Pool.Item;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Pool
{
    public interface IObjectPoolService
    {
        UniTask Init();
        void Execute();
        MonoSpawnableItem SpawnObject(MonoSpawnableItem prefab);
        MonoSpawnableItem SpawnObject(MonoSpawnableItem prefab, Vector3 position, Quaternion rotation, Transform parent);
        void ReleaseObject(MonoSpawnableItem clone);
        void ReleaseObjectAfterTime(MonoSpawnableItem clone, float time);
        void ReleaseAll();
    }
}