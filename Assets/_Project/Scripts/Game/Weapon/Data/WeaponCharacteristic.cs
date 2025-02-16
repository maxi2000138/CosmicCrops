using UnityEngine;

namespace _Project.Scripts.Game.Weapon.Data
{
  [System.Serializable]
  public struct WeaponCharacteristic
  {
    [Range(1,100)] public int ClipCount;
    [Range(0f,10f)] public float RechargeTime;
    [Range(0f,10f)] public float FireInterval;
    [Range(0f,20f)] public float DetectionDistance;
    [Range(0f,20f)] public float AttackDistance;
    [Range(0f, 1f)] public float Aiming;
  }
}