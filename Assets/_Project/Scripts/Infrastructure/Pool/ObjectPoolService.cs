using System;
using System.Collections.Generic;
using _Project.Scripts.Infrastructure.Factories.Game;
using _Project.Scripts.Infrastructure.Logger;
using _Project.Scripts.Infrastructure.Pool.Item;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Pool
{
	[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class ObjectPoolService : IObjectPoolService, IDisposable
    {
	    private Transform _root;
	    private readonly Transform _parent;
	    private bool _logStatus;
	    private bool _dirty;
	    private List<ObjectPoolItem> _poolItems;
	    private List<ObjectPoolContainer<MonoSpawnableItem>> _release;
	    private IDictionary<MonoSpawnableItem, ObjectPool<MonoSpawnableItem>> _prefabLookup;
	    private IDictionary<MonoSpawnableItem, ObjectPool<MonoSpawnableItem>> _instanceLookup;
	    
	    public ObjectPoolService(Transform parent)
	    {
		    _parent = parent;
	    }

	    UniTask IObjectPoolService.Init()
	    {
		    _root = new GameObject().transform;
		    _root.SetParent(_parent);
		    _root.name = "Pool";
		    
		    _prefabLookup = new Dictionary<MonoSpawnableItem, ObjectPool<MonoSpawnableItem>>();
		    _instanceLookup = new Dictionary<MonoSpawnableItem, ObjectPool<MonoSpawnableItem>>();
		    _release = new List<ObjectPoolContainer<MonoSpawnableItem>>();
		    
		    return UniTask.CompletedTask;
	    }
	    
	    void IObjectPoolService.Execute()
	    {
		    if (_logStatus && _dirty)
		    {
			    PrintStatus();
			    
			    _dirty = false;
		    }

		    if (_release.Count > 0)
		    {
			    for (int i = _release.Count - 1; i >= 0; i--)
			    {
				    _release[i].Time -= UnityEngine.Time.deltaTime;

				    if (_release[i].Time < 0f)
				    {
					    Release(_release[i].Item);
					    
					    _release.Remove(_release[i]);
				    }
			    }
		    }
	    }

	    MonoSpawnableItem IObjectPoolService.SpawnObject(MonoSpawnableItem prefab)
	    {
		    return Spawn(prefab);
	    }

	    MonoSpawnableItem IObjectPoolService.SpawnObject(MonoSpawnableItem prefab, Vector3 position, Quaternion rotation, Transform parent)
	    {
		    return Spawn(prefab, position, rotation, parent);
	    }

	    void IObjectPoolService.ReleaseObject(MonoSpawnableItem clone)
	    {
		    Release(clone);
	    }

	    void IObjectPoolService.ReleaseObjectAfterTime(MonoSpawnableItem clone, float time)
	    {
		    ObjectPoolContainer<MonoSpawnableItem> container = _instanceLookup[clone].GetContainer(clone);
		    container.Time = time;
		    _release.Add(container);
	    }

	    void IObjectPoolService.ReleaseAll()
	    {
		    foreach (KeyValuePair<MonoSpawnableItem, ObjectPool<MonoSpawnableItem>> keyValuePair in _instanceLookup)
		    {
			    keyValuePair.Key.gameObject.SetActive(false);
			    keyValuePair.Value.ReleaseAll();
		    }
		    
		    _release.Clear();
		    _instanceLookup.Clear();
	    }

	    private void Warm(MonoSpawnableItem prefab, int size)
	    {
		    if (_prefabLookup.ContainsKey(prefab))
		    {
			    DebugLogger.LogError($"Pool for prefab {prefab.name} has already been created", LogsType.Pool);
		    }

		    ObjectPool<MonoSpawnableItem> pool = new ObjectPool<MonoSpawnableItem>(() => InstantiatePrefab(prefab), size);
		    
		    _prefabLookup[prefab] = pool;
		    
		    _dirty = true;
	    }

	    private MonoSpawnableItem Spawn(MonoSpawnableItem prefab)
	    {
		    return Spawn(prefab, Vector3.zero, Quaternion.identity, null);
	    }

	    private MonoSpawnableItem Spawn(MonoSpawnableItem prefab, Vector3 position, Quaternion rotation, Transform parent)
	    {
		    if (!_prefabLookup.ContainsKey(prefab))
		    {
			    WarmPool(prefab, 1);
		    }

		    ObjectPool<MonoSpawnableItem> pool = _prefabLookup[prefab];

		    MonoSpawnableItem clone = pool.GetItem();
		    clone.OnSpawned();
		    
		    clone.transform.position = position;
		    clone.transform.rotation = rotation;
		    if (parent != null) clone.transform.parent = parent;

		    _instanceLookup.Add(clone, pool);
		    
		    _dirty = true;
		    
		    return clone;
	    }

	    private void Release(MonoSpawnableItem clone)
	    {
		    clone.OnDespawned();
		    
		    if (_instanceLookup.ContainsKey(clone))
		    {
			    _instanceLookup[clone].ReleaseItem(clone);
			    _instanceLookup.Remove(clone);
			    _dirty = true;
		    }
		    else
		    {
			    DebugLogger.LogWarning($"No pool contains the object: {clone.name}", LogsType.Pool);
		    }
	    }

	    private MonoSpawnableItem InstantiatePrefab(MonoSpawnableItem prefab)
	    {
		    var spawnableItem = UnityObjectFactory.Instantiate(prefab);
		    spawnableItem.OnCreated(this);
		    
		    if (_root != null)
		    {
			    spawnableItem.SetParent(_root);
		    }

		    return spawnableItem;
	    }

	    private void PrintStatus()
	    {
		    foreach (KeyValuePair<MonoSpawnableItem, ObjectPool<MonoSpawnableItem>> dictionary in _prefabLookup)
		    {
			    string message = $"Object Pool for Prefab: {dictionary.Key.name} In Use: {dictionary.Value.CountUsedItems.ToString()} Total: {dictionary.Value.Count.ToString()}";
			    
			    DebugLogger.Log(message, LogsType.Pool, DebugColorType.Lime);
		    }
	    }
	    
	    private void WarmPool(MonoSpawnableItem prefab, int size)
	    {
		    Warm(prefab, size);
	    }

	    void IDisposable.Dispose()
	    {
		    _prefabLookup.Clear();
		    _instanceLookup.Clear();
	    }
    }
}