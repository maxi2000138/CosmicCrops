using System;
using System.Collections.Generic;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Utils.Expressions;
using _Project.Scripts.Utils.Parse;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts.Utils.PartLinears
{

	[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
	public class PartLinearsConfig : BaseConfig<string, PartLinearData>
	{
		public override string ConfigName => "PartLinears";

		protected override string GetKey(PartLinearData data) => data.Id;

		protected override PartLinearData ParseData(List<string> row)
		{
			var id = row[0];
			var data = new PartLinearData(id, ParsePartLinear(id, row[1], row[2]));
			Validate(data);
			return data;
		}

		private void Validate(PartLinearData data)
		{
			if (data.IsEmpty()) 
				Debug.LogError($"Partlinear {data.Id} has no values");
		}

		private SortedDictionary<int, float> ParsePartLinear(string id, string xStr, string yStr)
		{
			IReadOnlyList<int> xArr = HasLoopParams(xStr)
				? LoopToArray(xStr, StringParseUtils.ToInt)
				: StringParseUtils.ToIntArray(xStr);
			
			IReadOnlyList<float> yArr = HasLoopParams(yStr)
				? LoopToArray(yStr, StringParseUtils.ToFloat)
				: StringParseUtils.ToFloatArray(yStr);
			
			return ToDictionary(id, xArr, yArr);
		}

		private bool HasLoopParams(string str) => str.Split(":").Length == 3;

		private List<T> LoopToArray<T>(string str, Func<string, T> valueParser)
		{
			var loopParams = str.Split(":");
			T from = valueParser(loopParams[0]);
			T step = valueParser(loopParams[1]);
			int count  = StringParseUtils.ToInt(loopParams[2]);

			var result = new List<T>();
			var lastValue = from;
			result.Add(lastValue);
			
			for (var i = 1; i < count; i++)
			{
				lastValue = Add<T>.Do(lastValue, step);
				result.Add(lastValue);
			}

			return result;
		}

		private static SortedDictionary<int, float> ToDictionary(string id, IReadOnlyList<int> xArr, IReadOnlyList<float> yArr)
		{
			if (xArr.Count > yArr.Count)
				throw new ArgumentException($"PartLinear '{id}' should has the same X and Y values: {xArr.Count} != {yArr.Count}");
			
			var result = new Dictionary<int, float>();
			for (int i = 0; i < xArr.Count; i++)
			{
				if (result.ContainsKey(xArr[i]))
					throw new ArgumentException($"PartLinear '{id}' contains duplicated X keys: '{xArr[i]}'");
				
				result.Add(xArr[i], yArr[i]);
			}

			return new SortedDictionary<int, float>(result);
		}
	}
}