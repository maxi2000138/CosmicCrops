using _Project.Scripts.Game.Features.Inventory;
using _Project.Scripts.Game.Features.Units.Enemy._Presets;
using _Project.Scripts.Game.Features.Weapon._Configs;
using _Project.Scripts.Game.Features.Weapon._Configs.Data;
using _Project.Scripts.Infrastructure.StaticData;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Menu.Features.CharacterPreview.Components;
using _Project.Scripts.Menu.Features.CharacterPreview.Model;
using _Project.Scripts.Scenes.Menu.Infrastructure.Data;
using R3;
using VContainer;

namespace _Project.Scripts.Scenes.Menu.Features.Shop.Systems
{
  public class ShopCharacterPreviewSystem : SystemComponent<CharacterPreviewComponent>
  {
    private InventoryModel _inventoryModel;
    private CharacterPreviewModel _characterPreviewModel;
    private WeaponsConfig _weaponsConfig;
    private IStaticDataService _staticData;

    [Inject]
    private void Construct(InventoryModel inventoryModel, CharacterPreviewModel characterPreviewModel, 
      IStaticDataService staticData, WeaponsConfig weaponsConfig) 
    {
      _staticData = staticData;
      _weaponsConfig = weaponsConfig;
      _inventoryModel = inventoryModel;
      _characterPreviewModel = characterPreviewModel;
    }

    protected override void OnEnableComponent(CharacterPreviewComponent component)
    {
      base.OnEnableComponent(component);

      SubscribeOnChangeEquipment(component);
      SubscribeOnStartPreviewState(component);
    }

    protected override void OnDisableComponent(CharacterPreviewComponent component)
    {
      base.OnDisableComponent(component);

      component.Camera.targetTexture.Release();
      component.Camera.targetTexture = null;
    }

    private void SubscribeOnChangeEquipment(CharacterPreviewComponent component)
    {
      _inventoryModel.IndexWeapon.Value = _inventoryModel.GetWeaponIndex();
      _inventoryModel.IndexSkin.Value = _inventoryModel.GetSkinIndex();

      _inventoryModel.IndexWeapon
        .Subscribe(index => {
          SetWeapon(component.CharacterPreviewModel, index);
          SetAnimatorController(component, index);
        })
        .AddTo(component.LifetimeDisposable);

      _inventoryModel.IndexSkin
        .Subscribe(index => {
          SetEquipment(component.CharacterPreviewModel, index);
        })
        .AddTo(component.LifetimeDisposable);
    }

    private void SubscribeOnStartPreviewState(CharacterPreviewComponent component)
    {
      _characterPreviewModel.State
        .Where(state => state == PreviewState.Start)
        .Subscribe(_ => {
          _inventoryModel.IndexWeapon.Value = _inventoryModel.GetWeaponIndex();
          _inventoryModel.IndexSkin.Value = _inventoryModel.GetSkinIndex();
        })
        .AddTo(component.LifetimeDisposable);
    }

    private void SetWeapon(CharacterPreviewModelComponent component, int index)
    {
      for (int i = 0; i < component.Weapons.Length; i++)
      {
        component.Weapons[i].Weapon.SetActive(i == index);
      }

      _inventoryModel.SelectedWeapon.Value = component.Weapons[index].WeaponId;
    }

    private void SetEquipment(CharacterPreviewModelComponent component, int index)
    {
      for (int i = 0; i < component.Skins.Length; i++)
      {
        component.Skins[i].Visual.SetActive(i == index);
      }

      _inventoryModel.SelectedSkin.Value = component.Skins[index].SkinId;
    }
  
    private void SetAnimatorController(CharacterPreviewComponent component, int index)
    {
      component.CharacterPreviewAnimator.Animator.SetAnimatorController(
        _staticData.UnitAnimatorsPreset().Controllers[WeaponType(component, index)]);
    }

    private WeaponType WeaponType(CharacterPreviewComponent component, int index) =>
      _weaponsConfig.Data[component.CharacterPreviewModel.Weapons[index].WeaponId].WeaponType;
  }
}  