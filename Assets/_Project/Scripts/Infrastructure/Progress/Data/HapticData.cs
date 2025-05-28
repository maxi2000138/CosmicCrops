using System;
using _Project.Scripts.Infrastructure.SaveLoad;
using R3;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Progress.Data
{
  public class HapticData : ISaveLoad<bool>
  {
    private readonly IDisposable _disposable;
        
    private const int DefaultValue = 1;
        
    public ReactiveProperty<bool> Data { get; }

    public HapticData()
    {
      Data = new ReactiveProperty<bool>(Load());

      _disposable = Data.Subscribe(Save);
    }

    public void Save(bool data)
    {
      PlayerPrefs.SetInt(DataKeys.Haptic, data == false ? 0 : 1);
      PlayerPrefs.Save();
    }

    public bool Load()
    {
      int data = PlayerPrefs.GetInt(DataKeys.Haptic, DefaultValue);
            
      return data != 0;
    }

    void IDisposable.Dispose() => _disposable?.Dispose();
  }
}