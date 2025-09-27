using _Project.Scripts.Infrastructure.Progress;
using R3;

namespace _Project.Scripts.Game.Features.Inventory
{
  public class InventoryModel
  {
    private readonly IProgressService _progressService;

    public ReactiveProperty<string> SelectedWeapon { get; } = new ReactiveProperty<string>();
    public ReactiveProperty<string> SelectedSkin { get; } = new ReactiveProperty<string>();
    public ReactiveProperty<int> IndexWeapon { get; } = new ReactiveProperty<int>();
    public ReactiveProperty<int> IndexSkin { get; } = new ReactiveProperty<int>();
    public ReactiveCommand<float> StartCollectingLoot { get; } = new ReactiveCommand<float>();
    public ReactiveCommand CancelCollectingLoot { get; } = new ReactiveCommand();

    public InventoryModel(IProgressService progressService)
    {
      _progressService = progressService;
    }

    public int GetSkinIndex() => _progressService.InventoryData.Data.Value.EquipmentIndex;
    public int GetWeaponIndex() => _progressService.InventoryData.Data.Value.WeaponIndex;
    public void SetSkinIndex(int index) => _progressService.InventoryData.Data.Value.EquipmentIndex = index;
    public void SetWeaponIndex(int index) => _progressService.InventoryData.Data.Value.WeaponIndex = index;
  }
}