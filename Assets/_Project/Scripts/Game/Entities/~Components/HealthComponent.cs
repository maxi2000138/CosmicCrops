using _Project.Scripts.Infrastructure.Systems.Components;
using R3;
using UnityEngine;

namespace _Project.Scripts.Game.Entities._Components
{
  public class HealthComponent : MonoComponent<HealthComponent>
  {
    public int MaxHealth { get; private set; }
    public int BaseHealth { get; private set; }
    public ReactiveProperty<int> CurrentHealth { get; } = new();
    
    public bool IsAlive => CurrentHealth.Value > 0;


    public void SetMaxHealth(int maxHealth) => MaxHealth = maxHealth;
    public void SetBaseHealth(int baseHealth) => BaseHealth = baseHealth;
    
    public override string ToString() => string.Format("{0}/{1}", Mathf.Clamp(CurrentHealth.Value, 0, MaxHealth).ToString(), MaxHealth.ToString());
  }
}