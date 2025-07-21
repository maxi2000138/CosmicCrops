using _Project.Scripts.Infrastructure.StaticData.Presets;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Menu.Features.CharacterPreview._Preset
{
  [CreateAssetMenu(fileName = nameof(CharacterPreviewPreset), menuName = "Presets/" + nameof(CharacterPreviewPreset))]
  public class CharacterPreviewPreset : BasePreset
  { 
    public AssetReference PrefabReference;
  }
}