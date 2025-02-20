using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Utils.Extensions;
using UnityEngine;

namespace _Project.Scripts.Utils.PartLinears
{
	public class PartLinearData
	{
		public readonly string Id;
		private readonly IReadOnlyDictionary<int, float> _map;

		public PartLinearData(string id, SortedDictionary<int, float> map)
		{
			Id = id;
			_map = map;
		}

		public bool IsEmpty() => _map.IsNullOrEmpty();
		public bool HasKey(int level) => _map.TryGetValue(level, out _);
		public int GetLastKey() => _map.Keys.Last();

		public float GetByLevel(int level)
		{
			if (_map.TryGetValue(level, out var value))
				return value;

			var lastPair = _map.Last();
			if (level > lastPair.Key)
				return lastPair.Value;
			
			Debug.LogError($"Level '{level}' not found in map '{Id}'");
			return _map.Last().Value;
		}
	}
}