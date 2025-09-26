using System;
using _Project.Scripts.Infrastructure.SaveLoad;
using _Project.Scripts.Utils.Extensions;
using R3;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;


namespace _Project.Scripts.Infrastructure.Progress.Data
{
  public sealed class InventoryData : ISaveLoad<Inventory>
  {
    private readonly CompositeDisposable _disposable;

    public ReactiveProperty<Inventory> Data { get; }

    public InventoryData()
    {
      Data = new ReactiveProperty<Inventory>(Load());
            
      _disposable = new CompositeDisposable();
            
      SubscribeOnDataChanged();
    }

    public void Save(Inventory data)
    {
      PlayerPrefs.SetString(DataKeys.Inventory, data.ToSerialize());
      PlayerPrefs.Save();
    }

    public Inventory Load()
    {
      return PlayerPrefs.HasKey(DataKeys.Inventory)
        ? PlayerPrefs.GetString(DataKeys.Inventory)?.ToDeserialize<Inventory>()
        : SetDefaultValue();
    }

    private Inventory SetDefaultValue()
    {
      Inventory inventory = new Inventory
      {
        WeaponIndex = default,
        EquipmentIndex = default
      };

      return inventory;
    }

    private void SubscribeOnDataChanged()
    {
      Data
        .ObserveEveryValueChanged(data => data.WeaponIndex)
        .Subscribe(_ => Save(Data.Value))
        .AddTo(_disposable);
            
      Data
        .ObserveEveryValueChanged(data => data.EquipmentIndex)
        .Subscribe(_ => Save(Data.Value))
        .AddTo(_disposable);
    }

    void IDisposable.Dispose() => _disposable.Clear();
  }

  [JsonObject]
  public sealed class Inventory
  {
    public int WeaponIndex;
    public int EquipmentIndex;
  }
}