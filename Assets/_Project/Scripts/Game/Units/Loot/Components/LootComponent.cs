using _Project.Scripts.Game.Loot.Interface;
using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;

namespace _Project.Scripts.Game.Loot.Components
{
  public class LootComponent : MonoComponent<LootComponent>, ILoot
  {
    public Vector3 Position => transform.position;

    public void Destroy() => Destroy(gameObject);
  }
}