using _Project.Scripts.Infrastructure.AssetData;
using _Project.Scripts.Infrastructure.StaticData;
using _Project.Scripts.Menu.Features.CharacterPreview._Preset;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Menu.Infrastructure.Factory
{
  public class MenuFactory : IMenuFactory
  {
    private readonly IStaticDataService _staticDataService;
    private readonly IAssetProvider _assetProvider;
    public MenuFactory(IStaticDataService staticDataService, IAssetProvider assetProvider)
    {
      _staticDataService = staticDataService;
      _assetProvider = assetProvider;
    }

    public async UniTask<CharacterPreviewComponent> CreateCharacterPreview()
    {
      CharacterPreviewPreset data = _staticDataService.CharacterPreviewPreset();
      GameObject prefab = await _assetProvider.LoadFromAddressable<GameObject>(data.PrefabReference);
      return Object.Instantiate(prefab).GetComponent<CharacterPreviewComponent>();
    }
  }
}