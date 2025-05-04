using _Project.Scripts.Infrastructure.StaticData.Configs.Data;

namespace _Project.Scripts.Game.Features.Weapon.Data
{
  public class WeaponCharacteristicData
  {
    public WeaponType WeaponType;
    
    public string Ability;
    public ConfigPrefab Prefab;
    public int ClipCount;
    public float RechargeTime;
    public float FireInterval;
    public float DetectionDistance;
    public float AttackDistance;
    public float Aiming;
    public float ForceBullet;
  }
}