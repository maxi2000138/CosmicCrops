using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Utils.Parse;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.StaticData.Configs.Loader
{
    public class ConfigsLoader : IConfigsLoader
    {
        private const int ProcessConfigsPerFrame = 4;

        private readonly IAssetProvider _assetProvider;
        private readonly IReadOnlyList<IConfigParser> _configParsers;

        public ConfigsLoader(IEnumerable<IConfigParser> configParsers, IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _configParsers = configParsers.ToList();
        }

        public async UniTask LoadConfigs(Action<float> onProgress)
        {
            await LoadConfigs(_configParsers, onProgress);
        }

        private async UniTask LoadConfigs(IReadOnlyList<IConfigParser> configParsers, Action<float> onProgress)
        {
            var configCount = configParsers.Count;

            var i = 0;
            while (i < configCount)
            {
                var parser = configParsers[i];
                var data = await LoadTsv(parser.ConfigName);
                parser.Parse(data);

                onProgress?.Invoke((float)i / configCount);
                i++;

                if (i % ProcessConfigsPerFrame == 0)
                   await UniTask.NextFrame();
            }
        }

        private async UniTask<List<List<string>>> LoadTsv(string configName)
        {
            var textAsset = await _assetProvider.LoadFromAddressable<TextAsset>(configName);
            var data = TsvHelper.ParseTsv(textAsset.text);
            
            data.RemoveAt(0); //ignore headers row
            return data;
        }
    }
}