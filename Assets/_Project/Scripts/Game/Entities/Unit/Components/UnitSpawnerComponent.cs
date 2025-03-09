using System.Collections;
using _Project.Scripts.Game.Entities.Unit._Configs;
using _Project.Scripts.Infrastructure.Systems.Components;
using _Project.Scripts.Utils.Parse;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Game.Entities.Unit.Components
{
  public class UnitSpawnerComponent : MonoComponent<UnitSpawnerComponent>
  { 
    [ValueDropdown(nameof(GetAllUnits))]
    [SerializeField] private string _unit;

    public Vector3 Position => transform.position;
    public string Unit => _unit;

    
    private static IEnumerable GetAllUnits()
    {
#if UNITY_EDITOR
      UnitsConfig configParser = new UnitsConfig();
      var textAsset = Addressables.LoadAssetAsync<TextAsset>(configParser.ConfigName).WaitForCompletion();
      
      var data = TsvHelper.ParseTsv(textAsset.text);
      data.RemoveAt(0);

      configParser.Parse(data);
      
      return configParser.Data.Keys;
#endif
    }
  }
}