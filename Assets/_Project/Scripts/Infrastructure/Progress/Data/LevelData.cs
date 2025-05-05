using System;
using _Project.Scripts.Infrastructure.SaveLoad;
using R3;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Progress.Data
{
    public sealed class LevelData : ISaveLoad<int>
    {
        private readonly IDisposable _disposable;
        
        private const int DefaultValue = 1;

        public ReadOnlyReactiveProperty<int> Data { get; }

        public LevelData()
        {
            Data = new ReactiveProperty<int>(Load());

            _disposable = Data.Subscribe(Save);
        }

        public void Save(int data)
        {
            PlayerPrefs.SetInt(DataKeys.Level, data);
            PlayerPrefs.Save();
        }

        public int Load() => PlayerPrefs.GetInt(DataKeys.Level, DefaultValue);

        void IDisposable.Dispose() => _disposable?.Dispose();
    }
}